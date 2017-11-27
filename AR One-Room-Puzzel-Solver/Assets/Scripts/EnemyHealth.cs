using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
    public int m_nHealth = 3;

    public void getDamaged()
    {
        m_nHealth--;
        if (m_nHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
