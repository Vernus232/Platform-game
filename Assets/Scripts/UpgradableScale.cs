using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableScale : UpgradableEntity
{
    [SerializeField] private float maxMul = 10;
    [SerializeField] private float stepMul = 1.1f;
    private float startScaleX;
    private float scaleX;

    private void Start()
    {
        scaleX = transform.localScale.x;
        startScaleX = scaleX;
    }

    public override void OnUpgradeReceived()
    {
        if (scaleX / startScaleX < maxMul)
        {
            transform.localScale *= stepMul;
        }
    }
}
