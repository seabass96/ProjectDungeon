using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordDisplay : MonoBehaviour
{
    public weapon Weapon;
    public GameObject bloodsplatter;
    private float damage;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Weapon.artwork;
        Vector2 coliderSize = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = coliderSize;
        gameObject.transform.localScale = Weapon.scale;
        gameObject.transform.localPosition = Weapon.position;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Weapon.rotation));
        damage = Weapon.attack;
        Invoke("turnoffWeapon", 1);

    }

    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Weapon.artwork;
        Vector2 coliderSize = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = coliderSize;
        gameObject.transform.localScale = Weapon.scale;
        gameObject.transform.localPosition = Weapon.position;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Weapon.rotation));
        damage = Weapon.attack;
        Invoke("turnoffWeapon", 1);
    }

    private void LateUpdate()
    {
        
        if(gameObject.activeSelf == false)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Weapon.artwork;
            Vector2 coliderSize = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            gameObject.GetComponent<BoxCollider2D>().size = coliderSize;
            gameObject.transform.localScale = Weapon.scale;
            gameObject.transform.localPosition = Weapon.position;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Weapon.rotation));
            damage = Weapon.attack;
        }
    }

    private void turnoffWeapon()
    {
        gameObject.SetActive(false);
    }

    public float getDamage()
    {
        return damage;
    }

    public weapon getWeapon()
    {
        return Weapon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            //TODO chage blood color depending on eenmy 
            Instantiate(bloodsplatter, transform.GetChild(0).position, new Quaternion(0,0,Random.Range(-180, 180), 0));
        }
    }
}
