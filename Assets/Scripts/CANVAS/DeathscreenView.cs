using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathscreenView : MonoBehaviour
{
    [SerializeField] private float interactionBlockTime;
    [SerializeField] private Text text;
    [SerializeField] private GameObject interactionBlock;

    [HideInInspector] public static DeathscreenView main;


    private void Start()
    {
        main = this;
    }

    private void OnEnable()
    {
        text.text = "Your Score: " + ScoreSystem.main.Score.ToString("000000");

        interactionBlock.SetActive(true);
        StartCoroutine(DisableBlockingImage());
    }

    IEnumerator DisableBlockingImage()
    {
        yield return new WaitForSeconds(interactionBlockTime);

        interactionBlock.SetActive(false);
    }

    public void SaveScore()
    {
        SaverV2.SaveScores();
    }
}
