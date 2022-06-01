using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSystem : MonoBehaviour
{
    public int currentLevel = 0;

    [SerializeField] private int maxLevel;
    [SerializeField] private float firstScoreForNextLvl;
    [SerializeField] private float scoreForNextLvlIncreaseCoef;
    [SerializeField] private float[] scoreForNextLvl;
    [SerializeField] private UnityEvent[] lvlBonuses;

    [HideInInspector] public static LevelSystem main;
    private float scoreForCurrLevel = 0;


    private void Start()
    {
        main = this;

        // Заполнить levelCaps
        scoreForNextLvl = new float[maxLevel];
        for (int i = 0; i < scoreForNextLvl.Length; i++)
        {
            float Function(int x, float m, float a)
            {
                return m * Mathf.Pow(2, x) + a;
            }

            scoreForNextLvl[i] = Function(i, scoreForNextLvlIncreaseCoef, firstScoreForNextLvl);
        }
    }

    public void OnScoreChanged()
    {
        if (ScoreSystem.main.Score >= scoreForNextLvl[currentLevel])
        {
            LevelUp();
        }

        float levelSliderValue = (ScoreSystem.main.Score - scoreForCurrLevel) / 
                                 (scoreForNextLvl[currentLevel] - scoreForCurrLevel);
        LevelView.main.UpdateUI(levelSliderValue);
    }

    private void LevelUp()
    {
        GivePlayerLvlBonuses(currentLevel);

        scoreForCurrLevel = scoreForNextLvl[currentLevel];
        currentLevel++;
    }

    private void GivePlayerLvlBonuses(int lvl)
    {
        lvlBonuses[lvl].Invoke();
    }


}
