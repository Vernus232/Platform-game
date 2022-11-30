using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class SaverV2
{
    private static string path = Application.persistentDataPath + "/Scores.txt";


    public static void SaveScore()
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
            string line = sr.ReadLine();
            string[] string_scores = line.Split(';');
            int[] scores = Array.ConvertAll(string_scores, s => int.Parse(s));
            return scores;
        }
    }
}
