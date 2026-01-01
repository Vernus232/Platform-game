using System.Collections.Generic;
using UnityEngine;

public enum MobEnum
{
	Zombie,
	Head,
	FatZombie,
	FatHead,
	Spawner,
    End
}

public enum ItemEnum
{
	AccBoost,
	DmgBoost,
	FrRtBoost,
	Heal,
	HpBoost,
	RldSpdBoost,
	Win,
    End
}


public class SpawnPrefabManager : MonoBehaviour
{
    [HideInInspector] public static SpawnPrefabManager main;
    private Dictionary<System.Enum, GameObject> enumPrefabDict = new Dictionary<System.Enum, GameObject>();
    
    private void Start() 
    {
        main = this;

        for (int i = 0; i < (int)MobEnum.End; i++)
        {
            foreach (Enemy enemy in Resources.LoadAll<Enemy>("Enemies"))
            {
                if ((int)enemy.type == i)
                {
                    enumPrefabDict.Add((MobEnum)i, enemy.gameObject);
                    break;
                }
            }
        }
        
        for (int i = 0; i < (int)ItemEnum.End; i++)
        {
            foreach (Item item in Resources.LoadAll<Item>("Items"))
            {
                if ((int)item.type == i)
                {
                    enumPrefabDict.Add((ItemEnum)i, item.gameObject);
                    break;
                }
            }
        }
    }

    public GameObject GetPrefab(System.Enum spawnEnum)
    {
        return enumPrefabDict[spawnEnum];
    }
}
