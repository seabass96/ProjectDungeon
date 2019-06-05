using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    //                            0      1      2    3      4
    public enum directionsEnum { ZERO, BOTTOM, TOP, LEFT, RIGHT };
    public int openingDirection;
    private roomTemplates template;
    public bool spawned = false;
    public float waitTime = 4f;

    private void Start()
    {
        template = GameObject.FindGameObjectWithTag("rooms").GetComponent<roomTemplates>();
        Invoke("spawn", 0.1f);
        Destroy(gameObject, waitTime);
    }


    private void spawn()
    {
        if(spawned == false)
        {
            if (openingDirection == (int)directionsEnum.BOTTOM)
            {
                Instantiate(template.bottomRooms[Random.Range(0, template.bottomRooms.Length)], transform.position, Quaternion.identity);
            }
            else if (openingDirection == (int)directionsEnum.TOP)
            {
                Instantiate(template.topRooms[Random.Range(0, template.topRooms.Length)], transform.position, Quaternion.identity);
            }
            else if (openingDirection == (int)directionsEnum.LEFT)
            {
                Instantiate(template.leftRooms[Random.Range(0, template.leftRooms.Length)], transform.position, Quaternion.identity);
            }
            else if (openingDirection == (int)directionsEnum.RIGHT)
            {
                Instantiate(template.rightRooms[Random.Range(0, template.rightRooms.Length)], transform.position, Quaternion.identity);
            }

            spawned = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("spawnPoint"))
        {
            if(collision.GetComponent<RoomSpawner>() == true)
            {
                if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    Instantiate(template.blankRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                spawned = true;
            }
            
        }
    }

}
