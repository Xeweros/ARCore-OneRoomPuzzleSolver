using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySound : MonoBehaviour
{
    public float m_fDelaySound = 9.5f;
    private float m_fActualDelay = 0.0f;
    public AudioClip m_auClipKeySparkelSound;

    private void Update()
    {
        m_fActualDelay -= Time.deltaTime;
        if (m_fActualDelay <= 0.0f)
        {
            m_fActualDelay = m_fDelaySound;
            AudioSource.PlayClipAtPoint(m_auClipKeySparkelSound, transform.position, 0.15f);
        }
    }
}
