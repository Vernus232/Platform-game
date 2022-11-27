using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZSerializer;


public class LeaderboardView : PersistentMonoBehaviour
{
    [NonZSerialized] [SerializeField] private Text[] texts;
    [SerializeField] private int[] scores;

    private void Start()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            texts[i].text = i.ToString() + ". " + scores[i].ToString();
        }
    }
}
