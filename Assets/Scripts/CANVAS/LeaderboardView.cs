using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZSerializer;


public class LeaderboardView : PersistentMonoBehaviour
{
    [NonZSerialized] public Button pressbutt;
    [NonZSerialized] [SerializeField] private Text[] texts;
    public int[] scores;

    private void Start()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            texts[i].text = i.ToString() + ". " + scores[i].ToString();
        }
    }

    public void ButtonClick(int value)
    {
        scores[1] = value;
    }
}
