using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private float initialTheoreticalDifficulty;
    [SerializeField] private float theoreticalDifficultyIncrease;
    [SerializeField] private float difficultyCorridorPercent;
    [SerializeField] private int checkStep;
    [SerializeField] private float difficultyToCorrectionCoef;
    private TimeManager timeManager;


    private void Start() 
    {
        timeManager = FindObjectOfType<TimeManager>();
        StartCoroutine(Check(checkStep));
    }

    private IEnumerator Check(int step)
    {
        while (true)
        {
            float currentDifficulty = CalculateCurrentDifficulty();
            float theoreticalDifficulty = CalculateTheoreticalDifficulty(timeManager.currentGameTimeInMinutes);

            yield return new WaitForSeconds(step);

            float corridor = theoreticalDifficulty * difficultyCorridorPercent / 100;
            if (currentDifficulty - theoreticalDifficulty > corridor)
            {
                float amount = currentDifficulty - (theoreticalDifficulty + corridor);
                CorrectDifficulty(-amount);
            }
            if (currentDifficulty - theoreticalDifficulty < -corridor)
            {
                float amount = (theoreticalDifficulty - corridor) - currentDifficulty;
                CorrectDifficulty(amount);
            }
        
            // print("theoreticalDifficulty = " + theoreticalDifficulty.ToString());
            // print("currentDifficulty = " + currentDifficulty.ToString() + "\n");
        }
        
    }

    private float CalculateCurrentDifficulty()
    {
        float currentDifficulty = 0;
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            currentDifficulty += enemy.difficulty;
        }
        return currentDifficulty;
    }

    private float CalculateTheoreticalDifficulty(float x)
    {
        float theoreticalDifficulty = theoreticalDifficultyIncrease * x + initialTheoreticalDifficulty;
        return theoreticalDifficulty;
    }

    private void CorrectDifficulty(float difficultyAmount)
    {
        float correctionMult = 1 + difficultyAmount * difficultyToCorrectionCoef;
        SpawnInstantiator.main.difficultyMultiplier *= correctionMult;
        print("difficultyMultiplier = " + SpawnInstantiator.main.difficultyMultiplier.ToString());
    }
}
