/**************************************************
 * IThresholdInteractable
 * 
 * Interface class that contains methods to respond to NeuroGuide experience events called by the NeuroGuideExperience singleton
 ***************************************************/

using UnityEngine;

namespace gambit.neuroguide
{

    /// <summary>
    /// Interface class, contains methods to respond to NeuroGuide hardware events
    /// </summary>
    public interface IThresholdInteractable
    {

        /// <summary>
        /// Changes the threshold value used to change state
        /// </summary>
        void SetThreshold( float threshold );

    } //END IThresholdInteractable Interface Class

}