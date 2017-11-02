using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class CameraFocusOnRaycastObject : MonoBehaviour 
{
    public GameObject m_goFocusObject;
    public RotateAroundCamera m_scrRotateAroundCamera;

    private bool m_bIsValid = false;
    private RaycastHit m_HitObject;
	
	// Update is called once per frame
	void Update ()
    {
        m_bIsValid = Physics.Raycast(transform.position, Vector3.forward, out m_HitObject);
        Debug.DrawLine(transform.position, Vector3.forward);

        if (m_bIsValid && m_HitObject.transform.gameObject == m_goFocusObject)
        {
            m_scrRotateAroundCamera.HitObjectIsFocus();
        }

        if (!m_bIsValid)
        {
            m_scrRotateAroundCamera.HitObjectIsNotFocus();
        }
    }
}
