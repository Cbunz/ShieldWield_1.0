using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsControl : MonoBehaviour {

    private bool moveStart;
    private MoveControl[] moveControl;
    private GameObject startingPlatform;
    private MoveControl startControl;
    [HideInInspector]
    public Move[] move;
    private Move startMove;

	// Use this for initialization
	void Start () {
        move = GetComponentsInChildren<Move>();
        // moveControl = GetComponentsInChildren<MoveControl>();
        startingPlatform = GameObject.Find("Starting Platform");
        // startControl = startingPlatform.GetComponent<MoveControl>();
        startMove = startingPlatform.GetComponent<Move>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
		if (startControl.isMoving == true)
        {
            foreach (MoveControl control in moveControl)
            {
                control.isMoving = true;
            }
        }
        */
        if (startMove.vanish == true)
        {
            foreach (Move script in move)
            {
                Debug.Log("Making vanishes true");
                script.vanish = true;
                Debug.Log("Script vanish = " + script.vanish);
            }
        }
    }
}
