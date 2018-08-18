using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownShield : MonoBehaviour {

    private GameObject player;
    private PlayerController playerController;
    private GameObject shieldPivot;
    private ShieldPivot shieldPivotScript;
    private Rigidbody2D shieldRB;
    private Vector2 shieldDirection;

    [HideInInspector]
    public bool canReturn = true;
    public iTween.EaseType easeType;
    public float rotateAmount = 50;
    public float speed = 5f;
    public float returnSpeed = 3f;
    public int maxRicochet = 3;

	void Awake ()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        shieldPivot = GameObject.Find("Shield Pivot");
        shieldPivotScript = shieldPivot.GetComponent<ShieldPivot>();
        shieldRB = GetComponent<Rigidbody2D>();
        shieldDirection = shieldPivotScript.boost.normalized;
        shieldRB.velocity = shieldDirection * speed;
    }
	
	void Update ()
    {
        if (playerController.canReturn == true)
        {
            this.gameObject.layer = 14;
        }
        else
        {
            this.gameObject.layer = 13;
        }
        if (shieldDirection.x < 0)
        {
            transform.Rotate(0, 0, rotateAmount * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, -rotateAmount * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (playerController.shieldReturn == true)
        {
            Vector2 returnDirection = (player.transform.position - this.transform.position).normalized;
            shieldRB.velocity = returnDirection * speed;
        }
    }
}
