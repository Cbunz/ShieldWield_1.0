using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour {


    public Sprite[] spriteList;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;


    // Use this for initialization
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //SetSprite();
        FacingDirection();
    }

    void FacingDirection()
    {
        if (playerController.faceLeft == true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void SetSprite()
    {
        if (playerController.spike == true)
        {
            spriteRenderer.sprite = spriteList[1];
        }
        else
        {
            spriteRenderer.sprite = spriteList[0];
        }
    }
}
