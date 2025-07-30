
#region IMPORTS

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

#if GAMBIT_MATHHELPER
using gambit.mathhelper;
#endif

using UnityEngine;

#endregion

/// <summary>
/// Rotate the hypercube a few times
/// </summary>
public class HypercubeSpin : MonoBehaviour, INeuroGuideInteractable
{
    public Animator animator;

    public float threshold = 0.99f;

    public string stateName;

    private int stateHash = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Convert the state name to a hash for performance
        int stateHash = Animator.StringToHash( stateName );

        PlayAnimationDirectly( stateName );
        animator.speed = 0f;
    }

    public void OnDataUpdate(float value)
    {
        PlayAnimationDirectly( stateName, 0, value);
    }

    //-----------------------------------------------------------------//
    public void PlayAnimationDirectly(string stateName, int layer = 0, float normalizedTime = 0f)
    //-----------------------------------------------------------------//
    {
        if (animator != null && animator.gameObject.activeSelf && animator.HasState(0, stateHash ) )
        {
            animator.Play(stateName, 0, normalizedTime);
        }

    } //END PlayAnimationDirectly
}