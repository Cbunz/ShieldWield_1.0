using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    private float t;
    private float x;
    private float y;
    private Vector3 startPosition;
    private Vector3 target;

    [HideInInspector]
    public bool isMoving = false;
    public float timeToTarget = 3f;
    public float xDirection = 0f;
    public float yDirection = 0f;
    public float speed = 1f;
    public bool oneWay;
    public bool horizontal;


    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        target = new Vector3(transform.position.x + xDirection, transform.position.y + yDirection, transform.position.z);
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (oneWay == true)
        {
            OneWayMove();
        }
        if (oneWay == false)
        {
            PingPong();
        }
    }

    private void OneWayMove()
    {
        if (isMoving == true)
        {
            t += Time.deltaTime / timeToTarget;
            transform.position = Vector3.Lerp(startPosition, target, t);
        }
    }

    private void PingPong()
    {
        if (isMoving == true)
        {
            Debug.Log("Ping Pong");
            float pingPong = Mathf.PingPong(Time.time * speed, 1);
            transform.position = Vector3.Lerp(startPosition, target, pingPong);
            // transform.position = new Vector3(transform.position.x + ((horizontal) ? Mathf.PingPong(x, xDirection) : 0), transform.position.y + ((horizontal) ? 0 : Mathf.PingPong(y, yDirection)), transform.position.z);
        }
    }

    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToTarget = time;
        target = destination;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMoving = true;
        }
    }
}
