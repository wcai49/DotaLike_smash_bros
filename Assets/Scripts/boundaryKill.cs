using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaryKill : MonoBehaviour
{
    Collider boundary;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hi");
    }
}
