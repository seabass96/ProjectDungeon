using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openChest : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<Animator>().SetTrigger("open");
            }
        }
    }
}
