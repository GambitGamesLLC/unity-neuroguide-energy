#region IMPORTS

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
public class GlobalVolume : MonoBehaviour, INeuroGuideAnimationExperienceInteractable
{
    #region PUBLIC - VARIABLES

    /// <summary>
    /// The URP global volume object, needed to get reference to the Bloom variables in the Bloom Volume Profile
    /// </summary>
    public Volume globalVolume; // Assign your Volume in the Inspector

    /// <summary>
    /// The minimum amount of bloom we want to show when the NeuroGuide hardware is at its minimum value
    /// </summary>
    public float bloom_min = 10f;

    /// <summary>
    /// The maximum amount of bloom we want to show when the NeuroGuide hardware is at its maximum value
    /// </summary>
    public float bloom_max = .5f;

    #endregion

    #region PRIVATE - VARIABLES

    private Bloom bloomSettings;

    #endregion

    #region PUBLIC - START

    /// <summary>
    /// Unity lifecycle method
    /// </summary>
    //-------------------------------//
    public void Start()
    //-------------------------------//
    {

        if(globalVolume == null)
        {
            Debug.LogError( "GlobalVolume.cs Start() Global Volume not assigned!" );
            return;
        }

#if !GAMBIT_MATHHELPER
        Debug.LogError( "GlobalVolume.cs Start() 'GAMBIT_MATHHELPER' scripting define symbol is missing. Unable to continue" );
        return;
#endif

        // Get the Bloom settings from the Volume profile
        // Make sure the Bloom effect is added to your Volume Profile
        if(globalVolume.profile.TryGet( out bloomSettings ))
        {
            // Enable the override for the threshold property
            bloomSettings.threshold.overrideState = true;

            // Set a new threshold value (e.g., 0.5f)
            bloomSettings.threshold.value = bloom_min; //Default to min value

            //Debug.Log( $"URP Bloom Threshold set to: {bloomSettings.threshold.value}" );
        }
        else
        {
            Debug.LogError( "GlobalVolume.cs Start() Bloom settings not found in the Volume profile! Make sure it's added to the profile." );
        }

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
    /// NeuroGuide hardware data has updated
    /// </summary>
    /// <param name="system"></param>
    //----------------------------------------------------------------------//
    public void OnDataUpdate( float value )
    //----------------------------------------------------------------------//
    {

#if GAMBIT_MATHHELPER
        bloomSettings.threshold.value = MathHelper.Map( value, 0f, 1f, bloom_min, bloom_max );
#endif

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

} //END GlobalVolume.cs Class
