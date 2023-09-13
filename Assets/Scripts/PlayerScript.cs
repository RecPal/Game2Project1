using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    public int coins;

    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Coins: " + Convert.ToString(coins);
    }

    public void OnCollisionEnter(Collision col)
    {
        
        print(col.collider.name);
        if(col.collider.tag == "CoinTag") {
            print("hit");
            Destroy(col.collider);
        }
    }
}
