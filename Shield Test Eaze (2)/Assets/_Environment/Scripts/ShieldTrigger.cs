using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour {

    private GameObject player;
    private PlayerController playerController;
    private GameObject sprite;
    private Canvas shieldCanvas;
    private GameObject shieldSpeechBubble;
    private GameObject playerSpeechBubble;
    private PlayerSpeechBubble playerSpeechBubbleScript;
    [HideInInspector]
    public bool shieldTalk = false;
    [HideInInspector]
    public bool stopTalk = false;
    public int shieldTalkDuration = 3;
    private bool coroutineCalled = false;
    private bool collided = false;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerSpeechBubble = GameObject.Find("Player SpeechBubble");
        playerSpeechBubbleScript = player.GetComponent<PlayerSpeechBubble>();
        sprite = GameObject.Find("Ground Shield Sprite");
        shieldCanvas = GetComponentInChildren<Canvas>();
        shieldSpeechBubble = GameObject.Find("Wall Shield SpeechBubble");
        shieldSpeechBubble.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (shieldTalk == true && stopTalk == false)
        {
            shieldSpeechBubble.SetActive(true);
        }
        else
        {
            shieldSpeechBubble.SetActive(false);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collided == false)
            {
                shieldTalk = false;
                stopTalk = true;
                playerSpeechBubbleScript.shieldTextIndex = 1;
                playerSpeechBubbleScript.shieldTextBool = true;
                sprite.SetActive(false);
                playerController.shieldEquip = true;
                collided = true;
            }
        }
    }
}
