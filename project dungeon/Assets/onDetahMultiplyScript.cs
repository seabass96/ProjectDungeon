using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDetahMultiplyScript : MonoBehaviour
{
    private void OnDestroy()
    {
        GameObject instance = Instantiate(GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().baseEnemy, transform.position + new Vector3(-0.2f, 0, 0), Quaternion.identity);
        GameObject instance2 = Instantiate(GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().baseEnemy, transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
        instance.GetComponent<enemyScript>().Enemy = GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().demonEnemies[0];
        instance2.GetComponent<enemyScript>().Enemy = GameObject.FindGameObjectWithTag("allEnemies").GetComponent<allEnemiesScript>().demonEnemies[0];
        instance.SetActive(true);
        instance2.SetActive(true);
    }

   
}
