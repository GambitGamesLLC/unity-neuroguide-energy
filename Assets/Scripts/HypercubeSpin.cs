
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
public class HypercubeSpin : MonoBehaviour, INeuroGuideAnimationExperienceInteractable
{
    #region PUBLIC - VARIABLES

    public Animator animator;

    public float threshold = 0.99f;

    public string stateName;

    #endregion

    #region PRIVATE - VARIABLES

    private int stateHash = 0;

    #endregion

    #region PUBLIC - START

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Convert the state name to a hash for performance
        stateHash = Animator.StringToHash( stateName );

        PlayAnimationDirectly( stateName );
        animator.speed = 0f;
    }

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

    #region PUBLIC - NUEROGUIDE - ON DATA UPDATE

    public void OnDataUpdate(float value)
    {
        PlayAnimationDirectly( stateName, 0, value);
    }

    #endregion

    #region PUBLIC - NEUROGUIDE - ON ABOVE THRESHOLD

    /// <summary>
    /// Called when the NeuroGuideAnimationExperience has a score thats above the threshold value
    /// </summary>
    //------------------------------------//
    public void OnAboveThreshold()
    //------------------------------------//
    {

    } //END OnAboveThreshold

    #endregion

    #region PUBLIC - NEUROGUIDE - ON BELOW THRESHOLD

    /// <summary>
    /// Called when the NeuroGuideAnimationExperience has a score thats below the threshold value
    /// </summary>
    //-------------------------------------//
    public void OnBelowThreshold()
    //-------------------------------------//
    {

    } //END OnBelowThreshold

    #endregion

    #region PBLIC - PLAY ANIMATION DIRECTLY

    //-----------------------------------------------------------------//
    public void PlayAnimationDirectly(string stateName, int layer = 0, float normalizedTime = 0f)
    //-----------------------------------------------------------------//
    {
        if (animator != null && animator.gameObject.activeSelf && animator.HasState(0, stateHash ) )
        {
            animator.Play(stateName, 0, normalizedTime);
        }

    } //END PlayAnimationDirectly

    #endregion

} //END HypercubeSpin Class