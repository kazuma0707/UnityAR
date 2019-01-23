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
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;

        public GameObject skin;//�L�����N���̃I�u�W�F�N�g
        private Animator skinAnimator;
        public GameObject Portal;//�G�t�F�N�g�I�u�W�F�N�g
        public  bool isOnceFlag=false;//�}�[�J��ǂݍ��񂾍ۂɈ�񂾂�true�ɂ��邽�߂̃t���O
        float Animtime=0.0f;//Walk�̍Đ�����
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
                iTween.MoveTo(this.skin, iTween.Hash("z", 1.0f, "time", 3.0f, "EaseType", iTween.EaseType.easeInOutQuart));

                //skin�̍��WZ��1.0f�������ꍇ
                if (skin.transform.position.z != EndPos)
                {
                   skinAnimator.SetBool("Walk", true);
                }
                else
                {
                    //�����A�j���[�V����
                    skinAnimator.SetBool("Walk", false);//�����A�j���[�V�������~
                    this.Portal.SetActive(false);//�o���G�t�F�N�g���\��
                    isOnceFlag = false;
                    if (!skinAnimator.GetBool("Walk"))
                        skinAnimator.SetBool("Pose5", true);
                }
            }

        }
        private void EmergenceCharEvent()
        {
            
        }

        #region PRIVATE_METHODS

        /// <summary>
        /// �}�[�J���������Ƃ��Ƀg���b�L���O����֐�
        /// </summary>
        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            MyCharDataManager.Instance.ReCreate(skin);
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

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
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

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        
        #endregion // PRIVATE_METHODS
    }
}
