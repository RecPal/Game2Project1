using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private bool toggleLight = true;
    [SerializeField] private GameObject lightBulb;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            toggleLight = !toggleLight;

            if(toggleLight)
            {
                lightBulb.SetActive(true);
            } else
            {
                lightBulb.SetActive(false);
            }
            
        }
    }

}
