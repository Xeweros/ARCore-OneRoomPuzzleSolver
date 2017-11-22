using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysRotate : MonoBehaviour
{
    public float m_fRotationSpeed = 10.0f;

	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.up * m_fRotationSpeed * Time.deltaTime);
	}
}
