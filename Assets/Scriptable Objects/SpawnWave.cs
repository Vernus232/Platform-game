using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnWave", menuName = "Spawn/SpawnWave", order = 1)]
public class SpawnWave : ScriptableObject
{
    public float offset;
    public float duration;
    public float scale;

    public SpawnMobMix mobMix;

}
