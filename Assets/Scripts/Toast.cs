using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifetime;

    void OnEnable()
    {
        StartCoroutine(TurnOff());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + (speed * Time.deltaTime));
    }

    //ABSTRACTION
    //Stop being alive
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
