using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fruit : MonoBehaviour
{
    [SerializeField] protected int score;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected ObjectPooler fruitToast;
    [SerializeField] protected AudioClip fruitClip;

    protected AudioSource m_Audio;

    // Start is called before the first frame update
    void Start()
    {
        m_Audio = fruitToast.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        FruitRotate();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FruitGet();
        }
    }

    protected void FruitRotate()
    {
        Vector3 target = new Vector3(0, transform.rotation.y + rotationSpeed, 0);
        transform.Rotate(target * Time.deltaTime);
    }

    protected virtual void FruitGet()
    {
        GameManager.instance.score += score;
        gameObject.SetActive(false);
    }
}
