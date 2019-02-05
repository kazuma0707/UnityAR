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
    public class AppTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;

        public GameObject skin;//�L�����N���̃I�u�W�F�N�g
        private Animator skinAnimator;
        public GameObject Portal;//�G�t�F�N�g�I�u�W�F�N�g
        [SerializeField]
        private GameObject canvas;
        public bool isOnceFlag = false;//�}�[�J��ǂݍ��񂾍ۂɈ�񂾂�true�ɂ��邽�߂̃t���O
        private bool isMoving = false;
        float Animtime = 0.0f;//Walk�̍Đ�����
        const float EndPos = 1.0f;//�Đ��I�����̍��W

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            Animtime = 0.0f;
            isOnceFlag = false;
            this.Portal.SetActive(true);
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            skinAnimator = skin.GetComponent<Animator>();
                
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
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

        private void Update()
        {
            if (isOnceFlag)
            {
                canvas.SetActive(true);
                isMoving = true;
                iTween.MoveTo(this.skin, iTween.Hash("z", 1.0f, "time", 3.0f,
                    "oncomplete", "OnCompleteHandler","oncompletetarget",this.gameObject, "EaseType", iTween.EaseType.easeInOutQuart));

                //skin�̍��WZ��1.0f�������ꍇ
                if (isMoving)
                {
                    skinAnimator.SetBool("Walk", true);
                }
               
            }

        }

        private void OnCompleteHandler()
        {
            isMoving = false;
            skinAnimator.SetBool("Walk", false);//�����A�j���[�V�������~
            this.Portal.SetActive(false);//�o���G�t�F�N�g���\��
            isOnceFlag = false;
        }

        #region PRIVATE_METHODS

        /// <summary>
        /// �}�[�J���������Ƃ��Ƀg���b�L���O����֐�
        /// </summary>
        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            isOnceFlag = true;
            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
           
            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

        }

        
        #endregion // PRIVATE_METHODS
    }
}
