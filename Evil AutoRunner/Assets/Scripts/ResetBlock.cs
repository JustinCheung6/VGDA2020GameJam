using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Block"))
        {
            c.gameObject.SetActive(false);
        }
    }
}
