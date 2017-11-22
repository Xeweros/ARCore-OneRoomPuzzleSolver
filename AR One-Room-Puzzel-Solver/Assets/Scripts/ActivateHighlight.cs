using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHighlight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Key"))
        {
            other.transform.gameObject.GetComponent<cakeslice.Outline>().enabled = true;
        }
    }
}
