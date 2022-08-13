using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeProjectile : VanishingProjectile
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpgradableEntity[] upgradableEntities = collision.gameObject.GetComponents<UpgradableEntity>();
        foreach (UpgradableEntity upgradableEntity in upgradableEntities)
        {
            upgradableEntity.OnUpgradeReceived();
        }
        Destroy(gameObject);
    }

}
