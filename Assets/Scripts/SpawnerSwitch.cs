using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSwitch : MonoBehaviour
{
     public bool isOccupied = false;

    void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}