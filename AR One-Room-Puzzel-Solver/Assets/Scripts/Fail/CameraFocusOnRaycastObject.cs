using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.HelloAR;

public class CameraFocusOnRaycastObject : MonoBehaviour 
{
    public GameObject m_goFocusObject;
    public ScanningAndTrackingManager m_scrScanningManager;

    private bool m_bIsValid = false;
    private RaycastHit m_HitObject;
	
	// Update is called once per frame
	void Update ()
    {
        m_bIsValid = Physics.Raycast(transform.position, transform.forward, out m_HitObject);
        Debug.DrawLine(transform.position, transform.forward);

        if (m_bIsValid && m_HitObject.transform.gameObject == m_goFocusObject)
        {
            m_scrScanningManager.HitObjectIsFocus();
        }

        if (!m_bIsValid || m_HitObject.transform.gameObject != m_goFocusObject)
        {
            m_scrScanningManager.HitObjectIsNotFocus();
        }
    }
}
