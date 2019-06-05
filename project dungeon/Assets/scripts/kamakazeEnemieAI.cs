using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamakazeEnemieAI : MonoBehaviour
{
    private GameObject player;
    private float detectionRange = 2;
    private enum state { RANDOMPATROL, RUNTOFIRE, RUNTOPLAYER, IDOL, EXPLODE };
    private state action;
    private float xvel = 1f;
    private float yvel = 1f;
    private float range = 0.1f;
    private bool hasHitWall = false;
    private GameObject flame = null;
    private GameObject tempParent;
    private float duration = 1.5f;

    //private List<GameObject> rooms;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        randomVelocity();
        action = state.RANDOMPATROL;
        tempParent = transform.GetChild(1).gameObject;

    }

    private void Update()
    {
        takeAction();
    }

    private void randomVelocity()
    {
        
        xvel = Random.Range(-1f, 1f);
        yvel = Random.Range(-1f, 1f);
        if(xvel < 0.05f && xvel > -0.05f)
        {
            xvel = 1;
        }

        if (yvel < 0.05f && yvel > -0.05f)
        {
            yvel = 1;
        }

    }

    private void takeAction()
    {

        switch (action)
        {
            case state.RANDOMPATROL:
                randomPatrole();
                break;
            case state.RUNTOFIRE:
                runToFireAction();
                break;
            case state.RUNTOPLAYER:
                runToPlayer();
                break;

            case state.EXPLODE:
                StartCoroutine(shakeAndExplode());
                break;

            case state.IDOL:
                break;

        }
    }

    private IEnumerator shakeAndExplode()
    {
        tempParent.transform.parent = null;
        transform.parent = tempParent.transform;

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
        //yield return new WaitForSeconds(duration);

        //explode here
        GameObject explosisve =  Instantiate(gameObject.transform.GetChild(0).GetComponent<projectiles>().circleExplode, transform.position, Quaternion.identity);
        explosisve.GetComponent<circleExplodeScript>().setDamage(transform.GetComponent<enemyScript>().getDamage());
        Destroy(gameObject);

        transform.parent = null;
        tempParent.transform.parent = transform;
    }

    private void runToPlayer()
    {
        xvel = 0;
        yvel = 0;

        if (player.transform.position.x + 0.01f < transform.position.x)
        {
            xvel = -2;
        }
        else if (player.transform.position.x - 0.01f > transform.position.x)
        {
            xvel = 2;
        }
        else if (player.transform.position.x == transform.position.x)
        {
            xvel = 0;
        }


        if (player.transform.position.y < transform.position.y)
        {
            yvel = -2;
        }
        else if (player.transform.position.y > transform.position.y)
        {
            yvel = 2;
        }
        else if (player.transform.position.y == transform.position.y)
        {
            yvel = 0;
        }

        transform.position = new Vector3(transform.position.x + xvel * Time.deltaTime, transform.position.y + yvel * Time.deltaTime, transform.position.z);

        if(Vector2.Distance(transform.position, player.transform.position) < 1f)
        {
            action = state.EXPLODE;
        }
    }

    private void randomPatrole()
    {
        bool hitleft = false;
        Vector3 endPosLeft = transform.position + new Vector3(-range, 0, 0) * transform.localScale.x;
        hitleft = Physics2D.Linecast(transform.position, endPosLeft, 1 << LayerMask.NameToLayer("room"));
        Debug.DrawLine(transform.position, endPosLeft, Color.red);

        bool hitRight = false;
        Vector3 endPosRight = transform.position + new Vector3(range, 0, 0) * transform.localScale.x;
        hitRight = Physics2D.Linecast(transform.position, endPosRight, 1 << LayerMask.NameToLayer("room"));
        Debug.DrawLine(transform.position, endPosRight, Color.red);

        bool hitUp = false;
        Vector3 endPosUp = transform.position + new Vector3(0, -range, 0) * transform.localScale.x;
        hitUp = Physics2D.Linecast(transform.position, endPosUp, 1 << LayerMask.NameToLayer("room"));
        Debug.DrawLine(transform.position, endPosUp, Color.red);

        bool hitDown = false;
        Vector3 endPosDown = transform.position + new Vector3(0, range, 0) * transform.localScale.x;
        hitDown = Physics2D.Linecast(transform.position, endPosDown, 1 << LayerMask.NameToLayer("room"));
        Debug.DrawLine(transform.position, endPosDown, Color.red);

        if(hitDown || hitleft || hitUp || hitRight)
        {
            hasHitWall = true;
            
        }
        
        if(Vector2.Distance(transform.position, findClosestCenter()) < 1 && hasHitWall)
        {
            hasHitWall = false;
            randomVelocity();
        }

        if (hasHitWall == false)
        {
            transform.position = new Vector3(transform.position.x + xvel * Time.deltaTime, transform.position.y + yvel * Time.deltaTime, transform.position.z);
        }
        else
        {
            if (findClosestCenter().x > transform.position.x)
            {
                transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
            }
            else if (findClosestCenter().x < transform.position.x)
            {
                transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
            }

            xvel = 0;
            yvel = 0;

            if (findClosestCenter().x + 0.01f < transform.position.x)
            {
                xvel = -1;
            }
            else if (findClosestCenter().x - 0.01f > transform.position.x)
            {
                xvel = 1;
            }
            else if (findClosestCenter().x == transform.position.x)
            {
                xvel = 0;
            }


            if (findClosestCenter().y < transform.position.y)
            {
                yvel = -1;
            }
            else if (findClosestCenter().y > transform.position.y)
            {
                yvel = 1;
            }
            else if (findClosestCenter().y == transform.position.y)
            {
                yvel = 0;
            }

            transform.position = new Vector3(transform.position.x + xvel * Time.deltaTime, transform.position.y + yvel * Time.deltaTime, transform.position.z);

        }

        runToFireSetter();
    }

    private void runToFireSetter()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < detectionRange)
        {
            action = state.RUNTOFIRE;
        }
    }

    public Vector3 findClosestFire()
    {
        GameObject[] fires = GameObject.FindGameObjectsWithTag("fire");
        GameObject closest = fires[0];

        foreach (GameObject item in fires)
        {
            if (Vector2.Distance(transform.position, item.transform.position) < Vector2.Distance(transform.position, closest.transform.position))
            {
                closest = item;
            }
        }

        return closest.transform.position;
    }

    public Vector2 findClosestCenter()
    {
        
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("room");
        GameObject closest = rooms[0];

        foreach (GameObject item in rooms)
        {
            if (Vector2.Distance(transform.position, item.transform.position) < Vector2.Distance(transform.position, closest.transform.position))
            {
                closest = item;
            }
        }

        return closest.transform.position;
    }

    private void runToFireAction()
    {
        if (findClosestFire().x > transform.position.x)
        {
            transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
        }
        else if (findClosestFire().x < transform.position.x)
        {
            transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
        }

        xvel = 0;
        yvel = 0;

        if (findClosestFire().x + 0.01f < transform.position.x)
        {
            xvel = -2;
        }
        else if (findClosestFire().x - 0.01f > transform.position.x)
        {
            xvel = 2;
        }
        else if (findClosestFire().x == transform.position.x)
        {
            xvel = 0;
        }


        if (findClosestFire().y < transform.position.y)
        {
            yvel = -2;
        }
        else if (findClosestFire().y > transform.position.y)
        {
            yvel = 2;
        }
        else if (findClosestFire().y == transform.position.y)
        {
            yvel = 0;
        }

        transform.position = new Vector3(transform.position.x + xvel * Time.deltaTime, transform.position.y + yvel * Time.deltaTime, transform.position.z);



        if (Vector2.Distance(transform.position, findClosestFire()) < 0.3f)
        {
            if(flame == null)
            {
                flame = Instantiate(gameObject.transform.GetChild(0).GetComponent<projectiles>().flame, transform.position, Quaternion.identity);
                flame.transform.parent = gameObject.transform;
                flame.transform.position = flame.transform.position + new Vector3(0, 0.08f, -1);
               //gameObject.GetComponent<SpriteRenderer>().color = new Color((255 / 255), (234 / 255), (0 / 255), (150 / 255));
            }
            action = state.RUNTOPLAYER;
        }
    }

    private void updateAnim()
    {
        switch (action)
        {
            case state.RANDOMPATROL:
                gameObject.GetComponent<Animator>().SetTrigger("move");
                break;
            case state.RUNTOFIRE:
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
            case state.RUNTOPLAYER:
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
            case state.IDOL:
                gameObject.GetComponent<Animator>().SetTrigger("idle");
                break;
            case state.EXPLODE:
                if (player.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
                }
                else if (player.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
                }
                gameObject.GetComponent<Animator>().SetTrigger("idle");
                break;
        }
    }
}
