using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject blankRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    private void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if(i == rooms.Count-1)
                {
                    if(boss != null)
                    {
                        Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        
                    }
                    spawnedBoss = true;
                }
            }
        }
        else { waitTime -= Time.deltaTime; }
    }
}
