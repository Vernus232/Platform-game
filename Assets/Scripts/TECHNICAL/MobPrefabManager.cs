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

public class MobPrefabManager : MonoBehaviour
{
    [HideInInspector] public static MobPrefabManager main;
    private Dictionary<MobEnum, GameObject> mobEnumPrefabDict = new Dictionary<MobEnum, GameObject>();
    
    private void Start() 
    {
        main = this;

        for (int i = 0; i < (int)MobEnum.End; i++)
        {
            foreach (Enemy enemy in Resources.LoadAll<Enemy>("Enemies"))
            {
                if ((int)enemy.type == i)
                {
                    mobEnumPrefabDict.Add((MobEnum)i, enemy.gameObject);
                    break;
                }
            }
        }
    }

    public GameObject GetPrefab(MobEnum mobEnum)
    {
        return mobEnumPrefabDict[mobEnum];
    }
}
