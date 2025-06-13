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

    public GameObject Hypercube;
    public GameObject Pieces_Hypercube;

    /// <summary>
    /// Should we enable the NeuroGuideManager debug logs?
    /// </summary>
    public bool logs = true;

    /// <summary>
    /// Should we enable the debug system for the NeuroGear hardware? This will enable keyboard events to control simulated NeuroGear hardware data spawned during the Create() method of NeuroGuideManager.cs
    /// </summary>
    public bool debug = true;

    /// <summary>
    /// How many cubes should we spawn? Each cube will be tied to a NeuroGuideData object
    /// </summary>
    public int entries = 1;

    /// <summary>
    /// What is the min value we should use for the local position possible to reach by the cube movement?
    /// </summary>
    public int min = -5;

    /// <summary>
    /// What is the max value we should use for the local position possible to reach by the cube movement?
    /// </summary>
    public int max = 5;

#if EXT_DOTWEEN
    /// <summary>
    /// What tween easing should we use?
    /// </summary>
    public Ease ease = Ease.OutBounce;
#endif

    /// <summary>
    /// How long should our tweens take?
    /// </summary>
    public int duration = 2;

    /// <summary>
    /// Should the starting value of our NeuroGuideData be randomized?
    /// </summary>
    public bool randomizeStartValue = true;

    /// <summary>
    /// The debug threshhold value to reach to be in the 'enabled' state
    /// </summary>
    public float threshhold_normalized = 0.5f;

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

    #region PUBLIC - UPDATE

    /// <summary>
    /// Unity lifecycle method
    /// </summary>
    //----------------------------------//
    public void Update()
    //----------------------------------//
    {

        DestroyOnDeleteKey();

    } //END Update Method

    #endregion

    #region PRIVATE - CREATE ON START

    /// <summary>
    /// Creates the NeuroGuideManager
    /// </summary>
    //---------------------------------------------//
    private void CreateNeuroGuideManager()
    //---------------------------------------------//
    {

        NeuroGuideManager.Create(
            new NeuroGuideManager.Options()
            {
                showDebugLogs = logs,
                enableDebugData = debug,
                debugNumberOfEntries = entries,
                debugMinCurrentValue = min,
                debugMaxCurrentValue = max,
#if EXT_DOTWEEN
                debugEaseType = ease,
#endif
                debugTweenDuration = duration,
                debugRandomizeStartingValues = randomizeStartValue
            },
            ( NeuroGuideManager.NeuroGuideSystem system ) => {
                Debug.Log( "Main.cs CreateNeuroGuideManager() Successfully created NeuroGuideManager and recieved system object... system.data.count = " + system.data.Count );

                SetHypercubeState( false );

            },
            ( string error ) => {
                Debug.LogWarning( error );
            },
            ( NeuroGuideManager.NeuroGuideSystem system ) =>
            {
                //Debug.Log( "NeuroGuideDemo CreateNeuroGuideManager() Data Updated" );

                if(system != null && system.data != null && system.data.Count > 0)
                {
                    Debug.Log( system.data[ 0 ].currentNormalizedValue );

                    //Get the overall value of the NeuroGuide, we'll use that instead of the individual nodes
                    if(system.data[ 0 ].currentNormalizedValue >= threshhold_normalized)
                    {
                        SetHypercubeState( true );
                    }
                    else
                    {
                        SetHypercubeState( false );
                    }
                }

            },
        ( NeuroGuideManager.NeuroGuideSystem system, NeuroGuideManager.State state ) =>
        {
            Debug.Log( "Main.cs CreateNeuroGuideManager() State changed to " + state.ToString() );
        } );

    } //END CreateNeuroGuideManager Method

    #endregion

    #region PRIVATE - DESTROY ON DELETE KEY PRESSED

    /// <summary>
    /// Destroy the NeuroGuideManager instance
    /// </summary>
    //--------------------------------------------//
    private void DestroyOnDeleteKey()
    //--------------------------------------------//
    {

#if UNITY_INPUT
        if(Keyboard.current.deleteKey.wasPressedThisFrame)
        {
            DestroyCubes();
            NeuroGuideManager.Destroy();
        }
#else
        if(Input.GetKeyUp( KeyCode.Delete ))
        {
            DestroyCubes();
            NeuroGuideManager.Destroy();
        }
#endif

    } //END DestroyOnDelete

    //-------------------------------//
    private void DestroyCubes()
    //-------------------------------//
    {

        if(NeuroGuideManager.system != null && NeuroGuideManager.system.data.Count > 0)
        {
            for(int i = 0; i < NeuroGuideManager.system.data.Count; i++)
            {
                GameObject go = GameObject.Find( "Cube: " + NeuroGuideManager.system.data[ i ].name );
                Destroy( go );
            }
        }

    } //END DestroyCubes

    #endregion

    #region SET HYPERCUBE STATE

    /// <summary>
    /// Sets the state of the hypercube as on or off. While off we show the smaller cube pieces
    /// </summary>
    /// <param name="enabled"></param>
    //-----------------------------------------------------//
    public void SetHypercubeState( bool enabled )
    //-----------------------------------------------------//
    {
        if(enabled)
        {
            Hypercube.SetActive( true );
            Pieces_Hypercube.SetActive( false );
        }
        else
        {
            Hypercube.SetActive( false );
            Pieces_Hypercube.SetActive( true );
        }

    } //END SetHypercubeState Method

    #endregion

} //END Main Class