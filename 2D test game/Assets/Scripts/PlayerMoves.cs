using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animation;
    private bool grounded;
    [SerializeField]float speed = 5;
    [SerializeField]float height = 5;
    [SerializeField]float jumpcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        body.velocity  = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if(horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);// = Vector3.one is also possible
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        animation.SetBool("Run", horizontalInput != 0);
        animation.SetBool("grounded", grounded);
        /*if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            animation.SetBool("Fly", true);
        }
        else
        {
            animation.SetBool("Fly", false);
        }

        if (horizontalInput! = 0f)
        {
            animation.SetBool("Fly", true);
        }
        else
        {
            animation.SetBool("Fly", false);
        }*/
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, height);
        animation.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
