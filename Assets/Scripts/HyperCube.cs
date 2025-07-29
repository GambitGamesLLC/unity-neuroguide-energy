#if !GAMBIT_NEUROGUIDE
    //Class is unused if gambit.neuroguide package is missing
#else

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
/// Keeps the Hypercube visual component up to date with the NeuroGuide hardware data
/// </summary>
public class HyperCube : MonoBehaviour, INeuroGuideInteractable
{

    #region PUBLIC - VARIABLES

    public GameObject hypercube;
    public Animator animator;

    /// <summary>
    /// How far into the NeuroGuideExperience should we be before we cross the threshold? Uses a 0-1 normalized percentage value
    /// </summary>
    public float threshold = 0.85f;

    #endregion

    #region PUBLIC START

    /// <summary>
    /// Unity lifecycle Start Method
    /// </summary>
    //---------------------------------//
    public void Start()
    //---------------------------------//
    {

        hypercube.SetActive( false );
        //PlayAnimationDirectly("HypercubeAnim");
        //animator.speed = 0f;

    } //END Start

    #endregion

    #region PUBLIC - NEUROGUIDE - ON DATA UPDATE

    /// <summary>
    /// Called when the NeuroGuide hardware updates
    /// </summary>
    /// <param name="system">The NeuroGuide system object</param>
    //------------------------------------------------------------------------//
    public void OnDataUpdate( float value )
    //------------------------------------------------------------------------//
    {
        //PlayAnimationDirectly("HypercubeAnim", 0, value);

        if ( value >= threshold )
        {
            hypercube.SetActive( true );
        }
        else
        {
            hypercube.SetActive( false );
        }
        
    } //END OnDataUpdate Method

    #endregion
    public void PlayAnimationDirectly(string stateName, int layer = 0, float normalizedTime = 0f)
    //-----------------------------------------------------------------//
    {
        if (animator != null && animator.gameObject.activeSelf)
        {
            animator.Play(stateName, 0, normalizedTime);
        }

    } //END PlayAnimationDirectly

} //END HyperCube Class

#endif