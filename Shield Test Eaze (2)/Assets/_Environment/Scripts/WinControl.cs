using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RockVR.Video.Demo
{


    public class WinControl : MonoBehaviour
    {
        [HideInInspector]
        public bool hasWon = false;
        public int nextLevelIndex = 2;
        private GameObject levelManager;
        private LevelManager levelManagerScript;
        private GameObject captureControl;
        public bool record;

        // Use this for initialization
        void Start()
        {
            levelManager = GameObject.Find("Level Manager");
            levelManagerScript = levelManager.GetComponent<LevelManager>();
            if (record == true)
            {
                VideoCaptureCtrl.instance.StartCapture();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (hasWon == true)
            {
                if(record == true)
                {
                    VideoCaptureCtrl.instance.StopCapture();
                }
                levelManagerScript.LoadScene(nextLevelIndex);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Has won");
                hasWon = true;
            }
        }
    }
}
