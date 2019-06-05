using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public GameObject weapon;
    private Transform restPosition;
    public float pokeTime = 1;
    private float timer;
    private bool pokeForward = true;

    private float swingTimer;
    private bool swingOver = true;

    private void Start()
    {
        restPosition = transform;
        timer = pokeTime;
    }

    private void Update()
    {
        switch (weapon.GetComponent<swordDisplay>().getWeapon().type)
        {
            case global::weapon.swingType.SINGLE:
                weaponSwingSingle();
                break;
            case global::weapon.swingType.AUTO:
                weaponSwingReapeat();
                break;
            case global::weapon.swingType.SPEAR:
                weaponThrust();
                break;
        }
    }

    public void weponTurnOff()
    {
        transform.localRotation = restPosition.localRotation;
        gameObject.GetComponent<Animator>().SetBool("swing", false);
        swingOver = true;
        weapon.SetActive(false);
    }

    public float getPlayerDamage()
    {
        return weapon.GetComponent<swordDisplay>().getDamage();
    }

    private void weaponSwingReapeat()
    {
        gameObject.GetComponent<Animator>().SetBool("auto", true);

        if (Input.GetMouseButtonDown(0))
        {
            weapon.SetActive(true);
            gameObject.GetComponent<Animator>().SetBool("swing", true);
        }
        else if (Input.GetMouseButton(0))
        {
            weapon.SetActive(true);
            gameObject.GetComponent<Animator>().SetBool("swing", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.SetActive(false);
            gameObject.GetComponent<Animator>().SetBool("swing", false);
        }
    }

    private void weaponSwingSingle()
    {
        gameObject.GetComponent<Animator>().SetBool("auto", false);

        if(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("swordIdle"))
        {
            if (Input.GetMouseButtonDown(0) && swingOver == true)
            {
                swingOver = false;
                weapon.SetActive(false);
                gameObject.GetComponent<Animator>().SetBool("swing", false);
                weapon.SetActive(true);
                gameObject.GetComponent<Animator>().SetBool("swing", true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                gameObject.GetComponent<Animator>().SetBool("swing", false);
            }

            if (swingOver)
            {
                weapon.SetActive(false);
                gameObject.GetComponent<Animator>().SetBool("swing", false);
            }
        }    
    }

    private void weaponThrust()
    {

        float angleInRad = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - weapon.transform.position.x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y - weapon.transform.position.y);

        float angleInDeg = (180 / Mathf.PI) * angleInRad;

        if(transform.parent.localScale.x > 0)
        {
            weapon.transform.localRotation = Quaternion.Euler(0, 0, -angleInDeg);
        }
        else
        {
            weapon.transform.localRotation = Quaternion.Euler(0, 0, angleInDeg);
        }

        if(timer <= 0)
        {
            timer = pokeTime;

            if(pokeForward)
            {
                pokeForward = false;
            }
            else
            {
                pokeForward = true;
            }        
        }
        else
        {
            timer -= Time.deltaTime;
        }

        

        if (Input.GetMouseButton(0))
        {
            weapon.SetActive(false);
            //gameObject.GetComponent<Animator>().SetBool("thrust", false);
            weapon.SetActive(true);
            //gameObject.GetComponent<Animator>().SetBool("thrust", true);

            if(pokeForward)
            {
                weapon.transform.localPosition = new Vector3(weapon.transform.localPosition.x, weapon.transform.localPosition.y + Time.deltaTime, weapon.transform.localPosition.z);
            }
            else
            {
                weapon.transform.localPosition = new Vector3(weapon.transform.localPosition.x, weapon.transform.localPosition.y - Time.deltaTime, weapon.transform.localPosition.z);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.SetActive(false);
           // gameObject.GetComponent<Animator>().SetBool("thrust", false);
        }
    }
        
}
