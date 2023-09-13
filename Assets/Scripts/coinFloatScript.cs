using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
public class coinFloatScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float counter;
    public bool goingUp;
    public float origPos;
    public float newPos;

    

    public float rotY;   
    // Start is called before the first frame update
    void Start()
    {
        counter = 0f;
        goingUp = true;
        origPos = transform.position.y;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        newPos = origPos + (float)counter;
        rotY = transform.rotation.y;
        transform.Rotate(0f, 65f * Time.deltaTime, 0f, Space.Self);
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + 0.003f, transform.rotation.z, transform.rotation.w);
        transform.position = new Vector3(transform.position.x, newPos, transform.position.z);
        if(goingUp) {
            counter += 0.009f;
        } else {
            counter -= 0.009f;
        }

        if((counter >= 1f) && (goingUp == true)) {
            goingUp = false;
            print("going down");
        } else if ((counter <= 0f) && (goingUp == false)) {
            goingUp = true;
        }
        
    }

    public void OnTriggerEnter(Collider col) {
        if (col.name == "PlayerSphere") {
            Destroy(this.gameObject);
            PlayerScript plrScript = col.GetComponent<PlayerScript>();
            plrScript.coins += 1;
        } else {
            print(col.name);
        }
    }
}
