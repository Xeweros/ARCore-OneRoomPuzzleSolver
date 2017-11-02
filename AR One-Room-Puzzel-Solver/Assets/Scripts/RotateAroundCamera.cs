using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class RotateAroundCamera : MonoBehaviour
{
    public Transform m_v3CameraPosition;
    public float m_fRotationSpeed = 1.0f;

    [SerializeField]
    private bool m_bIsHitObjectFocus = false;

    public void HitObjectIsFocus()
    {
        m_bIsHitObjectFocus = true;
    }

    public void HitObjectIsNotFocus()
    {
        m_bIsHitObjectFocus = false;
    }

    // Update is called once per frame
    void Update ()
    {
		if (m_bIsHitObjectFocus)
        {
            transform.Rotate(Vector3.up * m_fRotationSpeed * Time.deltaTime);
        }
	}
}
