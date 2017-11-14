using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.HelloAR;

public class IsCameraInCenter : MonoBehaviour
{
    public ScanningAndTrackingManager m_scrScanningManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("MainCamera"))
        {
            m_scrScanningManager.CameraEnterCenter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("MainCamera"))
        {
            m_scrScanningManager.CameraExitCenter();
        }
    }
}
