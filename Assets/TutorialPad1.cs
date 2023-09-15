using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPad1 : MonoBehaviour
{
    [SerializeField] Material activeMat;
    [SerializeField] Material disabledMat;
    [SerializeField] Text text;
    [SerializeField] string textIN;

    private MeshRenderer padRenderer;


    public bool isActive;

    void Start()
    {
        isActive = true;

        padRenderer = gameObject.GetComponent<MeshRenderer>();
    }


    private void Update()
    {
        if (isActive) { padRenderer.material = activeMat; }
        else { padRenderer.material = disabledMat; }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.name == "PlayerSphere" && isActive)
        {
            isActive = false;
            text.text = textIN;
        }



    }

    public bool boostActivated()
    {
        var tempActive = isActive;
        isActive = false;

        return tempActive;
    }
}
