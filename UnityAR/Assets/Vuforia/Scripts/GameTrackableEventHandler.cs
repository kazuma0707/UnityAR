/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    /// 
    public class GameTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        public GameObject Canvas;
        [SerializeField]
        Renderer[] rendererComponents;
        //Collider[] colliderComponents;
        public bool trackingFlag { get; set; }             //  �g���b�L���O�������������ǂ����̃t���O
        private bool onceTrakingFlag;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
            //colliderComponents = GetComponentsInChildren<Collider>(true);
    

        }
        private void Update()
        {
            if (rendererComponents==null)
            {
                rendererComponents = GetComponentsInChildren<Renderer>(true);

            }

        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS
        #region PRIVATE_METHODS

        /// <summary>
        /// �}�[�J���������Ƃ��Ƀg���b�L���O����֐�
        /// </summary>
        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            //Canvas.SetActive(true);

            // Enable rendering:
           // if(!rendererComponents[rendererComponents.Length].enabled)
            {
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = true;
                }
            }
            // Enable colliders:
            //if(!colliderComponents[colliderComponents.Length].enabled)
            {
                foreach (Collider component in colliderComponents)
                {
                    component.enabled = true;
                }
            }
        

            // Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

            // �g���b�L���O�������Ƀt���O��true�ɂ���
            onceTrakingFlag = true;
        }

    



        private void OnTrackingLost()
        {

            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
           Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
           // if (rendererComponents[rendererComponents.Length].enabled)
            {
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = false;
                }
            }
            // Enable colliders:
            //if (colliderComponents[colliderComponents.Length].enabled)
            {
                foreach (Collider component in colliderComponents)
                {
                    component.enabled = true;
                }
            }
            // Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");


        }

        
        // �g���b�L���O�̏�ԗp�t���O�̎擾
        public bool GetTrackingFlag()
        {
            return trackingFlag;
        }

        #endregion // PRIVATE_METHODS
    }
}
