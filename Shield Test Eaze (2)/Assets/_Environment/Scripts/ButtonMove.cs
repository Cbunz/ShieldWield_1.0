using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour {

    // Move positive
    public float xDistance = 0f;
    public float yDistance = 0f;
    public float time = 0f;
    public float delay = 0f;
    public iTween.LoopType loopType;
    public iTween.EaseType easeType;

    // Move negative
    public float xDistanceNeg = 0f;
    public float yDistanceNeg = 0f;
    public float timeNeg = 0f;
    public float delayNeg = 0f;
    public iTween.LoopType loopTypeNeg;
    public iTween.EaseType easeTypeNeg;
    
    public GameObject redButton;
    private RedButton redButtonScript;
    private bool pressed = false;
    private bool positive = false;

    [HideInInspector]
    public bool stopTweak = false;

    // Use this for initialization
    void Start () {
        redButtonScript = redButton.GetComponent<RedButton>();
	}
	
	// Update is called once per frame
	void Update () {
        pressed = redButtonScript.pressed;
        positive = redButtonScript.positive;
        RedButtonPlatforms();
	}

    private void RedButtonPlatforms()
    {
        if (pressed == true && stopTweak == false)
        {
            if (positive == true)
            {
                iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(transform.position.x + xDistance, transform.position.y + yDistance, transform.position.z), "time", time, "delay", delay, "looptype", loopType, "easetype", easeType));
                stopTweak = true;
            }
            if (positive == false)
            {
                iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(transform.position.x + xDistanceNeg, transform.position.y + yDistanceNeg, transform.position.z), "time", timeNeg, "delay", delayNeg, "looptype", loopTypeNeg, "easetype", easeTypeNeg));
            }
            stopTweak = true;
        }
    }
}
