using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stabilazeRotation : MonoBehaviour
{


    private float speed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;

    }
}
