using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Linq;

public static class Saver
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
            string[] sliced_string_scores = string_scores.Take(string_scores.Length - 1).ToArray<string>();
            int[] scores = Array.ConvertAll(sliced_string_scores, int.Parse);
            
            return scores;
        }
    }
}
