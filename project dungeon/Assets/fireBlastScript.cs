using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBlastScript : MonoBehaviour
{
    private bool hasHit = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(hasHit == false)
            {
                //playerTakesDamage
                collision.GetComponent<CharacterStats>().health -= gameObject.GetComponentInParent<enemyScript>().getDamage();
                hasHit = true;
            }
        }
    }
}
