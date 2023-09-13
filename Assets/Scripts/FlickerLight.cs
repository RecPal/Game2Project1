using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component makes a light flicker by playing with it's intensity.
/// </summary>
/// 
public class FlickerLight : MonoBehaviour
{

    private Light lightBulb;
    [SerializeField] private float threshold;
    [SerializeField] private float timer = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        lightBulb = GetComponent<Light>();
        threshold = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;    // Gather the time between frame updates

        if (timer > threshold)
        {
            lightBulb.intensity = Random.Range(0f, 1f);     // Pick a random number between 0 and 1
            timer = 0f;                                     // Zero out timer
            threshold = Random.Range(0.0f, 5.0f);           // Creates new threshold value
        }
    }
}
