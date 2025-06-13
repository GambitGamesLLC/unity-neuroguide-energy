#if !GAMBIT_NEUROGUIDE
    //Class is unused if gambit.neuroguide package is missing
#else

/// <summary>
/// Keeps the Hypercube visual component up to date with the NeuroGuide hardware data
/// </summary>

#region IMPORTS

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

using UnityEngine;

#endregion

public class HyperCube : MonoBehaviour, INeuroGuideInteractable
{

    #region PUBLIC - VARIABLES

    public GameObject hypercube;

    #endregion

    #region PUBLIC START

    /// <summary>
    /// Unity lifecycle Start Method
    /// </summary>
    //---------------------------------//
    public void Start()
    //---------------------------------//
    {

        hypercube.SetActive( false );

    } //END Start

    #endregion

    #region PUBLIC - NEUROGUIDE - ON DATA UPDATE

    /// <summary>
    /// Called when the NeuroGuide hardware updates
    /// </summary>
    /// <param name="system">The NeuroGuide system object</param>
    //------------------------------------------------------------------------//
    public void OnDataUpdate( NeuroGuideManager.NeuroGuideSystem system )
    //------------------------------------------------------------------------//
    {
        if(system.averageValueBelowThreshold)
        {
            hypercube.SetActive( true );
        }
        else
        {
            hypercube.SetActive( false );
        }
        
    } //END OnDataUpdate Method

    #endregion

    #region PUBLIC - NEUROGUIDE - ON STATE UPDATE

    /// <summary>
    /// When the state of the NeuroGuide updates
    /// </summary>
    /// <param name="system">The NeuroGuide system object</param>
    //------------------------------------------------------------------------------//
    public void OnStateUpdate( NeuroGuideManager.NeuroGuideSystem system )
    //------------------------------------------------------------------------------//
    {
        
    } //END OnStateUpdate

    #endregion

} //END HyperCube Class

#endif