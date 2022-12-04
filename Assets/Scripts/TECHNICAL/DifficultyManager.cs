using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public float initialTheoreticalDifficulty;
    public float theoreticalDifficultyIncrease;
    public float difficultyCorridorSize;
    public int checkStep;

    private void Start() 
    {
        StartCoroutine(Check(checkStep));
    }

    private IEnumerator Check(int step)
    {
        while (true)
        {
            float currentDifficulty = CalculateCurrentDifficulty();
            float theoreticalDifficulty = CalculateTheoreticalDifficulty(TimeManager.main.currentGameTime);

            yield return new WaitForSeconds(step);

            if (currentDifficulty - theoreticalDifficulty > difficultyCorridorSize)
            {
                float amount = currentDifficulty - (theoreticalDifficulty + difficultyCorridorSize);
                CorrectDifficulty(-amount);
            }
            if (currentDifficulty - theoreticalDifficulty < -difficultyCorridorSize)
            {
                float amount = (theoreticalDifficulty - difficultyCorridorSize) - currentDifficulty;
                CorrectDifficulty(amount);
            }
        }
        
    }

    private float CalculateCurrentDifficulty()
    {
        float currentDifficulty = 0;
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            currentDifficulty += 1;
        }
        return currentDifficulty;
    }

    private float CalculateTheoreticalDifficulty(float x)
    {
        float theoreticalDifficulty = theoreticalDifficultyIncrease * x + initialTheoreticalDifficulty;
        return theoreticalDifficulty;
    }

    private void CorrectDifficulty(float amount)
    {
        theoreticalDifficultyIncrease += amount;
    }
}
