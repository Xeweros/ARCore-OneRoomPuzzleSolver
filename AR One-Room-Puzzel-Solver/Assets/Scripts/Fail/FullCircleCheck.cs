using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.HelloAR;

public class FullCircleCheck : MonoBehaviour
{
    public GameObject m_goFocusObject;
    public ScanningAndTrackingManager m_scrScanningManager;

    [SerializeField]
    private bool m_HasLeftStartPosition = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == m_goFocusObject && m_HasLeftStartPosition)
        {
            Debug.Log("FullCircle");
            m_scrScanningManager.FullCircleCompleted();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject == m_goFocusObject)
        {
            m_HasLeftStartPosition = true;
        }
    }
}
