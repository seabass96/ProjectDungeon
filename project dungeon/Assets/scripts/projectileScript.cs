using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public float range;
    private float speed = 5;
    private float damage;
    public enum projectileType { ONESHOT, BOUNCE };
    public projectileType type;
    private bool hasCollidedWithWall = false;
    private float xvel = 0;
    private float yvel = 0;
    bool hitleft = false;
    bool hitRight = false;
    bool hitUp = false;
    bool hitDown = false;
    public float spriteWidth;
    public float spriteHeight;

    private void Start()
    {
        Destroy(gameObject, 10);
    }
    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case projectileType.ONESHOT:
                transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
                break;
            case projectileType.BOUNCE:
                //change this later
                bounceOffWalls();
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("room"))
        {
            switch (type)
            {
                case projectileType.ONESHOT:
                    destroyProjectile();
                    break;
                case projectileType.BOUNCE:
                    //change this later 
                    destroyProjectile();
                    break;
            }

            
        }
    }

    public void destroyProjectile()
    {
        if(type == projectileType.ONESHOT)
        {
            if (transform.GetChild(1).gameObject != null) transform.GetChild(1).parent = null;
        }
       
        Destroy(gameObject);
    }

    public void setDamage(float dam)
    {
        damage = dam;
    }

    public float getDamage()
    {
        return damage;
    }

    public void bounceOffWalls()
    {
        
        // do both corners of the tornado 
        Vector3 endPosLeftend = transform.GetChild(0).position + new Vector3(-range, spriteHeight, 0) * transform.GetChild(0).localScale.x;
        Vector3 endPosLeftstart = transform.GetChild(0).position + new Vector3(-range, -spriteHeight, 0) * transform.GetChild(0).localScale.x;
        Vector3 startPosEnd = transform.GetChild(0).position + new Vector3(0, spriteHeight, 0) * transform.GetChild(0).localScale.x;
        Vector3 startPosstart = transform.GetChild(0).position + new Vector3(0, -spriteHeight, 0) * transform.GetChild(0).localScale.x;
        if(Physics2D.Linecast(startPosEnd, endPosLeftend, 1 << LayerMask.NameToLayer("room")) == true &&
            Physics2D.Linecast(startPosstart, endPosLeftstart, 1 << LayerMask.NameToLayer("room")) == true)
        {
            hitleft = true;
        }
        Debug.DrawLine(startPosEnd, endPosLeftend, Color.red);
        Debug.DrawLine(startPosstart, endPosLeftstart, Color.red);

        Vector3 endPosRightend = transform.GetChild(0).position + new Vector3(range, spriteHeight, 0) * transform.GetChild(0).localScale.x;
        Vector3 endPosRightstart = transform.GetChild(0).position + new Vector3(range, -spriteHeight, 0) * transform.GetChild(0).localScale.x;
        if (Physics2D.Linecast(startPosEnd, endPosRightend, 1 << LayerMask.NameToLayer("room")) == true &&
            Physics2D.Linecast(startPosstart, endPosRightstart, 1 << LayerMask.NameToLayer("room")) == true)
        {
            hitRight = true;
        }
        Debug.DrawLine(startPosEnd, endPosRightend, Color.red);
        Debug.DrawLine(startPosstart, endPosRightstart, Color.red);

        Vector3 endPosUpend = transform.GetChild(0).position + new Vector3(spriteWidth, range, 0) * transform.GetChild(0).localScale.y;
        Vector3 endPosUpstart = transform.GetChild(0).position + new Vector3(-spriteWidth, range, 0) * transform.GetChild(0).localScale.y;
        Vector3 startPosEndvert = transform.GetChild(0).position + new Vector3(spriteWidth, 0, 0) * transform.GetChild(0).localScale.y;
        Vector3 startPosstartvert = transform.GetChild(0).position + new Vector3( -spriteWidth, 0, 0) * transform.GetChild(0).localScale.y;
        if (Physics2D.Linecast(startPosEndvert, endPosUpend, 1 << LayerMask.NameToLayer("room")) == true &&
            Physics2D.Linecast(startPosstartvert, endPosUpstart, 1 << LayerMask.NameToLayer("room")) == true)
        {
            hitUp = true;
        }
        Debug.DrawLine(startPosEndvert, endPosUpend, Color.red);
        Debug.DrawLine(startPosstartvert, endPosUpstart, Color.red);

        Vector3 endPosDownend = transform.GetChild(0).position + new Vector3(spriteWidth, -range, 0) * transform.GetChild(0).localScale.y;
        Vector3 endPosDownstart = transform.GetChild(0).position + new Vector3(-spriteWidth, -range, 0) * transform.GetChild(0).localScale.y;
        if (Physics2D.Linecast(startPosEndvert, endPosDownend, 1 << LayerMask.NameToLayer("room")) == true &&
            Physics2D.Linecast(startPosstartvert, endPosDownstart, 1 << LayerMask.NameToLayer("room")) == true)
        {
            hitDown = true;
        }
        Debug.DrawLine(startPosEndvert, endPosDownend, Color.red);
        Debug.DrawLine(startPosstartvert, endPosDownstart, Color.red);

        if (hitDown)
        {
            xvel = -0.5f;
            yvel = 0.5f;
            hitDown = false;
            hasCollidedWithWall = true;
           // Debug.Log("hit down");
            transform.rotation = Quaternion.identity;
        }
        else if (hitleft)
        {
            xvel = 0.5f;
            yvel = 0.5f;
            hitleft = false;
            hasCollidedWithWall = true;
            //Debug.Log("hit left");
            transform.rotation = Quaternion.identity;
        }
        else if (hitUp)
        {
            xvel = 0.5f;
            yvel = -0.5f;
            hitUp = false;
            hasCollidedWithWall = true;
            //Debug.Log("hit up");
            transform.rotation = Quaternion.identity;
        }
        else if (hitRight)
        {

            xvel = -0.5f;
            yvel = -0.5f;
            hitRight = false;
            hasCollidedWithWall = true;
            //Debug.Log("hit right");
            transform.rotation = Quaternion.identity;
        }
        else if (hasCollidedWithWall == false)
        {
            transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        }

        transform.position = new Vector3(transform.position.x + xvel * speed * Time.deltaTime, transform.position.y + yvel * speed * Time.deltaTime, transform.position.z);


    }

}
