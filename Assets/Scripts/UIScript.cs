using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;

public class UIScript : MonoBehaviour
{
    [SerializeField] UIDocument uiDocument;
    [SerializeField] FadeInSetActive fadeInOutScript;
    private VisualElement root;
    private VisualElement mainMenu;
    private VisualElement selectionMenu;


    void Start()
    {
        root = uiDocument.rootVisualElement;

        var settingsBtn = root.Q<VisualElement>("SettingsButton");
        var startBtn = root.Q<VisualElement>("TextContainer");
        var quitBtn = root.Q<VisualElement>("ExitButton");
        var exp1Btn = root.Q<VisualElement>("Exp1");
        var exp2Btn = root.Q<VisualElement>("Exp2");

        settingsBtn.RegisterCallback<ClickEvent>(OnSettingsEvent);
        startBtn.RegisterCallback<ClickEvent>(OnAnywhereEvent);
        quitBtn.RegisterCallback<ClickEvent>(OnQuitEvent);
        exp1Btn.RegisterCallback<ClickEvent>(OnExperience1);
        exp2Btn.RegisterCallback<ClickEvent>(OnExperience2);

        mainMenu = root.Q<VisualElement>("MainMenuContainer");
        selectionMenu = root.Q<VisualElement>("SelectionContainer");

        selectionMenu.SetEnabled(false);
    }


    private void OnSettingsEvent(ClickEvent evt)
    {
        Debug.Log("Settings Button Clicked");
    }

    private void OnQuitEvent(ClickEvent evt)
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    private void OnAnywhereEvent(ClickEvent evt)
    {
        Debug.Log("Anywhere Clicked");
        fadeInOutScript.fadeOut = true;
        DelayedAction();
    }

    private void OnExperience1(ClickEvent evt)
    {
        Debug.Log("Experience 1 Clicked");
    }
    private void OnExperience2(ClickEvent evt)
    {
        Debug.Log("Experience 2 Clicked");
        DelayedAction2();
    }

    public void DelayedAction()
    {
        StartCoroutine(ExecuteAfterDelay());
    }

    IEnumerator ExecuteAfterDelay()
    {
        //Debug.Log("Starting delay...");

        // Wait for 3 seconds using WaitForSeconds
        yield return new WaitForSeconds(2.5f);

        //SceneManager.LoadScene("Experience 2 - M3D");
        fadeInOutScript.fadeInScreen.SetActive(true);

        selectionMenu.style.display = DisplayStyle.Flex;
        selectionMenu.style.visibility = Visibility.Visible;
        selectionMenu.SetEnabled(true);

        mainMenu.style.display = DisplayStyle.None;
        mainMenu.style.visibility = Visibility.Hidden;
        mainMenu.SetEnabled(false);

        yield return new WaitForSeconds(2.5f);
        fadeInOutScript.fadeInScreen.SetActive(false);


    }
    public void DelayedAction2()
    {
        StartCoroutine(ExecuteAfterDelay2());
    }

    IEnumerator ExecuteAfterDelay2()
    {
        fadeInOutScript.fadeOutScreen.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Experience 2 - M3D");
    }
}
