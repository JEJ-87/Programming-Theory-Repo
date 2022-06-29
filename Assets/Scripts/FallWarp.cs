using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWarp : MonoBehaviour
{
    [SerializeField] GameObject warpTarget;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            Warp(other.gameObject);
        }
    }

    void Warp(GameObject warpObject)
    {
        warpObject.transform.position = new Vector3(warpObject.transform.position.x,
                                        warpTarget.transform.position.y,
                                        0);
    }
}
