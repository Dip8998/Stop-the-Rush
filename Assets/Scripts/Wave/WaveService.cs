using STR.Enemy;
using STR.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace STR.Wave
{
	public class WaveService : MonoBehaviour
	{
        [SerializeField] private WaveScriptableObject wave;
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private float timeBetweenWaves = 5f;

        private List<EnemyController> spawnedEnemies = new List<EnemyController>();
        private List<int> spawnedPerGroup = new List<int>();

        private float spawnTimer;
        private float waveTimer;

        private int spawnedEnemyCount;
        private int lastGroupIndex;
        private int currentWaveIndex = 0;
        private int totalEnemiesInWave;
        private WaveData currentWaveData;

        private WaveType currentWaveType = WaveType.WaitingToStart;

        private void Start()
        {
            waveTimer = timeBetweenWaves;
            UIEvents.RaiseWaveChanged(currentWaveIndex + 1);
            UIEvents.RaiseWaveTimerVisibilityChanged(true);

            ValidateReferences();
        }

        private void Update()
        {
            if (!IsWaveSetupValid()) return;

            UpdateWave();
            TickUpdate();
            UIEvents.RaiseWaveTimerChanged(waveTimer);
        }

        private void UpdateWave()
        {
            switch (currentWaveType)
            {
                case WaveType.WaitingToStart:
                    waveTimer -= Time.deltaTime;
                    if(waveTimer <= 0f)
                    {
                        currentWaveData = GetCurrentWaveData();
                        if (currentWaveData == null)
                        {
                            currentWaveType = WaveType.Completed;
                            break;
                        }

                        totalEnemiesInWave = GetTotalEnemies(currentWaveData);
                        currentWaveType = WaveType.Spawning;
                        spawnTimer = 0f;
                        spawnedEnemyCount = 0;
                        lastGroupIndex = -1;
                        InitializeGroupCounts();
                        UIEvents.RaiseWaveTimerVisibilityChanged(false);
                    }
                    break;

                case WaveType.Spawning:
                    if (currentWaveData == null)
                    {
                        currentWaveType = WaveType.Completed;
                        break;
                    }

                    spawnTimer -= Time.deltaTime;
                    if (TrySpawnEnemy(currentWaveData))
                        spawnTimer = currentWaveData.SpawnInterval;

                    if (CurrentWaveCompleted(currentWaveData))
                        currentWaveType = WaveType.WaitingToComplete;

                    break;

                case WaveType.WaitingToComplete:
                    if(spawnedEnemies.Count == 0)
                    {
                        AdvanceWaveOrFinished();
                    }
                    break;
            }
        }

        private void FixedUpdate()
        {
            FixedTickUpdate();
        }

        private void TickUpdate()
        {
            for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
            {
                if(spawnedEnemies[i].IsAlive == false)
                {
                    spawnedEnemies.RemoveAt(i);
                    continue;
                }

                spawnedEnemies[i].Tick();
            }
        }

        private void FixedTickUpdate()
        {
            for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
            {
                spawnedEnemies[i].FixedTick();
            }
        }

        private WaveData GetCurrentWaveData()
        {
            if (currentWaveIndex >= wave.Waves.Count)
            {
                return null;
            }

            return wave.Waves[currentWaveIndex];
        }

        private bool TrySpawnEnemy(WaveData currentWaveData)
        {
            if (currentWaveData == null) return false;

            if (spawnedEnemyCount >= GetTotalEnemies(currentWaveData)) return false;

            if (spawnTimer > 0f) return false;

            WaveEnemyGroup group = GetNextGroup(currentWaveData);

            if (group == null || group.Enemy == null) return false;

            SpawnEnemy(group.Enemy);
            spawnedEnemyCount++;
            spawnedPerGroup[lastGroupIndex]++;

            spawnTimer = currentWaveData.SpawnInterval;
            return true;
        }

        private void AdvanceWaveOrFinished()
        {
            if (wave.Waves.Count == 0)
            {
                currentWaveType = WaveType.Completed;
                UIEvents.RaiseGameWinTriggered();
                return;
            }

            if (currentWaveIndex >= wave.Waves.Count - 1)
            {
                currentWaveType = WaveType.Completed;
                UIEvents.RaiseGameWinTriggered();
                return;
            }

            currentWaveIndex++;
            UIEvents.RaiseWaveChanged(currentWaveIndex + 1);
            waveTimer = timeBetweenWaves;
            spawnedEnemyCount = 0;
            spawnTimer = 0f;
            lastGroupIndex = -1;
            spawnedPerGroup.Clear();
            currentWaveData = null;
            totalEnemiesInWave = 0;
            currentWaveType = WaveType.WaitingToStart;
            UIEvents.RaiseWaveTimerVisibilityChanged(true);
        }

        private bool CurrentWaveCompleted(WaveData currentWaveData)
        {
            if (currentWaveData == null) return true;

            if (spawnedEnemyCount >= GetTotalEnemies(currentWaveData)) return true;

            return false;
        }

        private void SpawnEnemy(EnemyScriptableObject enemyData)
        {
            EnemyController newEnemy = new EnemyController(enemyData, enemySpawnPoint, waypoints);
            spawnedEnemies.Add(newEnemy);
        }

        private void InitializeGroupCounts()
        {
            spawnedPerGroup.Clear();
            if (wave == null || currentWaveIndex >= wave.Waves.Count) return;

            WaveData currentWaveData = wave.Waves[currentWaveIndex];
            for (int i = 0; i < currentWaveData.EnemyGroups.Count; i++)
            {
                spawnedPerGroup.Add(0);
            }
        }

        private WaveEnemyGroup GetNextGroup(WaveData currentWaveData)
        {
            if (currentWaveData.EnemyGroups == null || currentWaveData.EnemyGroups.Count == 0) return null;

            if (spawnedPerGroup.Count != currentWaveData.EnemyGroups.Count) InitializeGroupCounts();

            int groupCount = currentWaveData.EnemyGroups.Count;
            for (int i = 0; i < groupCount; i++)
            {
                int index = (lastGroupIndex + 1 + i) % groupCount;
                WaveEnemyGroup group = currentWaveData.EnemyGroups[index];
                if (spawnedPerGroup[index] < group.Count)
                {
                    lastGroupIndex = index;
                    return group;
                }
            }

            return null;
        }

        private int GetTotalEnemies(WaveData currentWaveData)
        {
            if (currentWaveData.EnemyGroups == null) return 0;

            int total = 0;
            for (int i = 0; i < currentWaveData.EnemyGroups.Count; i++)
            {
                total += currentWaveData.EnemyGroups[i].Count;
            }

            return total;
        }

        private void ValidateReferences()
        {
            if (wave == null) 
                Debug.LogError("WaveService: Wave data is not assigned.");
            else if (wave.Waves == null || wave.Waves.Count == 0)
                Debug.LogError("WaveService: Wave list is empty.");

            if (enemySpawnPoint == null)
                Debug.LogError("WaveService: Enemy spawn point is not assigned.");

            if (waypoints == null || waypoints.Count == 0)
                Debug.LogError("WaveService: Waypoints are not assigned.");
        }

        private bool IsWaveSetupValid()
        {
            return wave != null && wave.Waves != null && wave.Waves.Count > 0 && enemySpawnPoint != null && waypoints != null && waypoints.Count > 0;
        }
    }
}
