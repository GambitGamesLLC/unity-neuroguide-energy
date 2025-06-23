#if !GAMBIT_NEUROGUIDE
    //Class is unused if gambit.neuroguide package is missing
#else

/// <summary>
/// Keeps the Hypercube Pieces visual component up to date with the NeuroGuide hardware data
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

    /// <summary>
    /// The minimim value for the grunge texture
    /// </summary>
    public float grungeMin = 0.1f;

    /// <summary>
    /// THe maximum value for the grunge texture
    /// </summary>
    public float grungeMax = 1.1f;

    /// <summary>
    /// The material with the transition glow texture 
    /// </summary>
    public Material transition_material;

    /// <summary>
    /// The minimim value for the transition material
    /// </summary>
    public float transitionMin = 0f;

    /// <summary>
    /// THe maximum value for the transition material
    /// </summary>
    public float transitionMax = 20f;

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

        cube_pieces.SetActive( true );

        //We need to start at the split animation until we start reading data from the NeuroGuide hardware
        PlayAnimationDirectly( "Joining" );
        animator.speed = 0f;

        if(grunge_material != null)
        {
            grunge_material.SetFloat( "_AlphaClipping", grungeMin );
        }

        if(transition_material != null)
        {
            transition_material.SetFloat( "_TransitionAmount", transitionMin );
        }
        
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

        //Debug.Log( system.currentNormalizedAverageValue );
        PlayAnimationDirectly( "Joining", 0, value );

        //If we reach our max value, hide the cube pieces and only show the hypercube
        if( value >= threshold)
        {
            cube_pieces.SetActive( false );
        }
        else
        {
            cube_pieces.SetActive( true );
        }

        //Animate our cube grunge texture
#if GAMBIT_MATHHELPER
        if(grunge_material != null ) 
            grunge_material.SetFloat( "_AlphaClipping", MathHelper.Map( value, 0f, threshold, grungeMin, grungeMax ) );
#endif

        //Animate our cube transition material
#if GAMBIT_MATHHELPER
        if(transition_material != null )
            transition_material.SetFloat( "_TransitionAmount", MathHelper.Map( value, 0f, threshold, transitionMin, transitionMax ) );
#endif

    } //END OnDataUpdate Method

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