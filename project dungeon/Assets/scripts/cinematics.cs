using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematics : MonoBehaviour
{
    int frame;
    public IEnumerator shake(float duration, float strength)
    {
        float time = 0;

        while (time < duration)
        {
            float x = Random.Range(-1f, 1f) * strength;
            float y = Random.Range(-1f, 1f) * strength;
            transform.localPosition = new Vector3(x, y, transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = new Vector3(0, 0, 0);

       
    }

    public IEnumerator sleep(float timer)
    {
        Time.timeScale = 0;
        frame++;
        if(frame >= timer)
        {
            Time.timeScale = 1;
        }
        yield return new WaitForSeconds(0);
    }
}
