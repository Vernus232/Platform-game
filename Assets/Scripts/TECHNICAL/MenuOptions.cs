using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuOptions : MonoBehaviour
{
    [SerializeField] private Slider lightsSlider;
    [SerializeField] private Slider itemsSlider;
    [SerializeField] private Slider enemiesSlider;
    [SerializeField] private Slider cycleSlider;
    [SerializeField] private Slider radiusSlider;
    [SerializeField] private Toggle popupsToggle;
    [SerializeField] private Toggle ammoToggle;
    [SerializeField] private Toggle reloadToggle;
    [SerializeField] private Toggle particlesToggle;

    private void Start()
    {
        Cursor.visible = true;
        // Updating options on scene launch
        lightsSlider.value = GameOptions.maxLights;
        itemsSlider.value = GameOptions.maxItems;
        enemiesSlider.value = GameOptions.maxEnemies;
        cycleSlider.value = GameOptions.updateTime;
        radiusSlider.value = GameOptions.circleRadius;

        popupsToggle.isOn = GameOptions.damagePopupsOn;
        ammoToggle.isOn = GameOptions.ammoOnCrosshairOn;
        reloadToggle.isOn = GameOptions.reloadOnCrosshairOn;
        particlesToggle.isOn = GameOptions.Particles;
    }

#region Methods
    public void ChangeMaxLights()
    {
        GameOptions.maxLights = Mathf.RoundToInt(lightsSlider.value);
    }
    public void ChangeMaxItems()
    {
        GameOptions.maxItems = Mathf.RoundToInt(itemsSlider.value);
    }
    public void ChangeMaxEnemies()
    {
        GameOptions.maxEnemies = Mathf.RoundToInt(enemiesSlider.value);
    }
    public void ChangeCheckCycle()
    {
        GameOptions.updateTime = Mathf.RoundToInt(cycleSlider.value);
    }
    public void ChangeCircleRadius()
    {
        GameOptions.circleRadius = Mathf.RoundToInt(radiusSlider.value);
    }
    public void ChangePopups()
    {
        GameOptions.damagePopupsOn = popupsToggle.isOn;
    }
    public void ChangeAmmoOnCrosshair()
    {
        GameOptions.ammoOnCrosshairOn = ammoToggle.isOn;
    }
    public void ChangeReloadOnCrosshair()
    {
        GameOptions.reloadOnCrosshairOn = reloadToggle.isOn;
    }
    public void ChangeParticles()
    {
        GameOptions.Particles = particlesToggle.isOn;
    }
#endregion
}
