using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwnChests : MonoBehaviour
{
    public GameObject chest;
    public GameObject spawnPoints;

    void Start()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                Instantiate(chest, spawnPoints.transform.GetChild(0).transform.position, Quaternion.identity);
                break;

            case 1:
                Instantiate(chest, spawnPoints.transform.GetChild(1).transform.position, Quaternion.identity);
                break;

            case 2:
                Instantiate(chest, spawnPoints.transform.GetChild(2).transform.position, Quaternion.identity);
                break;

        }
    }

}
