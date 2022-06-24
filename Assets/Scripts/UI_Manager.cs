using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] TMP_Text score_TXT;

    // Update is called once per frame at end of OoO
    void LateUpdate()
    {
        score_TXT.text = "Score ~ " + GameManager.instance.score.ToString();
    }
}
