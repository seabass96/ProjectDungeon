using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterInput : MonoBehaviour
{
    private float speed;
    private float horizontalSpeed;
    private float verticalSpeed;
    private bool go = true;
    // Start is called before the first frame update
    void Start()
    {
        speed = CharacterStats.Instance.Stamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(go)
        {
            getInput();
            moveCharacter();
        } 
        animCharacter();
    }

    private void getInput()
    {
        horizontalSpeed = Input.GetAxisRaw("Horizontal") * speed;
        verticalSpeed = Input.GetAxisRaw("Vertical") * speed;
    }

    private void moveCharacter()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    private void animCharacter()
    {
        float AnimSpeed = horizontalSpeed + verticalSpeed;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            transform.localScale = new Vector3(5, 5, 5);
            gameObject.GetComponent<Animator>().SetFloat("speed", AnimSpeed);
        }
        else
        {
            transform.localScale = new Vector3(-5, 5, 5);
            gameObject.GetComponent<Animator>().SetFloat("speed", -AnimSpeed);
        }
    }

    public void STOP()
    {
        horizontalSpeed = 0;
        verticalSpeed = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalSpeed, verticalSpeed);
        go = false;
    }

    public void START()
    {
        go = true;
    }
}
