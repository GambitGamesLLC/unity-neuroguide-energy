using UnityEngine;
using System.Collections;

public class FadeInSetActive : MonoBehaviour
{

    public GameObject fadeScreen;

    void Start()
    {

        if (fadeScreen != null)
        {
            fadeScreen.SetActive(true);

        }

    }
    public void DelayedAction()
    {
        StartCoroutine(ExecuteAfterDelay());
    }

    IEnumerator ExecuteAfterDelay()
    {
        //Debug.Log("Starting delay...");

        // Wait for 3 seconds using WaitForSeconds
        yield return new WaitForSeconds(3f);

        //Debug.Log("3 seconds have passed! Executing action now.");
        // Put the code you want to execute after the delay here
        fadeScreen.SetActive(false);
    }

    private void Awake()
    {
        DelayedAction();
    }
}