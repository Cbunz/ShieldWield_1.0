using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeechBubble : MonoBehaviour {

    private Canvas playerCanvas;
    private GameObject playerSpeechBubble, shieldSpeechBubble;
    private GameObject playerText1, playerText2, playerText3;
    private GameObject shieldText1, shieldText2, shieldText3, shieldText4, shieldText5;
    private bool triggered = false;
    [HideInInspector]
    public int playerTextIndex = 0;
    [HideInInspector]
    public int shieldTextIndex = 0;
    [HideInInspector]
    public bool playerTextBool = false;
    [HideInInspector]
    public bool shieldTextBool = false;
    public int speechDuration = 3;
    private bool coroutineCalled = false;
    
	void Start () {
        playerCanvas = GetComponentInChildren<Canvas>();
        playerSpeechBubble = GameObject.Find("Player SpeechBubble");
        playerText1 = GameObject.Find("Player Text 1");
        playerText2 = GameObject.Find("Player Text 2");
        playerText3 = GameObject.Find("Player Text 3");
        shieldSpeechBubble = GameObject.Find("Shield SpeechBubble");
        shieldText1 = GameObject.Find("Shield Text 1");
        shieldText2 = GameObject.Find("Shield Text 2");
        shieldText3 = GameObject.Find("Shield Text 3");
        shieldText4 = GameObject.Find("Shield Text 4");
        shieldText5 = GameObject.Find("Shield Text 5");
        playerSpeechBubble.SetActive(false);
        shieldSpeechBubble.SetActive(false);
	}
	
	void Update () {
		if (playerTextBool == true)
        {
            playerSpeechBubble.SetActive(true);
            if (coroutineCalled == false)
            {
                StartCoroutine(ClearSpeechBubble());
                coroutineCalled = true;
            }
        }
        else
        {
            playerSpeechBubble.SetActive(false);
        }

        if (playerTextIndex == 1)
        {
            playerText2.SetActive(false);
            playerText3.SetActive(false);
            playerText1.SetActive(true);
        }
        else if (playerTextIndex == 2)
        {
            playerText1.SetActive(false);
            playerText3.SetActive(false);
            playerText2.SetActive(true);
        }
        else if (playerTextIndex == 3)
        {
            playerText1.SetActive(false);
            playerText2.SetActive(false);
            playerText3.SetActive(true);
        }

        if (shieldTextBool == true)
        {
            shieldSpeechBubble.SetActive(true);
            // StartCoroutine(ClearShieldSpeechBubble());
        }
        else
        {
            shieldSpeechBubble.SetActive(false);
        }

        if (shieldTextIndex == 1)
        {
            shieldText2.SetActive(false);
            shieldText3.SetActive(false);
            shieldText4.SetActive(false);
            shieldText5.SetActive(false);
            shieldText1.SetActive(true);
            if (coroutineCalled == false)
            {
                StartCoroutine(NextShieldSpeechBubble());
                coroutineCalled = true;
            }
        }
        else if (shieldTextIndex == 2)
        {
            
            shieldText3.SetActive(false);
            shieldText4.SetActive(false);
            shieldText5.SetActive(false);
            
        }
        else if (shieldTextIndex == 3)
        {
            shieldText2.SetActive(false);
            shieldText3.SetActive(false);
            shieldText1.SetActive(false);
            shieldText5.SetActive(false);
            shieldText3.SetActive(true);
        }
        else if (shieldTextIndex == 4)
        {
            shieldText2.SetActive(false);
            shieldText3.SetActive(false);
            shieldText1.SetActive(false);
            shieldText5.SetActive(false);
            shieldText4.SetActive(true);
            if (coroutineCalled == false)
            {
                StartCoroutine(ClearShieldSpeechBubble());
                coroutineCalled = true;
                Debug.Log("Coroutine called");
            }
        }
        else if (shieldTextIndex == 5)
        {
            shieldText2.SetActive(false);
            shieldText3.SetActive(false);
            shieldText4.SetActive(false);
            shieldText1.SetActive(false);
            shieldText5.SetActive(true);
            if (coroutineCalled == false)
            {
                StartCoroutine(ClearShieldSpeechBubble());
                coroutineCalled = true;
                Debug.Log("Coroutine called");
            }
        }
    }

    IEnumerator ClearSpeechBubble()
    {
        yield return new WaitForSeconds(speechDuration);
        coroutineCalled = false;
        playerTextBool = false;
    }

    IEnumerator ClearShieldSpeechBubble()
    {
        yield return new WaitForSeconds(speechDuration);
        coroutineCalled = false;
        shieldTextBool = false;
    }

    IEnumerator NextShieldSpeechBubble()
    {
        shieldTextIndex = 0;
        Debug.Log("Coroutine start");
        coroutineCalled = true;
        yield return new WaitForSeconds(speechDuration);
        Debug.Log("waited 1");
        shieldTextBool = false;
        shieldText1.SetActive(false);
        shieldText2.SetActive(true);
        shieldTextBool = true;
        yield return new WaitForSeconds(speechDuration);
        Debug.Log("waited 2");
        shieldTextBool = false;
        shieldText2.SetActive(false);
        shieldText3.SetActive(true);
        shieldTextBool = true;
        yield return new WaitForSeconds(6);
        Debug.Log("waited 3");
        shieldTextBool = false;
    }
}
