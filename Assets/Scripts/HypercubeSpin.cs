#if !GAMBIT_NEUROGUIDE
    //Class is unused if gambit.neuroguide package is missing
#else

/// <summary>
/// Rotate the hypercube a few times
/// </summary>

#region IMPORTS

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
using static UnityEngine.Rendering.DebugUI;

#endif

#if GAMBIT_MATHHELPER
using gambit.mathhelper;
#endif

using UnityEngine;

#endregion

public class HypercubeSpin : MonoBehaviour, INeuroGuideInteractable
{
    public Animator animator;

    public float threshold = 0.99f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayAnimationDirectly("pieces_spin");
        animator.speed = 0f;
    }

    public void OnDataUpdate(float value)
    {
        PlayAnimationDirectly("pieces_spin", 0, value);
    }

    public void PlayAnimationDirectly(string stateName, int layer = 0, float normalizedTime = 0f)
    //-----------------------------------------------------------------//
    {
        if (animator != null && animator.gameObject.activeSelf)
        {
            animator.Play(stateName, 0, normalizedTime);
        }

    } //END PlayAnimationDirectly
}

#endif