using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour {

    public Sprite[] sprites;
    [HideInInspector]
    public bool pressed = false;
    [HideInInspector]
    public bool positive = false;
    public GameObject platform;
    private ButtonMove buttonMove;
    private bool stopTweak;

    // Use this for initialization
    void Start () {
        buttonMove = platform.GetComponent<ButtonMove>();
    }
	
	// Update is called once per frame
	void Update () {
        if (pressed)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        if (!pressed)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            positive = !positive;
            pressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pressed = false;
            buttonMove.stopTweak = false;
        }
    }
}
