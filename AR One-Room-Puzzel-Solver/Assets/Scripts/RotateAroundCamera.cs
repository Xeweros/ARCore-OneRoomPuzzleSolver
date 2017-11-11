using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.HelloAR;

public class RotateAroundCamera : MonoBehaviour
{
    public Transform m_v3CameraPosition;
    public float m_fRotationSpeed = 1.0f;

    public ScanningAndTrackingManager m_scrScanningManager;

    // Update is called once per frame
    void Update ()
    {
		if (m_scrScanningManager.IsFocus() && m_scrScanningManager.IsCameraInCenter())
        {
            transform.Rotate(Vector3.up * m_fRotationSpeed * Time.deltaTime);
        }
	}
}
