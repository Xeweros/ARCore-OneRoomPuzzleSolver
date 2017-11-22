using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroyed : MonoBehaviour
{
    public bool m_bIsActive = true;
    public float m_fDelay = 7.5f;

    private void Start()
    {
        if (m_bIsActive)
        {
            Destroy(gameObject, m_fDelay);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
