using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaverV2 : MonoBehaviour
{
    [SerializeField] private static int[] scores;
    private static string path = "D:/Exponenta-Data/Scores.txt";


    public static void SaveScores()
    {
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(Mathf.RoundToInt(ScoreSystem.main.Score) + ";");
            }	
        }
        else
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Write(Mathf.RoundToInt(ScoreSystem.main.Score) + ";");
            }
            print(ScoreSystem.main.Score);
            print("Saved successfully!");
        }
    }

    public static void LoadScores()
    {
        // Open the file to read from.
        using (StreamReader sr = File.OpenText(path))
        {
            string s = "";
            for (int i = 0; i < scores.Length; i++)
            {
                while ((s = sr.ReadLine()) != ";")
                {
                    int.TryParse(s, out scores[i]);
                    print(int.TryParse(s, out scores[i]));
                }
            }
        }
    }
}
