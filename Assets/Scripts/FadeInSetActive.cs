using UnityEngine;
using System.Collections;

public class FadeInSetActive : MonoBehaviour
{

    public GameObject fadeInScreen;
    public GameObject fadeOutScreen;

    public bool fadeIn;
    public bool fadeOut;
    public bool waiting;
    public bool fadeInMenu;

    public float timeSeconds = 3f;

    void Start()
    {
        fadeIn = true;
        waiting = false;

        FadeIn();

    }

    private void FixedUpdate()
    {
        FadeIn();
        FadeOut();
    }

    public void DelayedAction()
    {
        StartCoroutine(ExecuteAfterDelay());
    }

    IEnumerator ExecuteAfterDelay()
    {
        //Debug.Log("Starting delay...");

        // Wait for 3 seconds using WaitForSeconds
        if (fadeInMenu == true)
        {
            timeSeconds = 2.5f;
        }
        else
        {
            timeSeconds = 3f;
        }

        if (waiting == false)
        {
            waiting = true;
            yield return new WaitForSeconds(3f);
        }
        
        

        //Debug.Log("3 seconds have passed! Executing action now.");
        // Put the code you want to execute after the delay here
        if(fadeIn == true)
        {
            fadeInScreen.SetActive(false);
            fadeIn = false;
        }
        if(fadeOut == true)
        {

            fadeOutScreen.SetActive(false);
            fadeOut = false;
            
        }
        if(fadeInMenu == true)
        {
            fadeInScreen.SetActive(false);
            fadeInMenu = false;
        }

        timeSeconds = 3f;
        waiting = false;
    }

    private void Awake()
    {
        DelayedAction();
    }

    public void FadeIn()
    {
        
        if (fadeInScreen != null && fadeIn == true && waiting == false)
        {
            //waiting = false;
            fadeInScreen.SetActive(true);
            DelayedAction();

        }
        if (fadeInScreen != null && fadeInMenu == true)
        {
            fadeInScreen.SetActive(true);
            DelayedAction();
        }
    }

    public void FadeOut()
    {

        if (fadeOutScreen != null && fadeOut == true && waiting == false)
        {
            //waiting = false;
            fadeOutScreen.SetActive(true);
            DelayedAction();

        }
    }
}