#region IMPORTS

#if EXT_DOTWEEN
using DG.Tweening;
#endif

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

#if UNITY_INPUT
using UnityEngine.InputSystem;
#endif

using UnityEngine;

#endregion

/// <summary>
/// Primary entry point of the project
/// </summary>
public class Main : MonoBehaviour
{

    #region PUBLIC - VARIABLES

    /// <summary>
    /// Should we enable the NeuroGuideManager debug logs?
    /// </summary>
    public bool logs = true;

    /// <summary>
    /// Should we enable the debug system for the NeuroGear hardware? This will enable keyboard events to control simulated NeuroGear hardware data spawned during the Create() method of NeuroGuideManager.cs
    /// </summary>
    public bool debug = true;

    /// <summary>
    /// How long should this experience last if the user was in a reward state continuously?
    /// </summary>
    public float experienceLengthInSeconds = 5f;

    #endregion

    #region PUBLIC - START

    /// <summary>
    /// Unity lifecycle method
    /// </summary>
    //----------------------------------//
    public void Start()
    //----------------------------------//
    {
        CreateNeuroGuideManager();

    } //END Start Method

    #endregion

    #region PRIVATE - CREATE ON START

    /// <summary>
    /// Creates the NeuroGuideManager
    /// </summary>
    //---------------------------------------------//
    private void CreateNeuroGuideManager()
    //---------------------------------------------//
    {

        NeuroGuideManager.Create
        (
            //Options
            new NeuroGuideManager.Options()
            {
                showDebugLogs = logs,
                enableDebugData = debug
            },

            //OnSuccess
            ( NeuroGuideManager.NeuroGuideSystem system ) => 
            {
                if( logs ) Debug.Log( "Main.cs CreateNeuroGuideManager() Successfully created NeuroGuideManager" );
                CreateNeuroGuideExperience();
            },

            //OnError
            ( string error ) => {
                if( logs ) Debug.LogWarning( error );
            },

            //OnDataUpdate
            ( NeuroGuideData data ) =>
            {
                //if( logs ) Debug.Log( "NeuroGuideDemo CreateNeuroGuideManager() Data Updated" );
            },

            //OnStateUpdate
            ( NeuroGuideManager.State state ) =>
            {
                if( logs ) Debug.Log( "Main.cs CreateNeuroGuideManager() State changed to " + state.ToString() );
            } );

    } //END CreateNeuroGuideManager Method

    #endregion

    #region PRIVATE - CREATE NEUROGUIDE EXPERIENCE

    /// <summary>
    /// Initializes a NeuroGuideExperience once the hardware is ready
    /// </summary>
    //---------------------------------------------//
    private void CreateNeuroGuideExperience()
    //---------------------------------------------//
    {

        NeuroGuideExperience.Create
        (
            //Options
            new NeuroGuideExperience.Options()
            {
                showDebugLogs = logs,
                totalDurationInSeconds = experienceLengthInSeconds
            },

            //OnSuccess
            (NeuroGuideExperience.NeuroGuideExperienceSystem system)=>
            {
                if( logs ) Debug.Log( "Main.cs CreateNeuroGuideExperience() Successfully created NeuroGuideExperience" );
            },

            //OnFailed
            (string error)=>
            {
                if( logs ) Debug.Log( error );
            },

            //OnDataUpdate
            (float value)=>
            {
                if( logs ) Debug.Log( "Main.cs CreateNeuroGuideExperience() Data Updated = " + value );
            }

        );

    } //END CreateNeuroGuideExperience Method

    #endregion

} //END Main Class