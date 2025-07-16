using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.Controls;

public class UIScript : MonoBehaviour
{
    [SerializeField] UIDocument uiDocument;
    private VisualElement root;


    void Start()
    {
        root = uiDocument.rootVisualElement;

        var settingsBtn = root.Q<VisualElement>("SettingsButton");
        var startBtn = root.Q<VisualElement>("TextContainer");
        var quitBtn = root.Q<VisualElement>("ExitButton");

        settingsBtn.RegisterCallback<ClickEvent>(OnSettingsEvent);
        startBtn.RegisterCallback<ClickEvent>(OnAnywhereEvent);
        quitBtn.RegisterCallback<ClickEvent>(OnQuitEvent);
    }


    private void OnSettingsEvent(ClickEvent evt)
    {
        Debug.Log("Settings Button Clicked");
    }

    private void OnQuitEvent(ClickEvent evt)
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }
    
    private void OnAnywhereEvent(ClickEvent evt)
    {
        Debug.Log("Anywhere Clicked");
    }
}
