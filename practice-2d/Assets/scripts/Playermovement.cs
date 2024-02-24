using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Playermoveme : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField]private float speed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed,body.velocity.y);

        if (Input.GetKey(KeyCode.Space)){
            body.velocity = new Vector2(body.velocity.x, 5);
        }
    }
}
