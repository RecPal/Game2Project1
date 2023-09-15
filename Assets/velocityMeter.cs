using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class velocityMeter : MonoBehaviour
{
    /* [SerializeField] float currentVelocity;
    [SerializeField] float velocityCutoff
    [SerializeField] float timeHold;
    [SerializeField] float currentTime; */

    public Slider velocitySlider;
    public float velocityGoal;
    public float velocityMeasure;
    public bool notComplete = true;
    public float timeMeasure;
    public float timeGoal;
    public float timeLeft;
    public Text textOUT;
    private Rigidbody playerTemp;

    // Start is called before the first frame update
    void Start()
    {
        playerTemp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Rigidbody>();
        velocityGoal = GameObject.Find("GameManager").GetComponent<GameManager>().velocityCutoff;
        velocitySlider.maxValue = velocityGoal;
        timeGoal = GameObject.Find("GameManager").GetComponent<GameManager>().timeHold;
    }

    // Update is called once per frame
    void Update()
    {
        velocityMeasure = playerTemp.velocity.magnitude;
        velocitySlider.value = velocityMeasure;
        timeMeasure = GameObject.Find("GameManager").GetComponent<GameManager>().currentTime;
        timeLeft = timeGoal - timeMeasure;
        if (notComplete)
        {
            if(timeMeasure > 0)
            {
                if(timeMeasure > timeGoal)
                {
                    notComplete = false;
                }
                textOUT.text = "Time Left: " + Convert.ToString(timeLeft);
            }
            else
            {
                textOUT.text = "";
            }
        }
        else
        {
            textOUT.text = "Ascending Velocity Planes!";
        }

    }
}
