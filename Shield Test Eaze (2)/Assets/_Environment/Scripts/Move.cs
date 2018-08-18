using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    // Vanish
    public float xDistanceVan = 0f;
    public float yDistanceVan = 0f;
    public float timeVan = 0f;
    public float delayVan = 0f;
    public iTween.LoopType loopTypeVan;
    public iTween.EaseType easeTypeVan;
    public float vanishTime;

    // Move
    public float xDistance = 0f;
    public float yDistance = 0f;
    public float time = 0f;
    public float delay = 0f;
    public iTween.LoopType loopType;
    public iTween.EaseType easeType;

    // Vanish bools
    [HideInInspector]
    public bool vanish = false;
    private bool stopVanish = false;
    private bool shouldMove = false;
    private bool stopCo = false;
    private bool doMove = false;
    private bool stopMove = false;

    // PlatformsControl
    private GameObject movingPlatforms;
    private PlatformsControl platformsControl;
    private Move[] moveArray;

    public bool vanishingPlatform;

    // Red Buttons
    public int buttonNumber;
    private GameObject redButton1;
    private GameObject redButton2;
    private GameObject redButton3;
    private RedButton redButtonScript1;
    private RedButton redButtonScript2;
    private RedButton redButtonScript3;
    private bool pressed;
    private bool start;

    // Red Button Bools
    [HideInInspector]
    public bool stopTweak = false;

	void Start () {
        redButton1 = GameObject.Find("Red Button 1");
        redButton2 = GameObject.Find("Red Button 2");
        redButton3 = GameObject.Find("Red Button 3");
        redButtonScript1 = redButton1.GetComponent<RedButton>();
        redButtonScript2 = redButton2.GetComponent<RedButton>();
        redButtonScript3 = redButton3.GetComponent<RedButton>();
    }

	void Update () {
        if (vanishingPlatform == true)
        {
            VanishingPlatforms();
        }
        /*
        if (vanishingPlatform)
        {
            if (buttonNumber == 1)
            {
                pressed = redButtonScript1.pressed;
                start = redButtonScript1.start;
            }
            if (buttonNumber == 2)
            {
                pressed = redButtonScript2.pressed;
                start = redButtonScript2.start;
            }
            if (buttonNumber == 3)
            {
                pressed = redButtonScript3.pressed;
                start = redButtonScript3.start;
            }
            RedButtonPlatforms();
        }
        */
    }

    private void RedButtonPlatforms()
    {
        if (pressed == true)
        {
            if (start == true && stopTweak == false)
            {
                iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(transform.position.x + xDistance, transform.position.y + yDistance, transform.position.z), "time", time, "delay", delay, "looptype", loopType, "easetype", easeType));
                stopTweak = true;
            }
            else if (start == false && stopTweak == false)
            {
                iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(transform.position.x + xDistanceVan, transform.position.y + yDistanceVan, transform.position.z), "time", timeVan, "delay", delayVan, "looptype", loopTypeVan, "easetype", easeTypeVan));
                stopTweak = true;
            }
        }
    }

    private void VanishingPlatforms()
    {
        if (vanish == true && stopVanish == false)
        {
            Debug.Log("Vanish start");
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(transform.position.x + xDistanceVan, transform.position.y + yDistanceVan, transform.position.z), "time", timeVan, "delay", delayVan, "looptype", loopTypeVan, "easetype", easeTypeVan));
            shouldMove = true;
            stopVanish = true;
            Debug.Log("Vanish end");
        }
        if (shouldMove == true && stopCo == false)
        {
            Debug.Log("Coroutine start");
            StartCoroutine(MoveStart());
        }
        if (doMove == true && stopMove == false)
        {
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(transform.position.x + xDistance, transform.position.y + yDistance, transform.position.z), "time", time, "delay", delay, "looptype", loopType, "easetype", easeType));
            stopMove = true;
        }
    }

    IEnumerator MoveStart()
    {
        Debug.Log("Before waitfor");
        yield return new WaitForSeconds(vanishTime);
        Debug.Log("After waitfor");
        stopCo = true;
        doMove = true;
        Debug.Log("Coroutine end");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            vanish = true;
        }
    }
}
