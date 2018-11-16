//-----------------------------------------------------------------------
// <copyright file="HelloARController.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.UI;

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif
    
    /// <summary>
    /// Controls the HelloAR example.
    /// </summary>
    public class HelloARController : MonoBehaviour
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR background).
        /// </summary>
        public Camera FirstPersonCamera;
        public Text _debugText;
        /// <summary>
        /// A prefab for tracking and visualizing detected planes.
        /// </summary>
        public GameObject DetectedPlanePrefab;
        // A model to place when a raycast from a user touch hits a plane.
        public GameObject AndyAndroidPrefab;
        //ユニティちゃんのオブジェクト
        [Header("生成するキャラクター")]
        public GameObject UnityChanPrefab;
        public GameObject GetCreateCharcter {
            get { return unityChanObject; }
            
       }
        //A gameobject parenting UI for displaying the "searching for planes" snackbar.
        public GameObject SearchingForPlaneUI;
        //The rotation in degrees need to apply to model when the Andy model is placed.
        private const float k_ModelRotation = 180.0f;
        //A list to hold all planes ARCore is tracking in the current frame. This object is used across
        // the application to avoid per-frame allocations.
        private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
        // True if the app is in the process of quitting due to an ARCore connection error, otherwise false
        private bool m_IsQuitting = false;

        //生成フラグ
        private bool IsCreate=false;

        //音声録音ボタン
        public GameObject m_voiceRecButton;
        private GameObject unityChanObject;
        //public Slider slider;
        //テレポートフェードオブジェクト
        [SerializeField]
        TeleportFadeSamplePlayer _Teleport;
        int cnt = 0;
        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        public void Update()
        {


            _UpdateApplicationLifecycle();
            //生成オブジェクトの回転
            if (unityChanObject != null)
            {
                var aim = Camera.main.transform.position - unityChanObject.transform.position;
                var look = Quaternion.LookRotation(aim);
                unityChanObject.transform.localRotation = look;

            }
            if (0 < Input.touchCount)
            {
                // タッチされている指の数だけ処理
                for (int i = 0; i < Input.touchCount; i++)
                {
                    // タッチ情報をコピー
                    Touch t = Input.GetTouch(i);
                    // タッチしたときかどうか
                    if (t.phase == TouchPhase.Began)
                    {
                        //タッチした位置からRayを飛ばす
                        Ray ray = Camera.main.ScreenPointToRay(t.position);
                        RaycastHit Rayhit = new RaycastHit();
                  
                        if (Physics.Raycast(ray, out Rayhit))
                        {
                            _debugText.text = Rayhit.collider.gameObject.name;

                            //Rayを飛ばしてあたったオブジェクトが自分自身だったら
                            if (Rayhit.collider.gameObject.name == "skin(Clone)")
                            {
                                _Teleport.StartFadeOut();
                                Destroy(Rayhit.collider.gameObject,5.0f);
                            }
                        }
                    }
                }
            }
            

            // Hide snackbar when currently tracking at least one plane.
            Session.GetTrackables<DetectedPlane>(m_AllPlanes);
            bool showSearchingUI = true;
            for (int i = 0; i < m_AllPlanes.Count; i++)
            {
                if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
                {
                    showSearchingUI = false;
                    break;
                }
            }

            SearchingForPlaneUI.SetActive(showSearchingUI);

            // If the player has not touched the screen, we are done with this update.
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }


            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;
    
            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {

                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
             
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    if (unityChanObject==null)
                    {
                        //ユニティちゃんの生成
                          unityChanObject = Instantiate(UnityChanPrefab, hit.Pose.position, hit.Pose.rotation);
                        MyCharDataManager.Instance.ReCreate(unityChanObject);
                        _Teleport.StartFadeIn();
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                        // Make Andy model a child of the anchor.
                        //   unityChanObject.transform.parent = anchor.transform;
                        //サウンドボタンの生成
                        //m_voiceRecButton.SetActive(true);
                        IsCreate = true;
                    }

                }
            }


        }

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                const int lostTrackingSleepTimeout = 15;
                Screen.sleepTimeout = lostTrackingSleepTimeout;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage("c.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                        message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }

}
