using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;


public class Playermoveme : MonoBehaviour
{
    private Rigidbody2D body;
    private bool isgrounded = true;
    private bool CanDoubleJump = true;
    [SerializeField] private float speed;
    [SerializeField] private float jumpheight;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        //if statements to check for jump fucntions
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isgrounded)
            {
                Jump();
                CanDoubleJump = true;
            }
            else if (CanDoubleJump)
            {
                Jump();
                CanDoubleJump = false;
            }
        }
    }
    //jump function, is called during update method
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpheight);
        isgrounded = false;
    }

    //fucntion to check if the player is currently on the ground to reset the isgrounded bool which is needed in order not to continue jumping
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isgrounded = true;
            CanDoubleJump = true;
        }
    }

}


