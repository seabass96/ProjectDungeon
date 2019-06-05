using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class flameScript : MonoBehaviour
{
    public GameObject teleportEffect;
    private GameObject player;
    private enum dungeonType { ORKS, ZOMBIES, DEMONS, ROBOTS };

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(player.transform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z - 1);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(teleport());
            }
        }
    }

    private IEnumerator teleport()
    {
        GameObject effect = Instantiate(teleportEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        effect.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
