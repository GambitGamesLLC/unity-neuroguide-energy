using UnityEngine;

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

public class NestreLogo : MonoBehaviour, INeuroGuideAnimationExperienceInteractable
{
    #region PUBLIC - VARIABLES

    public GameObject logo;

    #endregion

    #region PUBLIC - START

    /// <summary>
    /// Unity lifecycle function
    /// </summary>
    //------------------------------------//
    public void Start()
    //------------------------------------//
    {
        logo.SetActive(false);

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

    #region PUBLIC - NEUROGUIDE - ON DATA UPDATE

    /// <summary>
    /// Called when the NeuroGuide hardware updates
    /// </summary>
    /// <param name="system">The NeuroGuide system object</param>
    //------------------------------------------------------------------------//
    public void OnDataUpdate( float value )
    //------------------------------------------------------------------------//
    {

    } //END OnDataUpdate Method

    #endregion

    #region PUBLIC - NEUROGUIDE - ON ABOVE THRESHOLD

    /// <summary>
    /// Called when the NeuroGuideAnimationExperience has a score thats above the threshold value
    /// </summary>
    //------------------------------------//
    public void OnAboveThreshold()
    //------------------------------------//
    {
        logo.SetActive( true );

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
        logo.SetActive( false );

    } //END OnBelowThreshold

    #endregion


} //END NestreLogo Class