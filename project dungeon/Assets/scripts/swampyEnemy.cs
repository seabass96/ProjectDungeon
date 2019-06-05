using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swampyEnemy : MonoBehaviour
{
    private GameObject player;
    private enum state { PATROL, RUNFART, RUNCHARGESETUP, RUNCHARGE, IDLE };
    private state action;
    private float xvel = 0;
    private float yvel = 0;
    bool isSpawnignSlime = false;
    private float timer = 0.5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");     
    }

    private void Update()
    {
        if(timer<=0)
        {
            timer = 0.1f;
            Instantiate(transform.GetChild(0).GetComponent<projectiles>().slimeTrail, transform.position, Quaternion.Euler(new Vector3(0,0,Random.Range(-180, 180))))
                .transform.position = transform.position + new Vector3(0, 0, 2);
        }
        else { timer -= Time.deltaTime; }
    }

   

}
