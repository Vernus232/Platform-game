using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobEnum
{
	Zombie,
	Head,
	FatZombie,
	FatHead
}

[CreateAssetMenu(fileName = "SpawnMobMix", menuName = "Spawn/SpawnMobMix", order = 0)]
public class SpawnMobMix : ScriptableObject
{
    public MobEnum[] mobNames;
    public int[] mobOdds;

}
