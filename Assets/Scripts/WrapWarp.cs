using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapWarp : MonoBehaviour
{
    [SerializeField] GameObject warpTarget;

    WrapWarp warpTrigger;

    private void Start()
    {
        warpTrigger = warpTarget.GetComponent<WrapWarp>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            Warp(other.gameObject);
        }
    }

    void Warp(GameObject warpObject)
    {
            warpObject.transform.position = new Vector3(warpTarget.transform.position.x,
                                            warpObject.transform.position.y,
                                            0);
    }
}
