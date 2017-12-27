using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainMovement : MonoBehaviour
{
    public float m_fLeftPushRange = 0.0f;
    public float m_fRightPushRange = 0.0f;
    public float m_fRotationSpeed = 10.0f;

    private bool m_bIsRotatingToRight = true;
	
	// Update is called once per frame
	void Update ()
    {
		if (m_bIsRotatingToRight)
        {
            transform.Rotate(Vector3.right * m_fRotationSpeed * Time.deltaTime);

            if (transform.eulerAngles.x > m_fRightPushRange && !(transform.eulerAngles.x > (m_fRightPushRange * 2)))
                m_bIsRotatingToRight = !m_bIsRotatingToRight;
        }
        else if (!m_bIsRotatingToRight)
        {
            transform.Rotate(-Vector3.right * m_fRotationSpeed * Time.deltaTime);

            if (transform.eulerAngles.x > m_fRightPushRange && transform.eulerAngles.x < (360.0f + m_fLeftPushRange))
                m_bIsRotatingToRight = !m_bIsRotatingToRight;
        }
    }
}
