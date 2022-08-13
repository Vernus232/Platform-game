using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradableEntity : MonoBehaviour
{
    //private int level;
    //[SerializeField] private int maxLevel;

    public abstract void OnUpgradeReceived();
}
