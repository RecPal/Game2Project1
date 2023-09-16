using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MomentumBallPlayer : MonoBehaviour
{
    [Header("Dominick Trusko \nBall-Player controller script")]

    [Header("WASD to move, [ctrl] to flip directions")]

    [Header("Enables debug vectors and console readouts")]
    public bool debug;

    [Header("Essential GameObject references")]
    [SerializeField] private GameObject cameraObj;       // Cant hunt camera down by tag for some reason
    [SerializeField] private GameObject cameraCube;      // Cameracube to anchor camera to rigidbody

    [Header("Essential cameraTransform (Transform of MainCamera Obj) \n and the player rigidbody (The rigidbody of the ball)")]
    [SerializeField] private Rigidbody playerRB;         // Playerball rigidbody reference
    [SerializeField] private Transform cameraTransform;  // Camera transform reference

    [Header("Forces and cooldown for manual boost (Turning around)")]
    [SerializeField] float moveForce = 15;               // Movement force multiplier
    [SerializeField] float boostForce = 20;              // Manual boost force multiplier
    [SerializeField] float manualTurnForce = 2;          // Turn force. 1 means stop, 2 means turn around without losing momentum

    [Header("Mouse min and max, as well as current position and camera turn speed")]
    [SerializeField] float minMouseY = 0;                // Minimum y axis for mouse (Inverted)
    [SerializeField] float maxMouseY = 70;               // Maximum y axis for mouse (Inverted)
    [SerializeField] float mouseX;                       // Current mouseX position
    [SerializeField] float mouseY;                       // Current mouseY position
    [SerializeField] float cameraTurnSpeed = 0.2f;       // Camera turn speed (Interpolation value)
    [SerializeField] float mouseSensitivityMultiple = 1.0f;

    [Header("Spawn Point")]

    [SerializeField] GameObject spawnObject;
    private Vector3 cameraForward;                       // Camera's forward vector, for making inputs camera relative

    void Start()
    {
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();    // Assigns the player rigidbody component to the variable
        cameraTransform = cameraObj.transform;                                    // Pulls camera transform from Unity camera reference
    }

    private void OnPreRender()
    {
        cameraTransform.LookAt(playerRB.position);
    }

    private void Update()
    {
        cameraForward = cameraTransform.forward;              // Updates the camera's forward vector value
        cameraCube.transform.position = playerRB.position;    // Moves the camera cube to the playerRB location

        ButtonInputReaderAndBoostTimer();                     // Calculates the current boost value, and checks to see if the brake is pressed

        if (debug)
        {
            cameraCube.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            cameraCube.GetComponent<MeshRenderer>().enabled = false;
        }

        cameraUpdate();

        if(debug) { print(playerRB.velocity.magnitude); }
    }

    private void ButtonInputReaderAndBoostTimer()
    {
        if (playerRB.velocity.y < 0.1f || playerRB.velocity.x > 0.1f)     // Checks if the player's Y velocity is below a certain threshold to see if they are airborne
        {
            addPlayerVelocity();                    // Actually add the force vectors
        }

        if (Input.GetButtonDown("Brake"))           // Flips player around without losing speed
        {
            addFlipForce();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            print("pressed N");
            GameObject.Find("GameManager").GetComponent<GameManager>().currentTime = 70;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            print("pressed R");
            GameObject.Find("PlayerSphere").transform.position = spawnObject.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {

            if (other.gameObject.tag == "BoostPad" && other.gameObject.GetComponent<BoostpadScript>().boostActivated())
            {
                if (debug) { print("Boost Activated!"); }
                addBoostVelocity();
            }
        


    }

    private void addFlipForce()
    {
        var tempVelocity = playerRB.velocity;
        tempVelocity.x *= manualTurnForce;
        tempVelocity.z *= manualTurnForce;
        tempVelocity.y = 0.0f;

        mouseX = mouseX + 180;                      // Flips the camera to match new direction

        playerRB.AddForce(-tempVelocity, ForceMode.VelocityChange);
    }

    private void addBoostVelocity()
    {
        // Assigns axis to floats
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Camera transform vectors
        Vector3 camFront = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camFront.y = 0;
        camRight.y = 0;
        camFront.Normalize();
        camRight.Normalize();

        camFront *= yAxis * boostForce;
        camRight *= xAxis * boostForce;

        var finalForceVector = camFront + camRight;
        Vector3.ClampMagnitude(finalForceVector, boostForce);

        playerRB.AddForce(finalForceVector, ForceMode.Impulse);





        /*    Vector3 camFront = cameraTransform.forward;

            camFront.y = 0;
            camFront.Normalize();

            camFront *= boostForce * Time.deltaTime;

            var finalBoostVector = camFront;

            playerRB.AddForce(finalBoostVector, ForceMode.Impulse);

            if (debug)
            {
                    if (Input.GetButton("Boost"))
                    {
                        Color debugColor = Color.red;
                        Debug.DrawLine(playerRB.position + finalBoostVector, playerRB.position);
                    }
            } */
    }

    private void cameraUpdate()
    {
        mouseX = mouseX + (Input.GetAxis("Mouse X") * mouseSensitivityMultiple);
        mouseY = mouseY + (Input.GetAxis("Mouse Y") * mouseSensitivityMultiple);

        if (mouseY < minMouseY) { mouseY = minMouseY; }
        if (mouseY > maxMouseY) { mouseY = maxMouseY; }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, mouseX, mouseY), cameraTurnSpeed);

        //transform.rotation = Quaternion.Euler(0, mouseX, mouseY);
    }

    void addPlayerVelocity()
    {
        // Assigns axis to floats
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Camera transform vectors
        Vector3 camFront = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camFront.y = 0;
        camRight.y = 0;
        camFront.Normalize();
        camRight.Normalize();

        camFront *= yAxis;
        camRight *= xAxis;

        var finalForceVector = camFront + camRight;

        finalForceVector.Normalize();
        finalForceVector *= moveForce * Time.deltaTime;

        playerRB.AddForce( finalForceVector, ForceMode.Force );

        if (debug)
        {
            if (Input.anyKey)
            {
                Color debugColor = Color.blue;
                Debug.DrawLine (playerRB.position + finalForceVector, playerRB.position);

                print(finalForceVector.magnitude + " " + moveForce * Time.deltaTime);
            }
            
        }
    }

}
