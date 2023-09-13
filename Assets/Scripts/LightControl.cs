using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    private Light lightBulb;
    private float lightIntensity;

    [SerializeField]
    private bool toggleLight = true;

    // Start is called before the first frame update
    void Start()
    {
        lightBulb = GetComponent<Light>();
        lightIntensity = lightBulb.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            toggleLight = !toggleLight;
        }

        if (toggleLight) {
            lightBulb.intensity = 0.0f;
        } else {
            lightBulb.intensity = lightIntensity;
        }
    }
}
