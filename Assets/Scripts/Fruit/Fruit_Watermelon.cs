using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//INHERITENCE
public class Fruit_Watermelon : Fruit
{
    //POLYMORPHISM
    protected override void FruitGet()
    {
        base.FruitGet();
        m_Audio.PlayOneShot(fruitClip);
        GameObject toast = fruitToast.GetPooledObject();
        toast.gameObject.SetActive(true);
        toast.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        TMP_Text toastText = toast.gameObject.GetComponent<TMP_Text>();
        toastText.text = "Watermelon!<br>+" + score.ToString();
    }
}
