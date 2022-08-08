using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animation;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpPower;
    [SerializeField] float height = 5;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
/*        float verticalInput = Input.GetAxis("Vertical");*/
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);// = Vector3.one is also possible
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animation.SetBool("Run", horizontalInput != 0);
        animation.SetBool("grounded", isGrounded());
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
        if (wallJumpCooldown > 0.2f)
            {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
            if(onWall() && isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 2.5f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }
    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animation.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Input.GetAxis("Horizontal") * 10 / 2, 0);
                transform.localScale = new Vector3(- Input.GetAxis("Horizontal"), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Input.GetAxis("Horizontal") * speed / 2, 12);
            }
            wallJumpCooldown = 0;
        }
    }
    /*    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                grounded = true;
            }
        }*/
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer); // center point of BoxCast , size, angle, direction, distance, check the layer of the ground, not enemies or oyher things 
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer); // center point of BoxCast , size, angle, direction, distance, check the layer of the ground, not enemies or oyher things 
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}