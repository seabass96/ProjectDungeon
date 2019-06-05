using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreaveFireAtPlayer : MonoBehaviour
{
    private bool canBreathFire = true;
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance( GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 1.5f)
        {
            if(canBreathFire)
            {
                
            }
           
        }
    }

    //TODO finish this seeeeb

    //private IEnumerator shakeThenFireBlast()
    //{
    //    gameObject.GetComponent<enemyChaser>().enabled = false;
    //    GameObject instance = Instantiate(transform.GetChild(0).gameObject.GetComponent<projectiles>().fireBlast, transform.position, Quaternion.identity);
    //    if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
    //    {
    //        instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
    //    }
    //    else if (GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x)
    //    {
    //        instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    //    }
    //    canBreathFire = false;
    //}


}
