using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Rigidbody2D body;//for body.velocity
    private Animator animation;
    private PlayerMoves playerMoves;
    private float cooldownTimer = 9999;//Mathf.Infinity (very big number)
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();//for body.velocity
        animation = GetComponent<Animator>();
        playerMoves = GetComponent<PlayerMoves>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMoves.canAttack())
        {
            Attack();
            /*            body.velocity = new Vector3(0, 0, 0);*/ //attack when i want
        }
        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        animation.SetTrigger("attack");
        cooldownTimer = 0;
    }
}