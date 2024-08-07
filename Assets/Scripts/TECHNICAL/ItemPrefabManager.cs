using System.Collections.Generic;
using UnityEngine;

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

public class ItemPrefabManager : MonoBehaviour
{
    [HideInInspector] public static ItemPrefabManager main;
    private Dictionary<ItemEnum, GameObject> itemEnumPrefabDict = new Dictionary<ItemEnum, GameObject>();
    
    private void Start() 
    {
        main = this;

        #region Fill itemEnumPrefabDict
        for (int i = 0; i < (int)ItemEnum.End; i++)
        {
            foreach (Item item in Resources.LoadAll<Item>("Items"))
            {
                if ((int)item.type == i)
                {
                    itemEnumPrefabDict.Add((ItemEnum)i, item.gameObject);
                    break;
                }
            }
        }
        #endregion
    }

    public GameObject GetPrefab(ItemEnum itemEnum)
    {
        return itemEnumPrefabDict[itemEnum];
    }
}
