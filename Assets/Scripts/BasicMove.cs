using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float playerJump = 2f;
    private Rigidbody playerRB;
    private Vector3 movement;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    //    TransformMove();

        float modSpeed = speed * Time.deltaTime;
        movement.x = modSpeed * Input.GetAxis("Horizontal");
        movement.z = modSpeed * Input.GetAxis("Vertical");
        playerRB.AddForce(movement, ForceMode.VelocityChange);

        if(Input.GetButtonDown("Jump"))
        {
            playerRB.AddForce(0f, playerJump, 0f, ForceMode.Impulse);
        }
    }

    void TransformMove()
    {
        // Using Axis instead of direct input

        float modSpeed = speed * Time.deltaTime;

        transform.Translate(
            modSpeed * Input.GetAxis("Horizontal"),     // Horizontal Translation Component 
            0f,
            modSpeed * Input.GetAxis("Vertical")        // Vertical Translation Component
            );

        // Using direct input

        /* if (Input.GetKey(KeyCode.W))                                  // Gets input for 'W' key
        {
            transform.Translate(0f, 0f, speed * Time.deltaTime);      // Translates the ball -speed Units on the Z Axis
        }
        if (Input.GetKey(KeyCode.S))                                  // Gets input for S Key
        {
            transform.Translate(0f, 0f, -speed * Time.deltaTime);     // Translates the ball -speed Units on the Z Axis
        }   */
    }
}
