using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSpeechBubble : MonoBehaviour {

    private Canvas parentCanvas;
    private GameObject dadSpeech, momSpeech;
    private GameObject dadText1, dadText2, dadText3;
    private GameObject momText1, momText2, momText3;
    private int dadTextIndex, momTextIndex;

    void Start () {
        parentCanvas = GetComponentInChildren<Canvas>();
        dadSpeech = GameObject.Find("Dad SpeechBubble");
        momSpeech = GameObject.Find("Mom SpeechBubble");
        dadText1 = GameObject.Find("Dad Text 1");
        dadText2 = GameObject.Find("Dad Text 2");
        dadText3 = GameObject.Find("Dad Text 3");
        momText1 = GameObject.Find("Mom Text 1");
        momText2 = GameObject.Find("Mom Text 2");
        momText3 = GameObject.Find("Mom Text 3");
        dadText2.SetActive(false);
        dadText3.SetActive(false);
        momText2.SetActive(false);
        momText3.SetActive(false);
        dadSpeech.SetActive(false);
        momSpeech.SetActive(false);
        dadTextIndex = 1;
        momTextIndex = 0;
	}
	
	void Update () {
		if (dadTextIndex == 1)
        {
            dadText2.SetActive(false);
            dadText3.SetActive(false);
            dadText1.SetActive(true);
            
        }
        else if (dadTextIndex == 2)
        {
            dadText1.SetActive(false);
            dadText3.SetActive(false);
            dadText2.SetActive(true);
            
        }
        else if (dadTextIndex == 3)
        {
            dadText1.SetActive(false);
            dadText2.SetActive(false);
            dadText3.SetActive(true);
            dadTextIndex = 0;
        }

        if (momTextIndex == 1)
        {
            momText2.SetActive(false);
            momText3.SetActive(false);
            momText1.SetActive(true);
            
        }
        else if (momTextIndex == 2)
        {
            momText1.SetActive(false);
            momText3.SetActive(false);
            momText2.SetActive(true);
            
        }
        else if (momTextIndex == 3)
        {
            momText1.SetActive(false);
            momText2.SetActive(false);
            momText3.SetActive(true);
            momTextIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            momSpeech.SetActive(false);
            dadSpeech.SetActive(true);
            momTextIndex++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dadSpeech.SetActive(false);
            momSpeech.SetActive(true);
            dadTextIndex++;
        }
    }
}
