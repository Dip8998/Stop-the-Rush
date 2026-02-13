using STR.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace STR.Wave
{
    [System.Serializable]
    public class WaveEnemyGroup
    {
        public EnemyScriptableObject Enemy;
        public int Count;
    }

    [System.Serializable]
    public class WaveData
    {
        public List<WaveEnemyGroup> EnemyGroups = new List<WaveEnemyGroup>();
        public float SpawnInterval;
    }

    [CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveScriptableObject", order = 1)]
    public class WaveScriptableObject : ScriptableObject
    {
        public List<WaveData> Waves = new List<WaveData>();
    }
}
