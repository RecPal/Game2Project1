using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Rigidbody playerRB;

    [Header("What is the cutoff velocity")]
    public float velocityCutoff = 10;
    public float currentVelocity;

    [Header("How long do you have to hold the velocity")]
    [SerializeField] float timeHold = 5;
    [SerializeField] float currentTime;

    [Header("Player exit material and light object")]
    [SerializeField] Material playerEndMat;
    [SerializeField] Light playerLight;

    [Header("Which scene should be loaded next?")]
    [SerializeField] string nextSceneName;
    [SerializeField] float transitionTime;
    [SerializeField] float transitionHangTime;

    private bool readyToTransition;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Rigidbody>();
        playerLight = GameObject.FindGameObjectWithTag("Player").GetComponent<Light>();

        playerLight.enabled = false;
        playerLight.intensity = 0.0f;
        readyToTransition = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = playerRB.velocity.magnitude;

        if (currentTime > timeHold)
        {
            endGameTransition();
        }

        if ( currentVelocity > velocityCutoff )
        {
            currentTime += Time.deltaTime;
        } else
        {
            currentTime = 0.0f;
        }
    }

    void endGameTransition()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>().material = playerEndMat;
        playerLight.enabled = true;
        
        if (transitionTime > 0)
        {
            transitionTime -= Time.deltaTime;

            playerLight.intensity += Time.deltaTime * 2;
        }

        if (transitionTime < 0) { 
            playerRB.gameObject.GetComponent<MeshRenderer>().enabled = false;
            playerLight.intensity = 0;
            playerRB.gameObject.isStatic = true;
            readyToTransition = true;
        }

        if (transitionHangTime > 0 && readyToTransition == true)
        {
            transitionHangTime -= Time.deltaTime;
        }

        if (transitionHangTime < 0)
        {
            SceneManager.LoadScene(nextSceneName);
        }

        
    }
}
