using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class UpgradableTurret : UpgradableEntity
{
    [SerializeField] private float maxDmgMul = 10;
    [SerializeField] private float maxLightrangeMul = 10;
    [SerializeField] private float stepDmgMul = 1.1f;
    [SerializeField] private float stepLightrangeMul = 1.1f;

    private float startDmg;
    private float startLightrange;
    private DamagingField dmgField;
    private Light2D light2d;


    private void Start()
    {
        dmgField = GetComponentInChildren<DamagingField>();
        light2d = GetComponentInChildren<Light2D>();

        startDmg = dmgField.damage;
        startLightrange = light2d.pointLightOuterRadius;
    }

    public override void OnUpgradeReceived()
    {
        if (dmgField.damage / startDmg < maxDmgMul)
            dmgField.damage *= stepDmgMul;

        if (light2d.pointLightOuterRadius / startLightrange < maxLightrangeMul)
        {
            light2d.pointLightOuterRadius *= stepLightrangeMul;
        }
    }
}
