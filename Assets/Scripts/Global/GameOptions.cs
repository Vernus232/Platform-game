using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameOptions
{
#region Ingame
    // Max lights on scene
    public static int maxLights = 50;
    // Max items on scene
    public static int maxItems = 100;
    // Max enemies on scene
    public static int maxEnemies = 50;

    // Time between checking items and lights
    public static float updateTime = 5;
    // Radius of circle where enemies do not destroy
    public static int circleRadius = 20;


    public static bool damagePopupsOn = true;
    public static bool ammoOnCrosshairOn = true;
    public static bool reloadOnCrosshairOn = true;
    public static bool Particles = true;
#endregion

#region Graphics
    // Game framerate (fps)
    public static int Framerate = -1;
    public static int FpsDropdownValue;
#endregion
}
