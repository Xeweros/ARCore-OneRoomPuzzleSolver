using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float m_fSpeed = 10.0f;

	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, m_fSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -m_fSpeed * Time.deltaTime);
        }
    }
}
