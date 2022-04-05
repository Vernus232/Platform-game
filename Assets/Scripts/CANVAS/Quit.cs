using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Exit : MonoBehaviour
{
    public void Exitgame()
    {
        Debug.Log("exitgame");
        Application.Quit();
    }
}