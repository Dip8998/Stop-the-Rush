using UnityEngine;

namespace STR.Wave
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveScriptableObject", order = 1)]
    public class WaveScriptableObject : ScriptableObject
    {
        public int EnemyCount;
        public float SpawnInterval;
    }
}
