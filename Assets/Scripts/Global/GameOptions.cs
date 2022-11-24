using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameOptions
{
    // Max lights on scene
    public static int maxLights = 50;
    // Max items on scene
    public static int maxItems = 100;
    // Time between checking items and lights
    public static float updateTime = 15;

    public static bool damagePopupsOn = true;
    public static bool ammoOnCrosshairOn = true;
    public static bool reloadOnCrosshairOn = true;
}
