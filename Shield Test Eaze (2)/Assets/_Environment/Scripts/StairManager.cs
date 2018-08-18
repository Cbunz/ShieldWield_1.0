using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairManager : MonoBehaviour {

    [HideInInspector]
    public bool crumble = false;
    private Rigidbody2D[] stairs;

	void Start ()
    {
        stairs = GetComponentsInChildren<Rigidbody2D>();
	}
	

	void Update ()
    {
		if (crumble == true)
        {
            Debug.Log("Crumble = true");
            foreach (Rigidbody2D rb in stairs)
            {
                Debug.Log("Crumbling");
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 1;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            crumble = true;
        }
    }
}
