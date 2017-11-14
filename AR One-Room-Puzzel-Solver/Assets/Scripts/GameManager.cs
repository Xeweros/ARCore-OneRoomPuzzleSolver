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

namespace GoogleARCore.HelloAR
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Rendering;
    using GoogleARCore;
    using UnityEngine.UI;

    /// <summary>
    /// Controlls the HelloAR example.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera.
        /// </summary>
        public Camera m_firstPersonCamera;

        /// <summary>
        /// A prefab for tracking and visualizing detected planes.
        /// </summary>
        public GameObject m_trackedPlanePrefab;

        [Header("UI")]
        public GameObject m_searchingForPlaneUI;
        public GameObject m_WinUI;
        public Text m_KeysLeftText;

        [Header("Keys")]
        public GameObject m_goPrefabKey;
        public int m_nNumbersOfKeys = 3;
        private int m_nNumbersOfKeysLeft = 0;     

        [Header("Room Dimension")]
        public float m_fRoomWidth = 4.0f;
        public float m_fRoomDepth = 7.0f;
        public float m_fRoomHeight = 3.0f;

        [Header("Fog")]
        public GameObject m_goPrefabFog;
        public int m_nNumberOfFog = 100;

        private List<TrackedPlane> m_newPlanes = new List<TrackedPlane>();

        private List<TrackedPlane> m_allPlanes = new List<TrackedPlane>();

        private void Start()
        {
            Vector3 _v3SpawnPosition = new Vector3(0.0f, 0.0f, 0.0f);

            float _fWidth = 0.0f;
            float _fDepth = 0.0f;
            float _fHeight = 0.0f;

            for (int i = 0; i < m_nNumberOfFog; i++)
            {
                _fWidth = Random.Range(-m_fRoomWidth / 2.0f, m_fRoomWidth / 2.0f);
                _fDepth = Random.Range(0.0f, m_fRoomDepth);
                _fHeight = Random.Range(-m_fRoomHeight / 2.0f, m_fRoomHeight / 2.0f);

                _v3SpawnPosition = new Vector3(_fWidth, _fHeight, _fDepth);

                Instantiate(m_goPrefabFog, _v3SpawnPosition, Quaternion.identity);
            }

            m_nNumbersOfKeysLeft = m_nNumbersOfKeys;
            m_KeysLeftText.text = "Es sind noch " + m_nNumbersOfKeysLeft + " Schlüssel zu finden!";

            for (int i = 0; i < m_nNumbersOfKeys; i++)
            {
                _fWidth = Random.Range(-m_fRoomWidth / 2.0f, m_fRoomWidth / 2.0f);
                _fDepth = Random.Range(0.0f, m_fRoomDepth);
                _fHeight = Random.Range(-m_fRoomHeight / 2.0f, m_fRoomHeight / 2.0f);

                _v3SpawnPosition = new Vector3(_fWidth, _fHeight, _fDepth);

                Instantiate(m_goPrefabKey, _v3SpawnPosition, Quaternion.identity);
            }
        }

        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        public void Update ()
        {
            _QuitOnConnectionErrors();

            // The tracking state must be FrameTrackingState.Tracking in order to access the Frame.
            if (Frame.TrackingState != FrameTrackingState.Tracking)
            {
                const int LOST_TRACKING_SLEEP_TIMEOUT = 15;
                Screen.sleepTimeout = LOST_TRACKING_SLEEP_TIMEOUT;
                return;
            }

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Frame.GetNewPlanes(ref m_newPlanes);

            // Iterate over planes found in this frame and instantiate corresponding GameObjects to visualize them.
            for (int i = 0; i < m_newPlanes.Count; i++)
            {
                // Instantiate a plane visualization prefab and set it to track the new plane. The transform is set to
                // the origin with an identity rotation since the mesh for our prefab is updated in Unity World
                // coordinates.
                GameObject planeObject = Instantiate(m_trackedPlanePrefab, Vector3.zero, Quaternion.identity,
                    transform);
                planeObject.GetComponent<TrackedPlaneVisualizer>().SetTrackedPlane(m_newPlanes[i]);

                // Apply a random grid rotation.
                planeObject.GetComponent<Renderer>().material.SetFloat("_UvRotation", Random.Range(0.0f, 360.0f));
            }

            // Disable the snackbar UI when no planes are valid.
            bool showSearchingUI = true;
            Frame.GetAllPlanes(ref m_allPlanes);
            for (int i = 0; i < m_allPlanes.Count; i++)
            {
                if (m_allPlanes[i].IsValid)
                {
                    showSearchingUI = false;
                    break;
                }
            }

            m_searchingForPlaneUI.SetActive(showSearchingUI);

            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

            RaycastHit raycastHit;

            if (Physics.Raycast(m_firstPersonCamera.ScreenPointToRay(touch.position), out raycastHit))
            {
                if (raycastHit.transform.CompareTag("Door"))
                {
                    if (m_nNumbersOfKeysLeft <= 0)
                    {
                        m_WinUI.SetActive(true);
                    }
                }

                if (raycastHit.transform.CompareTag("Key"))
                {
                    m_nNumbersOfKeysLeft--;
                    if (m_nNumbersOfKeysLeft <= 0)
                        m_KeysLeftText.text = "Es sind keine Schlüssel mehr zu finden!";
                    else
                        m_KeysLeftText.text = "Es sind noch " + m_nNumbersOfKeysLeft + " Schlüssel zu finden!";
                }
            }

            TrackableHit hit;
            TrackableHitFlag raycastFilter = TrackableHitFlag.PlaneWithinBounds | TrackableHitFlag.PlaneWithinPolygon;

            if (Session.Raycast(m_firstPersonCamera.ScreenPointToRay(touch.position), raycastFilter, out hit))
            {
                
            }
        }

        /// <summary>
        /// Quit the application if there was a connection error for the ARCore session.
        /// </summary>
        private void _QuitOnConnectionErrors()
        {
            // Do not update if ARCore is not tracking.
            if (Session.ConnectionState == SessionConnectionState.DeviceNotSupported)
            {
                _ShowAndroidToastMessage("This device does not support ARCore.");
                Application.Quit();
            }
            else if (Session.ConnectionState == SessionConnectionState.UserRejectedNeededPermission)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                Application.Quit();
            }
            else if (Session.ConnectionState == SessionConnectionState.ConnectToServiceFailed)
            {
                _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
                Application.Quit();
            }
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        /// <param name="length">Toast message time length.</param>
        private static void _ShowAndroidToastMessage(string message)
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
