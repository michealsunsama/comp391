using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMover : MonoBehaviour {
    public float walkSpeed;
    public int moveSpeed;
    private Animator anim;
    private Rigidbody2D rb;

    void Start () {
        anim= this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }
	
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.speed = walkSpeed;
            anim.Play("walkUp");
            rb.velocity = new Vector2(0f, moveSpeed);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.speed = walkSpeed;
            anim.Play("walkLeft");
            rb.velocity = new Vector2(-moveSpeed, 0f);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            anim.speed = walkSpeed;
            anim.Play("walkDown");
            rb.velocity = new Vector2(0f, -moveSpeed);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.speed = walkSpeed;
            anim.Play("walkRight");
            rb.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            anim.speed = 0;
            rb.velocity = new Vector2(0f, 0f);
        }
    }
}
