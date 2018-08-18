using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : RaycastController
{
    // States
    [HideInInspector]
    public bool onGround = false;
    [HideInInspector]
    public bool crouch = false;
    [HideInInspector]
    public bool spike = false;
    [HideInInspector]
    public bool spiked = false;
    [HideInInspector]
    public bool hasShield = true;
    [HideInInspector]
    public bool shieldEquip = true;
    [HideInInspector]
    public bool faceLeft = false;

    // Animation
    private Animator animator;

    // Movement
    public float topSpeed = 20f;
    public float groundAccelTime = .1f;
    public float airAccelTime = .15f;
    public float groundDecelTime = .05f;
    private float accelSpeed;
    private float decelSpeed;
    private Rigidbody2D rb;

    // Crouch
    private BoxCollider2D playerColl;
    private GameObject playerSprite;

    // Jump
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .44f;
    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;

    // Shield Throw
    [HideInInspector]
    public bool shieldThrown = false;
    [HideInInspector]
    public bool shieldReturn = false;
    [HideInInspector]
    public bool canReturn = true;
    private bool returnRefresh = true;
    private GameObject shieldPivot;
    public GameObject thrownShieldPrefab;
    private GameObject shield;
    private int shieldCount = 0;
    private GameObject thrownShield;

    // Respawn
    [HideInInspector]
    public bool dead;
    [HideInInspector]
    public Vector3 spawnLoc;
    private bool respawn;
    private bool imRespawning;
    private PlayerSpeechBubble playerSpeechBubbleScript;
    private bool respawnedOnce = false;

    public override void Start()
    {
        // Animation
        animator = GetComponentInChildren<Animator>();

        // Shield Throw
        shieldPivot = GameObject.Find("Shield Pivot");
        shield = GameObject.Find("Shield");

        // Crouch
        playerColl = GetComponent<BoxCollider2D>();
        playerSprite = GameObject.Find("Player Sprite");

        // Jump Physics
        rb = GetComponent<Rigidbody2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * maxJumpHeight);
        minJumpVelocity = Mathf.Sqrt(Mathf.Pow(maxJumpVelocity, 2) + 2 * gravity * (maxJumpHeight - minJumpHeight));

        // Speech Bubble
        playerSpeechBubbleScript = GetComponent<PlayerSpeechBubble>();
    }

    private void Update()
    {
        Spike();
        ShieldThrow();
        Respawn();
        Debug.Log("Shield Count: " + shieldCount);
        Debug.Log("canReturn = " + canReturn);
    }

    void FixedUpdate()
    {
        Gravity();
        if (dead == false)
        {
            if (rb.velocity.x == 0 || onGround == false)
            {
                animator.SetBool("moving",false);
            }
            else
            {
                animator.SetBool("moving", true);
            }
            Crouch();
            Movement();
        }
    }
    
    // Shield Throw Ability
    
    private void ShieldThrow()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && shieldThrown == false)
        {
            Debug.Log("Throw shield");
            shieldThrown = true;
            canReturn = false;
            thrownShield = Instantiate(thrownShieldPrefab, shield.transform.position, shield.transform.rotation);
            shieldCount++;
            shieldPivot.SetActive(false);
            if (returnRefresh == true)
            {
                StartCoroutine(CanReturn());
            }
            returnRefresh = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && shieldThrown == true)
        {
            Debug.Log("Shield return");
            shieldThrown = false;
            canReturn = true;
            shieldReturn = true;
        }
    }

    // Crouch
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.S) && spiked == false)
        {
            crouch = true;
            playerColl.size = new Vector2(1, 1);
            playerSprite.transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            crouch = false;
            playerSprite.transform.localScale = new Vector3(1, 1.5f, 1);
            playerColl.size = new Vector2(1, 1.5f);
        }
    }

    // Move
    private void Movement()
    {
        if (spiked == false)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                accelSpeed = Mathf.SmoothDamp(rb.velocity.x, -topSpeed, ref accelSpeed, (onGround ? groundAccelTime : airAccelTime));
                rb.velocity = new Vector2(accelSpeed, rb.velocity.y);
                faceLeft = true;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                accelSpeed = Mathf.SmoothDamp(rb.velocity.x, topSpeed, ref accelSpeed, (onGround ? groundAccelTime : airAccelTime));
                rb.velocity = new Vector2(accelSpeed, rb.velocity.y);
                faceLeft = false;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, ((minJumpVelocity < rb.velocity.y) ? minJumpVelocity : rb.velocity.y));
            }
            if (onGround)
            {
                /*
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    decelSpeed = Mathf.Clamp(Mathf.SmoothDamp(rb.velocity.x, topSpeed, ref decelSpeed, groundDecelTime), Mathf.NegativeInfinity, 0);
                    rb.velocity = new Vector2(decelSpeed, rb.velocity.y);
                    faceLeft = true;
                }
                if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    decelSpeed = Mathf.Clamp(Mathf.SmoothDamp(rb.velocity.x, -topSpeed, ref decelSpeed, groundDecelTime), 0, Mathf.Infinity);
                    rb.velocity = new Vector2(decelSpeed, rb.velocity.y);
                    faceLeft = false;
                }
                */
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.velocity = new Vector2(rb.velocity.x, maxJumpVelocity);//maxJumpVelocity
                }
            }
        }
    }

    // Spike Ability
    private void Spike()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            spike = !spike;
            Debug.Log("spike: " + spike);
            spiked = false;
        }
        if (spiked == true)
        {
            rb.isKinematic = true;
            rb.Sleep();
        }
        else if (spiked == false)
        {
            rb.isKinematic = false;
            rb.WakeUp();
        }
    }

    // Gravity
    private void Gravity()
    {
        if (spiked == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (gravity * Time.deltaTime));
        }
    }

    // Respawn
    private void Respawn()
    {
        if (respawn == true)
        {
            transform.position = spawnLoc;
            if (respawnedOnce == false)
            {
                playerSpeechBubbleScript.shieldTextIndex = 5;
                playerSpeechBubbleScript.shieldTextBool = true;
                respawnedOnce = true;
            }
            dead = false;
            respawn = false;
            imRespawning = false;
        }
        else if (dead == true)
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            StartCoroutine(Respawning());
        }
    }
    IEnumerator Respawning()
    {
        if (imRespawning == false)
        {
            imRespawning = true;
            yield return new WaitForSeconds(2);
            respawn = true;
        }
    }

    IEnumerator CanReturn()
    {
        returnRefresh = false;
        yield return new WaitForSeconds(.5f);
        canReturn = true;
    }

    // Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            onGround = true;
        }
        else if (spike == true && collision.gameObject.layer == 11)
        {
            spiked = true;
            Debug.Log("Spiked!");
        }
        if (collision.gameObject.layer == 14)
        {
            Debug.Log("Shield collected");
            shieldPivot.SetActive(true);
            shieldReturn = false;
            shieldThrown = false;
            thrownShield.GetComponent<ThrownShield>().canReturn = true;
            Destroy(collision.gameObject);
            shieldCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            onGround = false;
        }
    }
}