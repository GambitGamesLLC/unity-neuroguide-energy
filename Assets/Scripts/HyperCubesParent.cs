#region IMPORTS

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

#if EXT_DOTWEEN
using DG.Tweening;
#endif

using UnityEngine;

#endregion

/// <summary>
/// Controls the hyper cube parent gameobject. Causes it to rotate
/// </summary>
public class HyperCubesParent : MonoBehaviour, INeuroGuideAnimationExperienceInteractable
{

    #region PUBLIC - VARIABLES

    public GameObject hypercubeParent;

    #endregion

    #region PRIVATE - VARIABLES

    // A private field to store the initial Y-axis rotation.
    private float _initialYRotation;

    // We can also store the initial X and Z to ensure the rotation is perfectly clean
    // on the Y-axis only, relative to the starting orientation.
    private float _initialXRotation;
    private float _initialZRotation;

    #endregion

    #region PUBLIC - START

    /// <summary>
    /// Call this method to capture the current rotation as the new starting point for the cycle.
    /// </summary>
    //---------------------------//
    public void Start()
    //---------------------------//
    {
        if(hypercubeParent == null)
        {
            Debug.LogWarning( "HyperCubesParent.cs hypercubeParent is null, unable to continue" );
            return;
        }

        Vector3 initialEulerAngles = hypercubeParent.transform.localEulerAngles;
        _initialXRotation = initialEulerAngles.x;
        _initialYRotation = initialEulerAngles.y;
        _initialZRotation = initialEulerAngles.z;

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
    /// Data update event for the NeuroGuide hardware
    /// </summary>
    /// <param name="system"></param>
    //-----------------------------------------------------------------------//
    public void OnDataUpdate( float value )
    //-----------------------------------------------------------------------//
    {

        if(hypercubeParent == null)
            return;

        // Calculate the target rotation by adding to the initial Y rotation.
        // A value of 0 results in the initial rotation.
        // A value of 1 results in the initial rotation + 360 degrees.
        //float targetYRotation = _initialYRotation + (value * 360f );

        // Get the current local rotation of the transform
        //Vector3 currentRotation = hypercubeParent.transform.localEulerAngles;

        // Create the target rotation vector, using the stored initial X and Z rotations.
        //Vector3 targetRotation = new Vector3( _initialXRotation, targetYRotation, _initialZRotation );

#if EXT_DOTWEEN
        // Use DOTween to rotate the transform to the target rotation over the specified duration.
        // We use RotateMode.FastBeyond360 to ensure it takes the shortest path, 
        // but adding 360 degrees will always result in a full circle.
        //hypercubeParent.transform.DOLocalRotate( targetRotation, 0.1f, RotateMode.Fast );
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

} //END HyperCubesParent Class