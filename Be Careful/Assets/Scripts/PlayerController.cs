﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float WalkSpeed = 5f;
    public float JumpSpeed = 5f;
    public GroundCheck GC;
    private Rigidbody2D rb;
    private bool jumping;
    public float JumpingSpeedFraction = 0.25f;
    private Animator anim;
    public float DeathForce = 500;
    private bool dead = false;
    public bool dying = false;
    public GameObject Body;
    public bool GetGrounded() { return grounded; }
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        if (!dying && !dead)
        {
            Vector2 move = Vector2.zero;
            float h = Input.GetAxis("Horizontal");
            if (jumping) h = h * JumpingSpeedFraction;
            if (h > 0) transform.localScale = new Vector3(1, 1, 1);
            else if (h < 0) transform.localScale = new Vector3(-1, 1, 1);
            move.x = h;
            if (h != 0) anim.SetBool("walking", true);
            else anim.SetBool("walking", false);
            Debug.Log("CompteVelocity");
            if(velocity .y < 0)
            if(Input.GetButtonDown("Jump") && grounded)
            {
                anim.SetBool("jumping", true);
                jumping = true;
            }else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0) velocity.y = velocity.y * 0.5f;
            }
            targetVelocity = move * WalkSpeed;
        }
        else if (dying)
        {
            anim.SetTrigger("Dead");
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(!dying && !dead)
    //    {
    //        if (rb.velocity.x != 0) anim.SetBool("walking", true);
    //        else anim.SetBool("walking", false);

    //        if (GC.isGrounded && Input.GetKeyDown(KeyCode.Space))
    //        {
    //            anim.SetBool("jumping", true);
    //            jumping = true;
    //        }
    //    }
    //    else if (dying)
    //    {
    //        anim.SetTrigger("Dead");
    //    }
    //}
    //private void FixedUpdate()
    //{
    //    if(!dying && !dead)
    //    {
    //        float h = Input.GetAxis("Horizontal");
    //        if (jumping) h = h * JumpingSpeedFraction;
    //        float F = ((WalkSpeed * h - rb.velocity.x) / Time.deltaTime) * rb.mass;
    //        rb.AddForce(new Vector2(F, 0));

    //        if (h > 0) transform.localScale = new Vector3(1, 1, 1);
    //        else if (h < 0) transform.localScale = new Vector3(-1, 1, 1);
    //    }

    //}
    public void Jump()
    {
        if (grounded)
        {
            //float F = ((JumpSpeed - rb.velocity.y) / Time.deltaTime) * rb.mass;
            //rb.AddForce(new Vector2(0, F));
            velocity.y = JumpSpeed;
        }
        jumping = false;
    }
    public void Die()
    {
        dying = false;
        dead = true;
        int childCount = Body.transform.childCount;
        Destroy(GetComponent<Animator>());
        for(int i = childCount -1; i >= 0; i -= 1)
        {
            Transform child = Body.transform.GetChild(i);
            child.parent = null;
            child.gameObject.AddComponent<Rigidbody2D>();
            child.GetComponent<Collider2D>().enabled = true;
            Vector2 destructVector = (child.position - transform.position);
            child.GetComponent<Rigidbody2D>().AddForce(destructVector * DeathForce);
        }
        Body.transform.parent = null;
        Body.GetComponent<Collider2D>().enabled = true;
        Body.AddComponent<Rigidbody2D>();
        Destroy(gameObject);
    }
}
