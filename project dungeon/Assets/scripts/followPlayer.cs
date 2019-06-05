using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    private Transform player;
    public float followSpeed;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, player.position, followSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -50);
    }
}
