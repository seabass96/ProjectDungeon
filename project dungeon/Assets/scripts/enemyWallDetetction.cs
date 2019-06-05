using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWallDetetction : MonoBehaviour
{
    public bool canBounceLeft = true;
    public bool canBounceRight = true;
    public float range = 0.4f;

    private void Update()
    {
        bool hitleft = false;
        Vector3 endPosLeft = transform.position + new Vector3(-range, 0, 0) * transform.localScale.x;
        hitleft = Physics2D.Linecast(transform.position, endPosLeft, 1 << LayerMask.NameToLayer("room"));
        if(hitleft)
        {
            canBounceLeft = false;
        }
        else
        {
            canBounceLeft = true;
        }
        Debug.DrawLine(transform.position, endPosLeft, Color.red);

        bool hitRight = false;
        Vector3 endPosRight = transform.position + new Vector3(range, 0, 0) * transform.localScale.x;
        hitRight = Physics2D.Linecast(transform.position, endPosRight, 1 << LayerMask.NameToLayer("room"));
        if (hitRight)
        {
            canBounceRight = false;
        }
        else
        {
            canBounceRight = true;
        }
        Debug.DrawLine(transform.position, endPosRight, Color.red);
    }
}
