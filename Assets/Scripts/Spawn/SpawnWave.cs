using UnityEngine;

[CreateAssetMenu(fileName = "SpawnWave", menuName = "Spawn/SpawnWave", order = 1)]
public class SpawnWave : ScriptableObject
{
    [Tooltip("Offset in minutes, when to start a wave")]
    public float offset;
    
    [Tooltip("Duration in minutes")]
    public float duration;
    
    [Tooltip("Amount of mobs")]
    public float count;

    [Tooltip("Mob mixes define proportions of mobs, and mobs to appear")]
    public ScriptableObject spawnMix;
}
