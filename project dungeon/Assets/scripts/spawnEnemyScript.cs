using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemyScript : MonoBehaviour
{
    public GameObject enemySpawner;
    public enum layoutRotations { QUATER, HALF, THREEQUATERS, NORMAL };
    public int credits = 4;
    public enum enemyType { ZOMBIE, DEMON, ORK, ROBOT };
    public enemyType typeOfEnemy;
    private enemy[] enemies;
    public GameObject enemyShell;
    // Start is called before the first frame update
    void Start()
    {
        int randLyaout = Random.Range(0, 2);
        GameObject layout = enemySpawner.transform.GetChild(randLyaout).gameObject;
        layout.SetActive(true);
        layoutRotations rot = (layoutRotations)Random.Range(0, 3);
        switch (rot)
        {
            case layoutRotations.QUATER:
                layout.transform.rotation = Quaternion.Euler( new Vector3(layout.transform.rotation.x, layout.transform.rotation.y, layout.transform.rotation.z + 90));
                break;
            case layoutRotations.HALF:
                layout.transform.rotation = Quaternion.Euler(new Vector3(layout.transform.rotation.x, layout.transform.rotation.y, layout.transform.rotation.z + 180));
                break;
            case layoutRotations.THREEQUATERS:
                layout.transform.rotation = Quaternion.Euler(new Vector3(layout.transform.rotation.x, layout.transform.rotation.y, layout.transform.rotation.z + 270));
                break;
            case layoutRotations.NORMAL:
                layout.transform.rotation = Quaternion.Euler(new Vector3(layout.transform.rotation.x, layout.transform.rotation.y, layout.transform.rotation.z ));
                break;
        }

       

        switch (typeOfEnemy)
        {
            case enemyType.ZOMBIE:
                enemies = GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().zombieEnemies;
                break;
            case enemyType.DEMON:
                enemies = GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().demonEnemies;
                break;
            case enemyType.ORK:
                enemies = GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().orkEnemies;
                break;
            case enemyType.ROBOT:
                enemies = GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().robotEnemies;
                break;

        }

        int i = 0;
        while (credits > 0)
        {
            if(credits <= 0)
            {
                credits = 4;
            }
            //int randomEnemy = Random.Range(0, (int)enemies.Length-1);
            int randomEnemy = Random.Range(0, 5);
            GameObject enemy = Instantiate(enemyShell, layout.transform.GetChild(i).transform.position, Quaternion.identity);
            enemy.GetComponent<enemyScript>().Enemy = enemies[randomEnemy];
            enemy.transform.position = enemy.transform.position + new Vector3(0, 0, -7);
            enemy.SetActive(true);
            credits -= enemies[randomEnemy].cost;
            ++i;
        }




    }

   
}
