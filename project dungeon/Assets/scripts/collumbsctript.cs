using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collumbsctript : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z - 1);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + 1);
        }
    }
}
