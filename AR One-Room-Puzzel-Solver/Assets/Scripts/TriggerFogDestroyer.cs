using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFogDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Fog"))
        {
            Destroy(other.gameObject);
        }
    }
}
