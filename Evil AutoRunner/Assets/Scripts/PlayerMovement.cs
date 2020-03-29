using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("How fast the player speeds up")]
    [SerializeField] private float acceleration = 1f;

    [SerializeField] private float maxSpeed = 1f;

    [Tooltip("How fast the player slows down")]
    [SerializeField] private float deceleration = 1f;

    [Tooltip("Velocity that player is set to on jump")]
    [SerializeField] private float jumpForce = 0f;
    //Number of jumps player can perform
    [SerializeField] private int jumps = 0;
    //Number of jumps player gets when they go back onto the floor
    [SerializeField] private int maxJumps = 2;
    //Object References
    private Rigidbody rb = null;
    private Animator animator = null;
    private void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //Jumping
        if (WallCheck())
            jumps = 1;
        else if (jumps != maxJumps)
            if (GroundCheck() && rb.velocity.y <= 0)
                    jumps = maxJumps;

        Vector3 jumpForceFinal = new Vector3();

        if (Input.GetButtonDown("Jump"))
        {
            if (WallCheck())
            {
                jumpForceFinal.y += jumpForce;
            }
            else if (jumps > 0)
            {
                jumps -= 1;
                jumpForceFinal.y += jumpForce;
                animator.SetTrigger("Spin");
            }
            else
                jumpForceFinal.y += rb.velocity.y;
        }
        else
            jumpForceFinal.y += rb.velocity.y;

        float[] speed = { 0f, 0f };
        float[] input = { 0f, 0f };

            //Movement
            speed[0] = rb.velocity.x;
            speed[1] = rb.velocity.z;
            input[0] = Input.GetAxis("Horizontal");
            input[1] = Input.GetAxis("Vertical");

            input[1] = 0f;

        for (int i = 0; i < 2; i++)
        {
            if (input[i] != 0)
            {
                if ((speed[i] + acceleration) < maxSpeed * input[i])
                    speed[i] += acceleration * input[i];
                else
                    speed[i] = maxSpeed * input[i];
            }
            else
            {
                if (speed[i] > deceleration)
                    speed[i] -= deceleration;
                else
                    speed[i] = 0;
            }
        }

        //Final Movement
        rb.velocity = new Vector3(speed[0], 0f, speed[1]) + jumpForceFinal;
    }

    private bool GroundCheck()
    {
        LayerMask floorLayer = 1 << LayerMask.NameToLayer("Platform");

        return Physics.Raycast(transform.position, Vector3.down, 0.55f, floorLayer);
    }

    private bool WallCheck()
    {
        LayerMask wallLayer = 1 << LayerMask.NameToLayer("Platform");
        /*if ()
            return 1;
        else if ()
            return -1;
        else*/
        return Physics.Raycast(transform.position, Vector3.right, 0.55f, wallLayer) || Physics.Raycast(transform.position, Vector3.left, 0.55f, wallLayer);
    }

}
