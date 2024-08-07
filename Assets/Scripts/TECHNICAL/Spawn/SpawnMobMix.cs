using UnityEngine;
using SpawnNamespace;


[CreateAssetMenu(fileName = "SpawnMobMix", menuName = "Spawn/SpawnMobMix", order = 0)]
public class SpawnMobMix : SpawnMix
{
    public MobEnum[] names;
}