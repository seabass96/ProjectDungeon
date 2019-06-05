using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFarthHurricain : MonoBehaviour
{
    private GameObject player;
    private enum state { PATROL, RUNFART, RUNCHARGESETUP, RUNCHARGE, IDLE };
    private state action;
    private float xvel = -1;
    private float yvel = -1;
    private float maxHealth;
    private float range = 0.2f;
    private bool shoot = true;
    private float offset = 4;
    private GameObject tempParent;
    private float duration = 0.5f;
    private int OldState = 0;
    private bool isFarting = false;
    private float isfartingTimer = 4;

    private int UpOrDown;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = gameObject.GetComponent<enemyScript>().getHealth();
        UpOrDown = Random.Range(0, 2);
        action = state.PATROL;
        tempParent = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        isFartingTimerFunc();
        aimProjectielSpawn();
        actionsToTake();
        updateAnim();
    }

    private void isFartingTimerFunc()
    {
        if(isFarting)
        {
            if (isfartingTimer <= 0)
            {
                isFarting = false;
                isfartingTimer = 4;
            }
            else
            {
                isfartingTimer -= Time.deltaTime;
            }
        }      
    }

    private void actionsToTake()
    {
        switch (action)
        {
            case state.PATROL:
                if(Vector2.Distance(transform.position, player.transform.position) > 3)
                {
                    switch (UpOrDown)
                    {
                        case 0:
                            patrolVertical();
                            break;

                        case 1:
                            patrolHorizontal();
                            break;
                    }
                }
                else
                {
                    action = state.RUNCHARGESETUP;
                }
                
                break;

            case state.RUNFART:
                if (Vector2.Distance(transform.position, player.transform.position) < 3.5f)
                {
                    RunFromPlayer(); 
                }
                else
                {
                    if(isFarting)
                    {
                        action = state.RUNCHARGESETUP;
                    }
                    else
                    {
                        StartCoroutine(fartShot());
                        action = state.RUNCHARGESETUP;
                    }
                    
                }
                break;

            case state.RUNCHARGE:
                RunCharge();
                break;

            case state.RUNCHARGESETUP:
                RunChargeSetUp();
                break;
        }

        if(action == state.IDLE)
        {
            StartCoroutine(idleToPatrole());
        }
    }

    private IEnumerator idleToPatrole()
    {
        yield return new WaitForSeconds(duration+1);
        action = state.PATROL;
    }

    private void RunCharge()
    {
        if(player.transform.position.x < transform.position.x)
        {
            xvel = -2.5f;
            //transform.position = new Vector3(transform.position.x - 3 * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            xvel = 2.5f;
            //transform.position = new Vector3(transform.position.x  + 3 * Time.deltaTime, transform.position.y, transform.position.z);
        }

        if (player.transform.position.y < transform.position.y)
        {
            yvel = -0.5f;

        }
        else if (player.transform.position.y > transform.position.y)
        {
            yvel = 0.5f;

        }

        transform.position = new Vector3(transform.position.x + xvel * Time.deltaTime, transform.position.y + yvel * Time.deltaTime, transform.position.z);


        bool hitleft = false;
        Vector3 endPosLeft = transform.position + new Vector3(-range, 0, 0) * transform.localScale.x;
        hitleft = Physics2D.Linecast(transform.position, endPosLeft, 1 << LayerMask.NameToLayer("room"));
        if(hitleft == false)
        {
            hitleft = Physics2D.Linecast(transform.position, endPosLeft, 1 << LayerMask.NameToLayer("player"));
        }
        Debug.DrawLine(transform.position, endPosLeft, Color.red);

        bool hitRight = false;
        Vector3 endPosRight = transform.position + new Vector3(range, 0, 0) * transform.localScale.x;
        hitRight = Physics2D.Linecast(transform.position, endPosRight, 1 << LayerMask.NameToLayer("room"));
        if(hitRight == false)
        {
            hitRight = Physics2D.Linecast(transform.position, endPosRight, 1 << LayerMask.NameToLayer("player"));
        }
        Debug.DrawLine(transform.position, endPosRight, Color.red);

        bool hitUp = false;
        Vector3 endPosUp = transform.position + new Vector3(0, -range, 0) * transform.localScale.x;
        hitUp = Physics2D.Linecast(transform.position, endPosUp, 1 << LayerMask.NameToLayer("room"));
        if(hitUp == false)
        {
            hitUp = Physics2D.Linecast(transform.position, endPosUp, 1 << LayerMask.NameToLayer("player"));
        } 
        Debug.DrawLine(transform.position, endPosUp, Color.red);

        bool hitDown = false;
        Vector3 endPosDown = transform.position + new Vector3(0, range, 0) * transform.localScale.x;
        hitDown = Physics2D.Linecast(transform.position, endPosDown, 1 << LayerMask.NameToLayer("room"));
        if(hitDown == false)
        {
            hitDown = Physics2D.Linecast(transform.position, endPosDown, 1 << LayerMask.NameToLayer("player"));
        }
        
        Debug.DrawLine(transform.position, endPosDown, Color.red);

        if(hitDown || hitleft || hitUp || hitDown)
        {
            //int rand = Random.Range(0, 1);
            
            if(OldState == 0)
            {
               OldState = 1;
            }
            else if(OldState == 1)
            {
                OldState = 0;
            }
          


           

            switch (OldState)
            {
                case 0:
                    action = state.RUNCHARGESETUP;
                    OldState = 0;
                    break;

                case 1:
                    action = state.RUNFART;
                    OldState = 1;
                    break;
            }
        }

    }

    private void RunChargeSetUp()
    {
        xvel = 0;
        yvel = 0;

        if(Mathf.Abs( player.transform.position.x - transform.position.x) < 3)
        {
            if (player.transform.position.x < transform.position.x)
            {
                xvel = 1;

            }
            else if (player.transform.position.x > transform.position.x)
            {
                xvel = -1;

            }
            else if (player.transform.position.x == transform.position.x)
            {
                xvel = 0;

            }
        }
       

        if (player.transform.position.y > transform.position.y)
        {
            yvel = 1;

        }
        else if (player.transform.position.y < transform.position.y)
        {
            yvel = -1;

        }

        if (Mathf.Abs(player.transform.position.y - transform.position.y) < 1)
        {
            //isFarting = false;
            action = state.RUNCHARGE;
        }

        transform.position = new Vector3(transform.position.x + xvel * Time.deltaTime, transform.position.y + yvel * Time.deltaTime, transform.position.z);
    }

    private void patrolHorizontal()
    {
        bool hitleft = false;
        Vector3 endPosLeft = transform.position + new Vector3(-range, 0, 0) * transform.localScale.x;
        hitleft = Physics2D.Linecast(transform.position, endPosLeft, 1 << LayerMask.NameToLayer("room"));
        Debug.DrawLine(transform.position, endPosLeft, Color.red);

        bool hitRight = false;
        Vector3 endPosRight = transform.position + new Vector3(range, 0, 0) * transform.localScale.x;
        hitRight = Physics2D.Linecast(transform.position, endPosRight, 1 << LayerMask.NameToLayer("room"));
        Debug.DrawLine(transform.position, endPosLeft, Color.red);

        if (hitleft)
        {
            xvel = 1;
        }
        else if(hitRight)
        {
            xvel = -1;
        }

        transform.position = new Vector3(transform.position.x + xvel * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void patrolVertical()
    {
        bool hitUp = false;
        Vector3 endPosUp = transform.position + new Vector3( 0, -range, 0) * transform.localScale.x;
        hitUp = Physics2D.Linecast(transform.position, endPosUp, 1 << LayerMask.NameToLayer("room"));
        Debug.DrawLine(transform.position, endPosUp, Color.red);

        bool hitDown = false;
        Vector3 endPosDown = transform.position + new Vector3( 0, range, 0) * transform.localScale.x;
        hitDown = Physics2D.Linecast(transform.position, endPosDown, 1 << LayerMask.NameToLayer("room"));
        Debug.DrawLine(transform.position, endPosDown, Color.red);

        if (hitUp)
        {
            yvel = 1;
        }
        else if (hitDown)
        {
            yvel = -1;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + yvel * Time.deltaTime, transform.position.z);
    }

    private void RunFromPlayer()
    {
        xvel = 0;
        yvel = 0;

        if (player.transform.position.x < transform.position.x)
        {
            xvel = 1;

        }
        else if (player.transform.position.x > transform.position.x)
        {
            xvel = -1;

        }
        else if (player.transform.position.x == transform.position.x)
        {
            xvel = 0;

        }

        if (player.transform.position.y < transform.position.y)
        {
            yvel = 1;

        }
        else if (player.transform.position.y > transform.position.y)
        {
            yvel = -1;

        }
        else if (player.transform.position.y == transform.position.y)
        {
            yvel = 0;
        }

        transform.position = new Vector3(transform.position.x + xvel * Time.deltaTime, transform.position.y + yvel * Time.deltaTime, transform.position.z);
    }

    private void aimProjectielSpawn()
    {
        float rotZ = Mathf.Atan2(player.transform.position.x - transform.GetChild(0).transform.position.x,
                                   player.transform.position.y - transform.GetChild(0).transform.position.y) * Mathf.Rad2Deg;
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, -rotZ + offset);
    }

    private IEnumerator fartShot()
    {
        isFarting = true;
        tempParent.transform.parent = null;
        transform.parent = tempParent.transform;
        gameObject.GetComponent<Animator>().SetBool("isFarting", true);

        float time = 0;
        float strength = 0.05f;
        Vector3 pos = tempParent.transform.position;
        
        while (time < duration)
        {
            float x = Random.Range(-strength, strength);
            transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
            time += Time.deltaTime;
            yield return null;
            transform.position = pos;
        }

        transform.position = pos;
        gameObject.GetComponent<Animator>().SetBool("isFarting", false);

        if(shoot)
        {
            shoot = false;
            GameObject proj = Instantiate(transform.GetChild(0).gameObject.GetComponent<projectiles>().fartnado, transform.GetChild(0).GetChild(0).position, transform.GetChild(0).rotation);
            proj.transform.rotation = transform.GetChild(0).rotation;
            proj.GetComponent<projectileScript>().setDamage(gameObject.GetComponent<enemyScript>().getDamage());
            
        }

        transform.parent = null;
        tempParent.transform.parent = transform;
        shoot = true;

    }

    private void updateAnim()
    {
        switch (action)
        {
            case state.PATROL:
                gameObject.GetComponent<Animator>().SetTrigger("move");
                if(xvel > 0)
                {
                    transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
                }
                else if(xvel < 0)
                {
                    transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
                }
                break;
            case state.RUNFART:
                break;
            case state.RUNCHARGESETUP:
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
            case state.RUNCHARGE:
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
            case state.IDLE:
                gameObject.GetComponent<Animator>().SetTrigger("idle");
                break;
        }
    }
}
