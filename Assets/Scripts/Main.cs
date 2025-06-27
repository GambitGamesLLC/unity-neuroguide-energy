#region IMPORTS

using UnityEngine;
using System.Text.RegularExpressions;

#if EXT_DOTWEEN
using DG.Tweening;
#endif

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

#if GAMBIT_CONFIG
using gambit.config;
#endif

#if UNITY_INPUT
using UnityEngine.InputSystem;
using System;

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
    /// Should we enable the NeuroGuideManager debug logs?
    /// </summary>
    [NonSerialized]
    public bool logs = true;

    /// <summary>
    /// Should we enable the debug system for the NeuroGear hardware? This will enable keyboard events to control simulated NeuroGear hardware data spawned during the Create() method of NeuroGuideManager.cs
    /// </summary>
    [NonSerialized]
    public bool debug = true;

    /// <summary>
    /// How long should this experience last if the user was in a reward state continuously?
    /// </summary>
    [NonSerialized]
    public float experienceLengthInSeconds = 5f;

    /// <summary>
    /// Path to store the configuration file for this neuroguide experience. Can contain environment variables, and can contain escaped character sequences like \\ or \n
    /// </summary>
    [NonSerialized]
    public string configPath = "%LOCALAPPDATA%\\M3DVR\\BuildingBlocks\\config.json";

    /// <summary>
    /// UDP port address to listen to for NeuroGuide communication
    /// </summary>
    [NonSerialized]
    public string address = "127.0.0.1";

    /// <summary>
    /// UDP port to listen to for NeuroGuide communication
    /// </summary>
    [NonSerialized]
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
#if !EXT_TOTALJSON
        Debug.LogError( "Main.cs Start() Missing 'EXT_TOTALJSON' scripting define symbol and/or package" );
#endif

        ConvertConfigPath();
        CreateConfigManager();

    } //END Start Method

#endregion

    #region PRIVATE - CONVERT CONFIG PATH

    /// <summary>
    /// Converts the config file path to expand environment variables, also unescapes character sequences like \\ or \n
    /// </summary>
    //---------------------------------------------//
    private void ConvertConfigPath()
    //---------------------------------------------//
    {
        if( string.IsNullOrEmpty(configPath) )
        {
            Debug.LogError( "Main.cs ConvertConfigPath() configPath is null or empty. Unable to continue with experience. Please provide a path to store and retrieve the configuration file from" );
            return;
        }

        //Unescape the string
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable CS0168
        try
        {
            configPath = Regex.Unescape( configPath );
        }
        catch(Exception e)
        {
            //LogWarning( e.ToString() );
        }
#pragma warning restore CS0168
#pragma warning restore IDE0059 // Unnecessary assignment of a value

        //Expand any environment variables for the configPath
        configPath = System.Environment.ExpandEnvironmentVariables( configPath );


    } //END ConvertConfigPath Method

    #endregion

    #region PRIVATE - CREATE CONFIG MANAGER

    /// <summary>
    /// Instantiates the config manager and loads a config file that has values that change this experience
    /// </summary>
    //----------------------------------//
    public void CreateConfigManager()
    //----------------------------------//
    {
        ConfigManager.Create
        (
            new ConfigManager.Options()
            {
                path = configPath,
                showDebugLogs = logs
            },

            (ConfigManager.ConfigManagerSystem system)=>
            {
                configSystem = system;

#if EXT_TOTALJSON
                //If the config file doesn't exist, spawn it and grab the data from it
                if(!ConfigManager.DoesFileExist( configPath ))
                {
                    ConfigManager.ReplaceFileUsingResources( configSystem, "config", SetVariablesFromConfig, LogError );
                }
                else
                {
                    //Otherwise just grab the config json data
                    ConfigManager.ReadFileContents( configSystem, SetVariablesFromConfig, LogError );
                }
#endif

                return;
            },

            LogError

        );

    } //END CreateConfigManager Method

    #endregion

    #region PRIVATE - SET VARIABLES FROM CONFIG

#if EXT_TOTALJSON

    /// <summary>
    /// Pull variables from the config file to use as our experience variables
    /// </summary>
    /// <param name="json"></param>
    //---------------------------------------------------//
    private void SetVariablesFromConfig( JSON json )
    //---------------------------------------------------//
    {
        //How many variables do we need to wait for to load?
        int isReady = 0;
        int waitForCount = 5;


        //Set the 'address' variable, should be within the 'communication' object and the 'address' key
        ConfigManager.GetNestedString
        (
            configSystem,
            new string[ ] { "communication", "address" },
            ( string value ) =>
            {
                address = value;
                isReady++;
                if( isReady == waitForCount )
                {
                    CreateNeuroGuideManager();
                }
            },
            LogError
        );

        //Set the 'port' variable, should be within the 'communication' object and the 'port' key
        ConfigManager.GetNestedInteger
        (
            configSystem,
            new string[ ] { "communication", "port" },
            ( int value ) =>
            {
                port = value;
                isReady++;
                if(isReady == waitForCount)
                {
                    CreateNeuroGuideManager();
                }
            },
            LogError
        );

        //Set the 'logs' variable, should be within the 'experience' object and 'logs' key
        ConfigManager.GetNestedBool
        (
            configSystem,
            new string[ ] { "experience", "logs" },
            (bool value)=>
            {
                logs = value;
                isReady++;
                if(isReady == waitForCount)
                {
                    CreateNeuroGuideManager();
                }
            },
            (string error)=>
            {
                LogWarning( error );
                isReady++;
                if(isReady == waitForCount)
                {
                    CreateNeuroGuideManager();
                }
            }
        );

        //Set the 'debug' variable, should be within the 'experience' object and 'debug' key
        ConfigManager.GetNestedBool
        (
            configSystem,
            new string[ ] { "experience", "debug"},
            (bool value)=>
            {
                debug = value;
                isReady++;
                if(isReady == waitForCount)
                {
                    CreateNeuroGuideManager();
                }
            },
            ( string error ) =>
            {
                LogWarning( error );
                isReady++;
                if(isReady == waitForCount)
                {
                    CreateNeuroGuideManager();
                }
            }
        );

        //Set the 'length' variable, should be within the 'experience' object and the 'length' key
        ConfigManager.GetNestedInteger
        (
            configSystem,
            new string[ ] { "experience", "length" },
            (int value)=>
            {
                experienceLengthInSeconds = value;
                isReady++;
                if(isReady == waitForCount)
                {
                    CreateNeuroGuideManager();
                }
            },
            ( string error ) =>
            {
                LogWarning( error );
                isReady++;
                if(isReady == waitForCount)
                {
                    CreateNeuroGuideManager();
                }
            }
        );

    } //END SetVariablesFromConfig Method

#endif

#endregion

    #region PRIVATE - CREATE NEUROGUIDE MANAGER

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
            LogError,

            //OnDataUpdate
            (float value)=>
            {
                if( logs ) Debug.Log( "Main.cs CreateNeuroGuideExperience() Data Updated = " + value );
            }

        );

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