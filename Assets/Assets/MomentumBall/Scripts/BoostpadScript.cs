using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostpadScript : MonoBehaviour
{
    [SerializeField] Material activeMat;
    [SerializeField] Material disabledMat;

    [SerializeField] float rechargeTime;
    [SerializeField] float trueRechargeTime;

    private MeshRenderer padRenderer;
    

    public bool isActive;

    void Start()
    {
        isActive = true;

        padRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        if (isActive) { padRenderer.material = activeMat;    }
        else          { padRenderer.material = disabledMat;  }
    }

    private void Update()
    {
        if (trueRechargeTime > 0)
        {
            trueRechargeTime -= Time.deltaTime;
        }

        if (trueRechargeTime <= 0.0f )
        {
            isActive = true;
        }
    }

    public bool boostActivated()
    {
        var tempActive = isActive;
        isActive = false;

        trueRechargeTime = rechargeTime;

        return tempActive;
    }
}
