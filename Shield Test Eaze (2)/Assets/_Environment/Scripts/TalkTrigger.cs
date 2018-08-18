using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTrigger : MonoBehaviour {

    public bool playerTrigger;

    private GameObject player;
    private PlayerSpeechBubble playerSpeechBubbleScript;
    public int playerIndex;
    public int shieldIndex;
    private GameObject shieldTrigger;
    private ShieldTrigger shieldTriggerScript;
    public bool jumpTrigger;
    private int jumpAttempts = 0;
    private bool triggered = false;

    // Use this for initialization
    void Start () {
        shieldTrigger = GameObject.Find("Shield Trigger");
        shieldTriggerScript = shieldTrigger.GetComponent<ShieldTrigger>();
        player = GameObject.Find("Player");
        playerSpeechBubbleScript = player.GetComponent<PlayerSpeechBubble>();
	}
	
	// Update is called once per frame
	void Update () {
		if (jumpAttempts > 2)
        {
            shieldTriggerScript.shieldTalk = true;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerTrigger == true)
            {
                if (triggered == false)
                {
                    playerSpeechBubbleScript.playerTextIndex = playerIndex;
                    playerSpeechBubbleScript.playerTextBool = true;
                    Debug.Log("Player text triggered");
                    triggered = true;
                }
            }
            else if (playerTrigger == false)
            {
                if (jumpTrigger == true)
                {
                    jumpAttempts++;
                }
                else
                {
                    if (triggered == false)
                    {
                        playerSpeechBubbleScript.shieldTextIndex = shieldIndex;
                        playerSpeechBubbleScript.shieldTextBool = true;
                        triggered = true;
                    }
                }
            }
        }
    }
}
