﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staySameWay : MonoBehaviour
{
    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = scale;
    }
}
