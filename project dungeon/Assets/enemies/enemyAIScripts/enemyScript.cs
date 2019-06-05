using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyScript : MonoBehaviour
{
    public enemy Enemy;
    public float xbounce;
    public float ybounce;
    public float InvicabilatyTime;
    public float sleepTimer;
    public GameObject bloodsplatter;
    private float health;
    private float attack;
    public Slider slider;

    public void Awake()
    {
        health = Enemy.health;
        attack = Enemy.baseDamage;
        gameObject.AddComponent<Animator>().runtimeAnimatorController = Enemy.animator as RuntimeAnimatorController;
        gameObject.GetComponent<BoxCollider2D>().size = Enemy.colliderSize;
        gameObject.GetComponent<BoxCollider2D>().offset = Enemy.colliderOffset;
        switch (Enemy.type)
        {
            case enemy.AItype.CHASER:
                gameObject.AddComponent<enemyChaser>();
                break;

            case enemy.AItype.ICESHOOTER:
                gameObject.AddComponent<rangedEnemyAi>();
                break;

            case enemy.AItype.FARTNADO:
                gameObject.AddComponent<enemyFarthHurricain>();
                break;

            case enemy.AItype.KAMAKAZE:
                gameObject.AddComponent<kamakazeEnemieAI>();
                break;

            case enemy.AItype.SLIMETRAIL:
                gameObject.AddComponent<swampyEnemy>();
                gameObject.AddComponent<enemyChaser>();
                break;

            case enemy.AItype.MUILTIPLY:
                gameObject.AddComponent<enemyChaser>();
                gameObject.AddComponent<onDetahMultiplyScript>();
                break;

            case enemy.AItype.GROWTOPARENT:
                gameObject.AddComponent<enemyMultiplyScript>();
                break;

        }
        slider.maxValue = Enemy.health;
    }

    private void Update()
    {
        slider.value = health;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("playerWeapon"))
        {
            if(collision.transform.position.x < transform.position.x)
            {
                if(gameObject.GetComponent<enemyWallDetetction>().canBounceRight)
                {
                    StartCoroutine(bump(xbounce, ybounce));
                }   
                health -= collision.GetComponentInParent<playerAttack>().getPlayerDamage();
                StartCoroutine(flashRed());
                StartCoroutine(invicebilaty(InvicabilatyTime));
            }
            else if(collision.transform.position.x > transform.position.x)
            {
                if (gameObject.GetComponent<enemyWallDetetction>().canBounceLeft)
                {
                    StartCoroutine(bump(-xbounce, ybounce));
                }
                health -= collision.GetComponentInParent<playerAttack>().getPlayerDamage();
                StartCoroutine(flashRed());
                StartCoroutine(invicebilaty(InvicabilatyTime));
            } 
        }

        
    }

 

    public float getDamage()
    {
        return attack;
    }

    public float getHealth()
    {
        return health;
    }

    private IEnumerator flashRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    private IEnumerator bump(float xbump, float ybump)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xbump, ybump));
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    private IEnumerator invicebilaty(float time)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

}
