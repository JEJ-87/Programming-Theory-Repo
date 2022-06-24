using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fruit_Cherry : Fruit
{
    protected override void FruitGet()
    {
        base.FruitGet();
        m_Audio.PlayOneShot(fruitClip);
        GameObject toast = fruitToast.GetPooledObject();
        toast.gameObject.SetActive(true);
        toast.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        TMP_Text toastText = toast.gameObject.GetComponent<TMP_Text>();
        toastText.text = "Cherry!<br>+" + score.ToString();
    }
}
