using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleExplodeScript : MonoBehaviour
{
    private float damage;
    public float ExplosivePower;
    public float ExplosiveRadius;

    private void Start()
    {
        StartCoroutine(waitAndEnd());
    }
    
    private IEnumerator waitAndEnd()
    {
        yield return new WaitForSeconds(0.2f);
        // eplode with damage
        var Objects = GameObject.FindObjectsOfType<Rigidbody2D>();
        foreach (Rigidbody2D item in Objects)
        {
            if (Vector2.Distance(item.gameObject.transform.position, transform.gameObject.transform.position) < ExplosivePower)
            {
                if (item.CompareTag("Player"))
                {
                    item.gameObject.GetComponent<Rigidbody2D>().AddForce((item.gameObject.transform.position - transform.position) * ExplosivePower, ForceMode2D.Impulse);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().health -= damage;
                }
            }
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
}
