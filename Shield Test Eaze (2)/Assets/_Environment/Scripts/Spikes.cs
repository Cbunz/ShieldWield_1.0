using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private GameObject player;
    private PlayerController playerController;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.dead = true;
        }
    }
}
