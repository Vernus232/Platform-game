using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsManager : MonoBehaviour
{
    private PerformanceManager performanceManager;
    private Slider crosshairReloadingSlider;
    private Text ammoOnCrosshairText;

    // Start is called before the first frame update
    void Start()
    {
        // Performance manager
        performanceManager = FindObjectOfType<PerformanceManager>();
        performanceManager.maxLights = GameOptions.maxLights;
        performanceManager.maxItems = GameOptions.maxItems;
        performanceManager.maxEnemies = GameOptions.maxEnemies;
        performanceManager.updateTime = GameOptions.updateTime;
        performanceManager.radiusOfDelete = GameOptions.circleRadius;

        // Crosshair prikolchiki
        crosshairReloadingSlider = FindObjectOfType<SimpleCrosshair>().GetComponentInChildren<Slider>();
        ammoOnCrosshairText = FindObjectOfType<SimpleCrosshair>().GetComponentInChildren<Text>();

        if (GameOptions.reloadOnCrosshairOn == false)
            crosshairReloadingSlider.gameObject.SetActive(false);
        if (GameOptions.ammoOnCrosshairOn == false)
            ammoOnCrosshairText.enabled = false;
    }
}
