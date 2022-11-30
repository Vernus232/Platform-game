using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class SaverV2
{
    public static int[] scores;
    private static string path = Application.persistentDataPath + "/Scores.txt";


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
        }
    }

    public static int[] LoadScores()
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
                    Debug.Log(int.TryParse(s, out scores[i]));
                }
            }
            return scores;
        }
    }
}
