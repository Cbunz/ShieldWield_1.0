using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSprite : MonoBehaviour{

    public Sprite[] spriteList;
    private SpriteRenderer spriteRenderer;
    private GameObject shieldBoost;
    private ShieldBoost shieldBoostScript;
    private PlayerController playerController;

	// Use this for initialization
	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shieldBoost = GameObject.Find("Shield Boost");
        shieldBoostScript = shieldBoost.GetComponent<ShieldBoost>();
        playerController = GetComponentInParent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        spriteRenderer.sprite = spriteList[shieldBoostScript.boostCharge];
        FacingDirection();
	}

    void FacingDirection()
    {
        if (playerController.faceLeft == true)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
