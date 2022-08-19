using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public void Spawn()
    {
        GameObject instantiatedObj = Instantiate(prefab, transform);
        instantiatedObj.SetActive(true);
    }
}
