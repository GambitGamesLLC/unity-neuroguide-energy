using System.Collections;
using System.Collections.Generic;
#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif
using UnityEngine;

public class ShowHideLogo : MonoBehaviour
{
    public GameObject logo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logo.SetActive(false);
    }

    // Update is called once per frame
    public void OnDataUpdate(NeuroGuideManager.NeuroGuideSystem system)
    //------------------------------------------------------------------------//
    {
        if (system.currentNormalizedAverageValue > .95f)
        {
            logo.SetActive(true);
        }
        else
        {
            logo.SetActive(false);
        }

    } //END OnDataUpdate Method
}
