using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}