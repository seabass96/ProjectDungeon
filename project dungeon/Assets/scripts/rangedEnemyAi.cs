using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemyAi : MonoBehaviour
{
    private GameObject player;
    private float detectionRange = 8;
    private enum state { IDLE, SHOOT, RUNAWAY };
    private state action;
    private float xvelocity;
    private float yvelocity;
    private float maxHealth;
    private float offset = 4;
    private bool shoot = true;
    private float shootTimer = 2;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = gameObject.GetComponent<enemyScript>().getHealth();
        timer = shootTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(shoot == false)
        {
            setShootTimer();
        }

        aimProjectielSpawn();
        setState();
        shootOrRun();
        //updateAnimation();
    }

    private void setShootTimer()
    {
        if(timer <= 0)
        {
            timer = shootTimer;
            if(shoot == false)
            {
                shoot = true;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void aimProjectielSpawn()
    {
        float rotZ = Mathf.Atan2(player.transform.position.x - transform.GetChild(0).transform.position.x,
                                   player.transform.position.y - transform.GetChild(0).transform.position.y) * Mathf.Rad2Deg;
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, -rotZ + offset);
    }

    private void setState()
    {
        if (gameObject.GetComponent<enemyScript>().getHealth() > maxHealth * 0.1)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < detectionRange)
            {
                action = state.SHOOT;
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

    public void shootOrRun()
    {
        switch (action)
        {
            case state.SHOOT:
                if(Vector2.Distance(transform.position, player.transform.position) < 3.5f)
                {
                    RunFromPlayer();

                    if (shoot)
                    {
                        GameObject proj = Instantiate(transform.GetChild(0).gameObject.GetComponent<projectiles>().iceProjectile, transform.GetChild(0).GetChild(0).position, transform.GetChild(0).rotation);
                        proj.transform.rotation = transform.GetChild(0).rotation;
                        proj.GetComponent<projectileScript>().setDamage(gameObject.GetComponent<enemyScript>().getDamage());
                        shoot = false;
                    }
                }
                else
                {
                    if (shoot)
                    {
                        GameObject proj = Instantiate(transform.GetChild(0).gameObject.GetComponent<projectiles>().iceProjectile, transform.GetChild(0).GetChild(0).position, transform.GetChild(0).rotation);
                        proj.transform.rotation = transform.GetChild(0).rotation;
                        proj.GetComponent<projectileScript>().setDamage(gameObject.GetComponent<enemyScript>().getDamage());
                        shoot = false;
                    }
                }
                
                break;

            case state.RUNAWAY:
                RunFromPlayer();
                break;
        }
    }

    public void RunFromPlayer()
    {
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
    }

    public void updateAnimation()
    {
        switch (action)
        {
            case state.IDLE:
                gameObject.GetComponent<Animator>().SetTrigger("idle");
                break;

            case state.SHOOT:
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("room"))
        {
            action = state.SHOOT;
        }
    }
}
