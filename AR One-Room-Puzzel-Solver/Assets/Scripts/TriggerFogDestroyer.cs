using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFogDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Fog"))
        {
            other.transform.GetChild(0).GetComponent<EllipsoidParticleEmitter>().emit = false;
            other.transform.GetChild(1).GetComponent<EllipsoidParticleEmitter>().emit = false;

            Destroy(other.gameObject, 6.0f);
        }
    }
}
