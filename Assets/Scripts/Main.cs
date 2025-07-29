#region IMPORTS

using System.Collections.Generic;
using UnityEngine;
using System;

#if EXT_DOTWEEN
using DG.Tweening;
#endif

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

#if GAMBIT_CONFIG
using gambit.config;
#endif

#if GAMBIT_PROCESS
using gambit.process;
#endif

#if UNITY_INPUT
using UnityEngine.InputSystem;
#endif

#if EXT_TOTALJSON
using Leguar.TotalJSON;
#endif

#endregion

/// <summary>
/// Primary entry point of the project
/// </summary>
public class Main : MonoBehaviour
{

    #region PUBLIC - VARIABLES

    /// <summary>
    /// Should we enable the debug logs?
    /// </summary>
    public bool logs = true;

    /// <summary>
    /// Should we enable the debug system for the NeuroGear hardware? This will enable keyboard events to control simulated NeuroGear hardware data spawned during the Create() method of NeuroGuideManager.cs
    /// </summary>
    public bool debug = true;

    /// <summary>
    /// How long should this experience last if the user was in a reward state continuously?
    /// </summary>
    public float length = 1f;

    /// <summary>
    /// UDP port address to listen to for NeuroGuide communication
    /// </summary>
    public string address = "127.0.0.1";

    /// <summary>
    /// UDP port to listen to for NeuroGuide communication
    /// </summary>
    public int port = 50000;

    #endregion

    #region PRIVATE - VARIABLES

    /// <summary>
    /// The config manager system instantiated at Start()
    /// </summary>
    private ConfigManager.ConfigManagerSystem configSystem;

    #endregion

    #region PUBLIC - START

    /// <summary>
    /// Unity lifecycle method
    /// </summary>
    //----------------------------------//
    public void Start()
    //----------------------------------//
    {
#if !EXT_DOTWEEN
        Debug.LogError( "Main.cs Start() Missing 'EXT_DOTWEEN' scripting define symbol and/or package" );
#endif
#if !GAMBIT_NEUROGUIDE
        Debug.LogError( "Main.cs Start() Missing 'GAMBIT_NEUROGUIDE' scripting define symbol and/or package" );
#endif
#if !GAMBIT_CONFIG
        Debug.LogError( "Main.cs Start() Missing 'GAMBIT_CONFIG' scripting define symbol and/or package" );
#endif
#if !EXT_TOTALJSON
        Debug.LogError( "Main.cs Start() Missing 'EXT_TOTALJSON' scripting define symbol and/or package" );
#endif
#if !GAMBIT_PROCESS
        Debug.LogError( "Main.cs Start() Missing 'GAMBIT_PROCESS' scripting define symbol and/or package" );
#endif

        LoadDataFromProcess();

    } //END Start Method

    #endregion

    #region PRIVATE - LOAD DATA FROM CONFIG - UPDATE CONFIG IF NEEDED

    /// <summary>
    /// Loads data that was passed into the process
    /// </summary>
    //-------------------------------------//
    private void LoadDataFromProcess()
    //-------------------------------------//
    {

#if GAMBIT_PROCESS

        List<string> keys = ProcessManager.ReadArgumentKeys();
        List<string> values = ProcessManager.ReadArgumentValues();

        if(keys == null || (keys != null && keys.Count == 0) ||
           values == null || (values != null && values.Count == 0) ||
           keys.Count != values.Count)
        {
            Debug.Log( "Skipping Reading Key-Values : Keys.Count = " + keys.Count + ", Values.Count = " + values.Count );

            for(int i = 0; i < keys.Count; ++i)
            {
                Debug.Log( "key : " + keys[ i ] );
            }

            for(int i = 0; i < values.Count; ++i)
            {
                Debug.Log( "value : " + values[i] );
            }
            
            CreateNeuroGuideManager();
            return;
        }

        for(int i = 0; i < keys.Count; ++i)
        {
            string key = keys[ i ];
            string value = values[ i ];

            if(key == "logs")
            {
                logs = bool.Parse( value );
            }
            else if(key == "debug")
            {
                debug = bool.Parse( value );
            }
            else if(key == "length")
            {
                length = int.Parse( value );
            }
            else if(key == "address")
            {
                address = value;
            }
            else if(key == "port")
            {
                port = int.Parse( value );
            }

        }

        if(logs)
        {
            Debug.Log( "logs : " + logs );
            Debug.Log( "debug : " + debug );
            Debug.Log( "length : " + length );
            Debug.Log( "address : " + address );
            Debug.Log( "port : " + port );
        }
        
#endif

    } //END LoadDataFromProcess Method

    #endregion

    #region PRIVATE - CREATE NEUROGUIDE MANAGER

    /// <summary>
    /// Creates the NeuroGuideManager
    /// </summary>
    //---------------------------------------------//
    private void CreateNeuroGuideManager()
    //---------------------------------------------//
    {

#if GAMBIT_NEUROGUIDE

        NeuroGuideManager.Create
        (
            //Options
            new NeuroGuideManager.Options()
            {
                showDebugLogs = logs,
                enableDebugData = debug,
                udpAddress = address,
                udpPort = port
            },

            //OnSuccess
            ( NeuroGuideManager.NeuroGuideSystem system ) => 
            {
                if( logs ) Debug.Log( "Main.cs CreateNeuroGuideManager() Successfully created NeuroGuideManager" );
                CreateNeuroGuideExperience();
            },

            //OnError
            LogError,

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

#endif

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

#if GAMBIT_NEUROGUIDE

        NeuroGuideExperience.Create
        (
            //Options
            new NeuroGuideExperience.Options()
            {
                showDebugLogs = logs,
                totalDurationInSeconds = length
            },

            //OnSuccess
            (NeuroGuideExperience.NeuroGuideExperienceSystem system)=>
            {
                if( logs ) Debug.Log( "Main.cs CreateNeuroGuideExperience() Successfully created NeuroGuideExperience" );
            },

            //OnFailed
            LogError,

            //OnDataUpdate
            (float value)=>
            {
                if( logs ) Debug.Log( "Main.cs CreateNeuroGuideExperience() Data Updated = " + value );
            }

        );

#endif

    } //END CreateNeuroGuideExperience Method

    #endregion

    #region PRIVATE - LOG WARNING

    /// <summary>
    /// Logs warning if the writing to the console log has been enabled
    /// </summary>
    /// <param name="warning"></param>
    //------------------------------------------//
    private void LogWarning( string warning )
    //------------------------------------------//
    {
        if(logs)
            Debug.LogWarning( warning );

    } //END LogWarning Method

    #endregion

    #region PRIVATE - LOG ERROR

    /// <summary>
    /// Logs errors if the writing to the console log has been enabled
    /// </summary>
    /// <param name="error"></param>
    //------------------------------------------//
    private void LogError( string error )
    //------------------------------------------//
    {
        if(logs)
            Debug.LogError( error );

    } //END LogError Method

    #endregion

} //END Main Class