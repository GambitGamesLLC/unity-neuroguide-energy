#region IMPORTS

using UnityEngine;
using DG.Tweening;


#if GAMBIT_NEUROGUIDE
using gambit.neuroguide;
#endif

#if GAMBIT_MATHHELPER
using gambit.mathhelper;
#endif

#endregion


/// <summary>
/// Controls the transition flash material, which shows up when transition the cube to its complete state
/// </summary>
public class FullscreenTransitionFlash: MonoBehaviour, INeuroGuideInteractable
{
    #region PUBLIC - VARIABLES

    public Material flash_material;

    /// <summary>
    /// Normalized 0-1 value, what is the maximum value of flash intensity?
    /// </summary>
    public float flash_intensity = 0.9f;

    /// <summary>
    /// Normalized 0-1 value, how far into the experience should the user be before the flash_material begins showing?
    /// </summary>
    public float threshold = 0.9f;

    /// <summary>
    /// What normalized experience progress value should we reach before we hide the flash?
    /// </summary>
    public float thresholdHideFlash = 1.0f;

    /// <summary>
    /// How long should it take to hide the flash after we are told to stop it (by reaching the end of the experience)
    /// </summary>
    public float flashHide_Length = 1.50f;

    /// <summary>
    /// Normalized 0-1 value, after the flash is shown, how far back does the experience value have to lower before we allow the flash to show again?
    /// </summary>
    public float thresholdPreventRepeatedFlashUntilLowerThan = 0.9f;

    #endregion

    #region PRIVATE - VARIABLES

    /// <summary>
    /// Set to true when the flash has been shown, we use this to prevent the flash from showing when our experience progress dips below our threshold, to prevent the flash from showing up again
    /// </summary>
    private bool preventFlash = false;

    #endregion

    #region PUBLIC - START

    /// <summary>
    /// Unity lifecycle method
    /// </summary>
    //-----------------------------//
    public void Start()
    //-----------------------------//
    {

        if(flash_material != null)
        {
            flash_material.SetFloat( "_Opacity", 0f );
        }

    } //END Start

    #endregion

    #region PUBLIC - ON DATA UPDATE

    /// <summary>
    /// Updates the fullscreen transition flash based on our progress in the Neuroguide experience
    /// </summary>
    /// <param name="normalizedValue"></param>
    //------------------------------------------------------//
    public void OnDataUpdate( float normalizedValue )
    //------------------------------------------------------//
    {
        
        if( flash_material != null )
        {
            if(normalizedValue > threshold && normalizedValue != 1f)
            {
                //If we haven't shown the flash yet
                if(preventFlash == false)
                {
#if EXT_DOTWEEN
                    flash_material.DOFloat( MathHelper.Map( normalizedValue, threshold, 1f, 0f, flash_intensity ), "_Opacity", .1f );
#endif
                }

            }
            else if(normalizedValue == thresholdHideFlash )
            {
                preventFlash = true;
                flash_material.DOFloat( 0f, "_Opacity", flashHide_Length );
            }

            if(preventFlash == true)
            {
                if(normalizedValue < thresholdPreventRepeatedFlashUntilLowerThan)
                {
                    preventFlash = false;
                }
            }
        }

    } //END OnDataUpdate Method

    #endregion

} //END FullscreenTransitionFlash Class
