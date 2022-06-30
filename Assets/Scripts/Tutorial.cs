using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] [Min(0)] float checkPause;
    [SerializeField][Min(0)] float fadeSpeed;
    [SerializeField] GameObject[] checkmark;

    CanvasGroup m_text;
    AudioSource m_audio;

    bool leftSwitch = false;
    bool rightSwitch = false;
    bool jumpSwitch = false;

    void Start()
    {
        m_text = GetComponent<CanvasGroup>();
        m_audio = GetComponent<AudioSource>();
        if (DataManager.instance.tutorialComplete)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Checkmark switch
        if (Input.GetKeyDown(KeyCode.A) && !leftSwitch)
        {
            checkmark[0].SetActive(true);
            leftSwitch = true;
            m_audio.Play();
            if (leftSwitch && rightSwitch && jumpSwitch)
            {
                StartCoroutine(dismissTutorial());
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && !rightSwitch)
        {
            checkmark[1].SetActive(true);
            rightSwitch = true;
            m_audio.Play();
            if (leftSwitch && rightSwitch && jumpSwitch)
            {
                StartCoroutine(dismissTutorial());
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !jumpSwitch)
        {
            checkmark[2].SetActive(true);
            jumpSwitch = true;
            m_audio.Play();
            if (leftSwitch && rightSwitch && jumpSwitch)
            {
                StartCoroutine(dismissTutorial());
            }
        }

        if (GameManager.instance.isGameOver)
        {
            Destroy(gameObject);
        }
    }

    //ABSTRACTION
    //Dismiss Tutorial 
    IEnumerator dismissTutorial()
    {
        DataManager.instance.tutorialComplete = true;
        yield return new WaitForSeconds(checkPause);
        while (m_text.alpha > 0)
        {
            m_text.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
