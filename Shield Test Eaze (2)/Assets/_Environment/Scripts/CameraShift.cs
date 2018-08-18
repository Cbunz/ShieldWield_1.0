using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour {

    [HideInInspector]
    public bool shift = false;
    private GameObject mainCamera;
    private CameraFollow cameraFollow;
    private float yOffset;
    private float ySmooth;

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Main Camera");
        cameraFollow = mainCamera.GetComponent<CameraFollow>();
	}
	
	// Update is called once per frame
	void Update () {
		if (shift == true)
        {
            yOffset = 3.5f;
            ySmooth = .3f;
        }
        if (shift == false)
        {
            yOffset = 1f;
            ySmooth = .01f;
        }
        cameraFollow.verticalSmoothTime = ySmooth;
        cameraFollow.verticalOffset = yOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shift = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shift = false;
        }
    }
}
