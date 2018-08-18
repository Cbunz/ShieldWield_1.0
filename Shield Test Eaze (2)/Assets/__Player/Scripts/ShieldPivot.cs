using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPivot : MonoBehaviour {

    // Shield Control
    private GameObject player;
    private Vector2 mousePos;
    private Vector3 screenPos;
    private Camera mainCamera;
    [HideInInspector]
    public Quaternion shieldDirection;
    [HideInInspector]
    public float boostX;
    [HideInInspector]
    public float boostY;
    [HideInInspector]
    public Vector2 boost;
	
	void Start ()
    {
        player = GameObject.Find("Player");
        mainCamera = FindObjectOfType<Camera>();
    }

    void FixedUpdate ()
    {
        ShieldControl();
    }

    private void ShieldControl()
    {
        mousePos = Input.mousePosition;
        screenPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (player.transform.position.z - mainCamera.transform.position.z)));
        boostX = screenPos.x - player.transform.position.x;
        boostY = screenPos.y - player.transform.position.y;
        boost = new Vector2(boostX, boostY);
        shieldDirection = Quaternion.Euler(0, 0, (Mathf.Atan2(boostY, boostX) * Mathf.Rad2Deg));
        transform.rotation = shieldDirection;
    }
}
