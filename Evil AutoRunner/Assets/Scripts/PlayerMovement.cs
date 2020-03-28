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
    private int jump = 0;
    //Number of jumps player gets when they go back onto the floor
    private int maxJumps = 2;

    //Object References
    private Rigidbody rb = null;

    private void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float[] speed = { rb.velocity.x, rb.velocity.z };
        float[] input = { Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") };
        float jump = Input.GetButtonDown("Jump") ? jumpForce : rb.velocity.y;

        input[1] = 0f;

        for (int i = 0; i < 2; i++)
        {
            if(input[i] != 0)
            {
                if ((speed[i] + acceleration)  < maxSpeed * input[i])
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

        Vector3 x = transform.right * speed[0];
        Vector3 z = transform.forward * speed[1];

        rb.velocity = new Vector3(0f, jump, 0f) + x + z;
    }

    private bool GroundCheck()
    {
        LayerMask floorLayer = 1 << LayerMask.NameToLayer("Floor");

        return Physics.Raycast(transform.position, Vector3.down, 0.55f, floorLayer);
    }
}
