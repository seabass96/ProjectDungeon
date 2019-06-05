using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMultiplyScript : MonoBehaviour
{
    private GameObject player;
    private float timer = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //TODO
        //runaway from player

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
        }
        gameObject.GetComponent<Animator>().SetTrigger("move");

       float xvelocity = 0;
       float yvelocity = 0;

        if (player.transform.position.x < transform.position.x)
        {
            xvelocity = 2;

        }
        else if (player.transform.position.x > transform.position.x)
        {
            xvelocity = -2;

        }
        else if (player.transform.position.x == transform.position.x)
        {
            xvelocity = 0;

        }

        if (player.transform.position.y < transform.position.y)
        {
            yvelocity = 2;

        }
        else if (player.transform.position.y > transform.position.y)
        {
            yvelocity = -2;

        }
        else if (player.transform.position.y == transform.position.y)
        {
            yvelocity = 0;
        }

        transform.position = new Vector3(transform.position.x + xvelocity * Time.deltaTime, transform.position.y + yvelocity * Time.deltaTime, transform.position.z);

        if (timer <= 0)
        {
           
            GameObject instance = Instantiate(GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().baseEnemy, transform.position,  Quaternion.identity);
            instance.GetComponent<enemyScript>().Enemy = GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().demonEnemies[1];
            instance.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
        //timercountdown to grow
    }
}
