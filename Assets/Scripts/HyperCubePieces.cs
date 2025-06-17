#if !GAMBIT_NEUROGUIDE
    //Class is unused if gambit.neuroguide package is missing
#else

/// <summary>
/// Keeps the Hypercube Pieces visual component up to date with the NeuroGuide hardware data
/// </summary>

#region IMPORTS

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

#if GAMBIT_MATHHELPER
using gambit.mathhelper;
#endif

using UnityEngine;

#endregion

public class HyperCubePieces: MonoBehaviour, INeuroGuideInteractable
{

    #region PUBLIC - VARIABLES

    /// <summary>
    /// GameObject for the cube pieces
    /// </summary>
    public GameObject cube_pieces;

    /// <summary>
    /// Animator for the cube pieces
    /// </summary>
    public Animator animator;

    /// <summary>
    /// The material with the grunge texture 
    /// </summary>
    public Material grunge_material;

    #endregion

    #region PUBLIC START

    /// <summary>
    /// Unity lifecycle Start Method
    /// </summary>
    //---------------------------------//
    public void Start()
    //---------------------------------//
    {

        cube_pieces.SetActive( true );

        //We need to start at the split animation until we start reading data from the NeuroGuide hardware
        PlayAnimationDirectly( "Split" );
        animator.speed = 0f;
        
    } //END Start

    #endregion

    #region PUBLIC - NEUROGUIDE - ON DATA UPDATE

    /// <summary>
    /// Called when the NeuroGuide hardware updates
    /// </summary>
    /// <param name="system">The NeuroGuide system object</param>
    //------------------------------------------------------------------------//
    public void OnDataUpdate( NeuroGuideManager.NeuroGuideSystem system )
    //------------------------------------------------------------------------//
    {
        if(system == null || (system != null && system.data == null) || (system != null && system.data.Count == 0))
        {
            return;
        }

        //Debug.Log( system.currentNormalizedAverageValue );
        PlayAnimationDirectly( "Joining", 0, system.currentNormalizedAverageValue );

        //If we reach our max value, hide the cube pieces and only show the hypercube
        if(system.currentNormalizedAverageValue > .95f)
        {
            cube_pieces.SetActive( false );
        }
        else
        {
            cube_pieces.SetActive( true );
        }

        //Animate our cube grunge texture
#if GAMBIT_MATHHELPER
        grunge_material.SetFloat( "_AlphaClipping", MathHelper.Map( system.currentNormalizedAverageValue, 0f, 1f, 0.1f, 1.1f ) );
#endif

    } //END OnDataUpdate Method

    #endregion

    #region PUBLIC - NEUROGUIDE - ON STATE UPDATE

    /// <summary>
    /// When the state of the NeuroGuide updates
    /// </summary>
    /// <param name="system">The NeuroGuide system object</param>
    //------------------------------------------------------------------------------//
    public void OnStateUpdate( NeuroGuideManager.NeuroGuideSystem system )
    //------------------------------------------------------------------------------//
    {

    } //END OnStateUpdate

    // Call this method to play a specific animation based on a trigger

    #endregion

    #region PUBLIC - PLAY ANIMATION TRIGGER

    /// <summary>
    /// Switches to an animation clip based on the trigger name
    /// </summary>
    /// <param name="triggerName"></param>
    //----------------------------------------------------------//
    public void PlayAnimationTrigger( string triggerName )
    //----------------------------------------------------------//
    {
        if(animator != null)
        {
            animator.SetTrigger( triggerName );
        }

    } //END PlayAnimationTrigger

    #endregion

    #region PUBLIC - PLAY ANIMATION DIRECTLY

    /// <summary>
    /// Call this method to directly play an animation state
    /// </summary>
    /// <param name="stateName"></param>
    //-----------------------------------------------------------------//
    public void PlayAnimationDirectly( string stateName, int layer = 0, float normalizedTime = 0f )
    //-----------------------------------------------------------------//
    {
        if(animator != null && animator.gameObject.activeSelf)
        {
            animator.Play( stateName, 0, normalizedTime );
        }

    } //END PlayAnimationDirectly

    #endregion

} //END HyperCube Class

#endif