using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelStopperScript : MonoBehaviour
{
    public bool open = false;
    public GameObject[] pillarEffects;
    public GameObject bubble;

    private void Update()
    {
        if(open)
        {
            foreach (GameObject item in pillarEffects)
            {
                item.SetActive(true);    
            }

            bubble.SetActive(false);
        }
    }
}
