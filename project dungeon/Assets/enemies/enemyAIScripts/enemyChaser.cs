using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChaser : MonoBehaviour
{
    private GameObject player;
    private float detectionRange = 5;
    private enum state { IDLE, CHASE, RUNAWAY };
    private state action;
    private float xvelocity;
    private float yvelocity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        setState();
        ChaseOrRun();
        updateAnimation();
    }

    private void setState()
    {
        if (gameObject.GetComponent<enemyScript>().getHealth() > 2)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < detectionRange)
            {
                action = state.CHASE;
            }
            else
            {
                action = state.IDLE;
            }
        }
        else
        {
            action = state.RUNAWAY;
        }
    }

    private void ChaseOrRun()
    {
        switch (action)
        {
            case state.IDLE:
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                break;

            case state.CHASE:
                xvelocity = 0;
                yvelocity = 0;

                if (player.transform.position.x + 0.01f < transform.position.x)
                {
                    xvelocity = -1;
                }
                else if (player.transform.position.x - 0.01f > transform.position.x)
                {
                    xvelocity = 1;
                }
                else if (player.transform.position.x == transform.position.x)
                {
                    xvelocity = 0;
                }


                if (player.transform.position.y < transform.position.y)
                {
                    yvelocity = -1;
                }
                else if (player.transform.position.y > transform.position.y)
                {
                    yvelocity = 1;
                }
                else if (player.transform.position.y == transform.position.y)
                {
                    yvelocity = 0;
                }

                transform.position = new Vector3(transform.position.x + xvelocity * Time.deltaTime, transform.position.y + yvelocity * Time.deltaTime, transform.position.z);
                break;

            case state.RUNAWAY:
                xvelocity = 0;
                yvelocity = 0;

                if (player.transform.position.x < transform.position.x)
                {
                    xvelocity = 1;

                }
                else if (player.transform.position.x > transform.position.x)
                {
                    xvelocity = -1;

                }
                else if (player.transform.position.x == transform.position.x)
                {
                    xvelocity = 0;

                }

                if (player.transform.position.y < transform.position.y)
                {
                    yvelocity = 1;

                }
                else if (player.transform.position.y > transform.position.y)
                {
                    yvelocity = -1;

                }
                else if (player.transform.position.y == transform.position.y)
                {
                    yvelocity = 0;
                }

                transform.position = new Vector3(transform.position.x + xvelocity * Time.deltaTime, transform.position.y + yvelocity * Time.deltaTime, transform.position.z);
                
                break;

        }
    }

    private void updateAnimation()
    {
        

        switch (action)
        {
            case state.IDLE:
                gameObject.GetComponent<Animator>().SetTrigger("idle");
                break;

            case state.CHASE:
                if (player.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
                }
                else if (player.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
                }
                gameObject.GetComponent<Animator>().SetTrigger("move");
                break;

            case state.RUNAWAY:
                if (player.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
                }
                else if (player.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
                }
                gameObject.GetComponent<Animator>().SetTrigger("move");
                break;
            
        }


    }
}
