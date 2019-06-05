using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTakeDamage : MonoBehaviour
{
    public float xbounce;
    public GameObject playerdeath;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("playerHealthBar").GetComponent<Slider>().maxValue = gameObject.GetComponent<CharacterStats>().health;
    }

    private void Update()
    {
        if(gameObject.GetComponent<CharacterStats>().health <= 0)
        {
            Instantiate(playerdeath, transform.GetChild(0).GetChild(0).GetChild(0).position, Quaternion.identity);
            gameObject.SetActive(false);
        }

      
        GameObject.FindGameObjectWithTag("playerHealthBar").GetComponent<Slider>().value = gameObject.GetComponent<CharacterStats>().health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            if(collision.transform.position.x < transform.position.x)
            {
                StartCoroutine(bump(gameObject, xbounce*2));
                StartCoroutine(bump(collision.gameObject, -xbounce));
                gameObject.GetComponent<CharacterStats>().health -= collision.gameObject.GetComponent<enemyScript>().getDamage();
            }
            else if (collision.transform.position.x > transform.position.x)
            {

                StartCoroutine(bump(gameObject, -xbounce*2));
                StartCoroutine(bump(collision.gameObject, xbounce));
                gameObject.GetComponent<CharacterStats>().health -= collision.gameObject.GetComponent<enemyScript>().getDamage();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy projectile"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                StartCoroutine(bump(gameObject, xbounce * 2));
                
                gameObject.GetComponent<CharacterStats>().health -= collision.gameObject.GetComponent<projectileScript>().getDamage();
                collision.GetComponent<projectileScript>().destroyProjectile();
            }
            else if (collision.transform.position.x > transform.position.x)
            {

                StartCoroutine(bump(gameObject, -xbounce * 2));
                gameObject.GetComponent<CharacterStats>().health -= collision.gameObject.GetComponent<projectileScript>().getDamage();
                collision.GetComponent<projectileScript>().destroyProjectile();
            }
        }
    }

    private IEnumerator bump(GameObject actor, float amount)
    {
        if(actor != null)
        {
            actor.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            actor.GetComponent<Rigidbody2D>().AddForce(new Vector2(amount, 0));
            yield return new WaitForSeconds(0.1f);
            if (actor != null)
            {
                actor.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }     
        }     
    }
}
