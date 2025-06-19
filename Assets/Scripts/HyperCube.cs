#if !GAMBIT_NEUROGUIDE
    //Class is unused if gambit.neuroguide package is missing
#else

#region IMPORTS

#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

using UnityEngine;

#endregion

/// <summary>
/// Keeps the Hypercube visual component up to date with the NeuroGuide hardware data
/// </summary>
public class HyperCube : MonoBehaviour, INeuroGuideInteractable
{

    #region PUBLIC - VARIABLES

    public GameObject hypercube;

    /// <summary>
    /// How far into the NeuroGuideExperience should we be before we cross the threshold? Uses a 0-1 normalized percentage value
    /// </summary>
    public float threshold = 0.85f;

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
    public void OnDataUpdate( float value )
    //------------------------------------------------------------------------//
    {
        if( value >= threshold )
        {
            hypercube.SetActive( true );
        }
        else
        {
            hypercube.SetActive( false );
        }
        
    } //END OnDataUpdate Method

    #endregion

} //END HyperCube Class

#endif