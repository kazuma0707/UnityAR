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

        public GameObject skin;//キャラクリのオブジェクト
        public  bool isOnceFlag=false;//マーカを読み込んだ際に一回だけtrueにするためのフラグ
        float Animtime=0.0f;//Walkの再生時間
        const float EndPos = 1.0f;//再生終了時の座標
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            Animtime = 0.0f;
            isOnceFlag = false;
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
            EmergenceCharEvent();


        }
        private void EmergenceCharEvent()
        {
            if (isOnceFlag)
            {
                //移動
                iTween.MoveTo(this.skin, iTween.Hash("z", 1.0f, "time", 3.0f, "EaseType", iTween.EaseType.easeInOutQuart));
                //DebugText.SetText = skin.transform.position.ToString();

                //Animtimeが3秒以上だった場合
                if (skin.transform.position.z == EndPos)
                {
                    skin.GetComponent<Animator>().SetBool("Walk", false);
                    isOnceFlag = false;
                }
                else
                {
                    //歩くアニメーション
                    skin.GetComponent<Animator>().SetBool("Walk", true);
                }
            }
        }

        #region PRIVATE_METHODS

        /// <summary>
        /// マーカを見つけたときにトラッキングする関数
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
