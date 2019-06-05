using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeScript : MonoBehaviour
{
    private float timer = 3;
    private bool stick;

    private void Update()
    {
        if(timer <= 0)
        {
            stick = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<characterInput>().START();
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = Color.white;
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (stick)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<characterInput>().STOP();
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = Color.green;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            stick = true;
           
        }
    }


}
