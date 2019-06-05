using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorScript : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -9);
    }
}
