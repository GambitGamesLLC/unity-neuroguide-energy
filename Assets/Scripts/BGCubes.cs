#region IMPORTS

using UnityEngine;

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

#if GAMBIT_MATHHELPER
using gambit.mathhelper;
#endif

#endregion

/// <summary>
/// Controls the Global Volume URP object based on NeuroGuide hardware values
/// </summary>
public class BGCubes: MonoBehaviour, INeuroGuideInteractable
{
    #region PUBLIC - VARIABLES

    /// <summary>
    /// Animator used to make the background cubes float and shake
    /// </summary>
    public Animator animator;

    private string shake_anim = "BG_Cubes_Shake_Loop";
    private string float_anim = "BG_Cubes_Float_Start";

    /// <summary>
    /// How far into the NeuroGuideExperience should we be before we cross the threshold? Uses a 0-1 normalized percentage value
    /// </summary>
    public float threshold = 0.85f;

    #endregion

    #region PRIVATE - VARIABLES

    #endregion

    #region PUBLIC - START

    /// <summary>
    /// Unity lifecycle method
    /// </summary>
    //-------------------------------//
    public void Start()
    //-------------------------------//
    {
        if(animator == null)
        {
            Debug.LogError( "BGCubes.cs Start() animator component is null, unable to continue" );
            return;
        }

        PlayAnimationDirectly( float_anim, 0, 0f );
        animator.speed = 0f;

    } //END Start Method

    #endregion

    #region PUBLIC - NEUROGUIDE - ON RECIEVING REWARD CHANGED

    /// <summary>
    /// Called when the NeuroGuide software starts or stops sending the user a reward
    /// </summary>
    /// <param name="isRecievingReward">Is the user currently recieiving a reward?</param>
    //--------------------------------------------------------------------//
    public void OnRecievingRewardChanged( bool isRecievingReward )
    //--------------------------------------------------------------------//
    {

    } //END OnRecievingRewardChanged

    #endregion

    #region PUBLIC - ON DATA UPDATE

    /// <summary>
    /// NeuroGuide hardware data has updated
    /// </summary>
    /// <param name="system"></param>
    //----------------------------------------------------------------------//
    public void OnDataUpdate( float value )
    //----------------------------------------------------------------------//
    {

        if(value >= threshold )
        {
            PlayAnimationDirectly( float_anim, 0, 0f );
            animator.speed = 1f;
        }
        else
        {
            CrossFadeAnimationDirectly( shake_anim, .25f, 0, 0f );
            animator.speed = 0f;
        }

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
            animator.Play( stateName, layer, normalizedTime );
        }

    } //END PlayAnimationDirectly Method

    #endregion

    #region PUBLIC - CROSS FADE ANIMATION

    /// <summary>
    /// Cross fade to one of our animation states over time
    /// </summary>
    //--------------------------------------------//
    public void CrossFadeAnimationDirectly( string stateName, float normalizedTransitionDuration, int layer, float normalizedTime )
    //--------------------------------------------//
    {
        if(animator != null && animator.gameObject.activeSelf)
        {
            animator.CrossFade( stateName, normalizedTransitionDuration, layer, normalizedTime );
        }

    } //END CrossFadeAnimationDirectly Method

    #endregion

} //END GlobalVolume.cs Class
