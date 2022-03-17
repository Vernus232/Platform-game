using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    // Start is called before the first frame update
    void Update()
    {
        camera.transform.position = new Vector3(player.transform.position.x, -1/5+player.transform.position.y, camera.transform.position.z);
    }
}
