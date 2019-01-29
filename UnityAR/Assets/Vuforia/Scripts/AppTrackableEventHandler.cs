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

        public GameObject skin;//キャラクリのオブジェクト
        private Animator skinAnimator;
        public GameObject Portal;//エフェクトオブジェクト
        [SerializeField]
        private GameObject canvas;
        public bool isOnceFlag = false;//マーカを読み込んだ際に一回だけtrueにするためのフラグ
        private bool isMoving = false;
        float Animtime = 0.0f;//Walkの再生時間
        const float EndPos = 1.0f;//再生終了時の座標

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

                //skinの座標Zが1.0fだった場合
                if (isMoving)
                {
                    skinAnimator.SetBool("Walk", true);
                }
               
            }

        }

        private void OnCompleteHandler()
        {
            isMoving = false;
            skinAnimator.SetBool("Walk", false);//歩くアニメーションを停止
            this.Portal.SetActive(false);//出現エフェクトを非表示
            isOnceFlag = false;
        }

        #region PRIVATE_METHODS

        /// <summary>
        /// マーカを見つけたときにトラッキングする関数
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
