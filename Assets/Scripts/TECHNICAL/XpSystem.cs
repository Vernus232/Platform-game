using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpSystem : MonoBehaviour
{
    public static XpSystem main;
    public float[] levelCaps;
    public int currentLevel;
    [HideInInspector] public float xp;

    private void Start()
    {
        main = this;
    }

    public void OnScoreChanged()
    {
        xp = ScoreSystem.main.Score / 100;
    }
}
