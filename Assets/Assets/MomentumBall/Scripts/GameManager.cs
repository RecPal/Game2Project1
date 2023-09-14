using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Rigidbody playerRB;

    [SerializeField] CanvasGroup fadeObj;

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
    private bool levelComplete;
    private bool transitionInProcess = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Rigidbody>();
        playerLight = GameObject.FindGameObjectWithTag("Player").GetComponent<Light>();

        playerLight.enabled = false;
        playerLight.intensity = 0.0f;
        readyToTransition = false;
        levelComplete = false;

        CanvasGroup temp = fadeObj;
        temp.alpha = 1.0f;

        StartCoroutine(FadeInLevel());
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = playerRB.velocity.magnitude;

        if (currentTime > timeHold || levelComplete)
        {
            levelComplete = true;
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

            playerLight.intensity += Time.deltaTime * 4;
        }

        if (transitionTime < 0 && !readyToTransition) { 
            playerRB.gameObject.GetComponent<MeshRenderer>().enabled = false;
            playerLight.intensity -= Time.deltaTime * 8;
            readyToTransition = true;
            
        }

        if (readyToTransition && !transitionInProcess)
        {
            StartCoroutine(FadeOutLevel());
            transitionInProcess = true;
        }

        if (readyToTransition == true)
        {
            playerRB.constraints = RigidbodyConstraints.FreezeAll;
            transitionHangTime -= Time.deltaTime;
            
        }

        if (transitionHangTime < 0)
        {
            SceneManager.LoadScene(nextSceneName);
        }

        
    }

    IEnumerator FadeInLevel()
    {
        CanvasGroup canvasGroup = fadeObj;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    IEnumerator FadeOutLevel()
    {
        CanvasGroup canvasGroup = fadeObj;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            //print(canvasGroup.alpha);
        }
        yield return null;
    }
}
