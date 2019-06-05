using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shackAndBreak : MonoBehaviour
{
    public GameObject inFront;
    public float dropSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(shakeThenBreak());
        }
    }

    private IEnumerator breakOldPeice()
    {
        while(transform.GetChild(0).localScale.x >= 0)
        {
            transform.GetChild(0).localScale = new Vector3(transform.GetChild(0).localScale.x - dropSpeed, transform.GetChild(0).localScale.y - dropSpeed, transform.GetChild(0).localScale.z);
            transform.GetChild(1).localScale = new Vector3(transform.GetChild(1).localScale.x - dropSpeed, transform.GetChild(1).localScale.y - dropSpeed, transform.GetChild(1).localScale.z);
            yield return null;
        }
        yield return 0;
       
    }

    private IEnumerator shakeFront(float duration, float strength)
    {
        if(inFront != null)
        {
            float time = 0;
            Vector3 pos = inFront.transform.position;

            while (time < duration)
            {
                float x = Random.Range(-1f, 1f) * strength;
                float y = Random.Range(-1f, 1f) * strength;
                inFront.transform.position = new Vector3(inFront.transform.position.x + x, inFront.transform.position.y + y, inFront.transform.position.z);
                time += Time.deltaTime;
                yield return null;
            }

            inFront.transform.position = pos;
        }   
    }

    private IEnumerator shakeThenBreak()
    {
        StartCoroutine(shakeFront(0.2f, 0.03f));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(breakOldPeice());
    }
}
