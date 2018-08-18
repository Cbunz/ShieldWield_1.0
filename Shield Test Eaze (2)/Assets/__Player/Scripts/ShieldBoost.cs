using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoost : MonoBehaviour
{
    public float[] boostFactor;
    public float groundBoostX, groundBoostY, airBoostX, airBoostY, wallBoostX, wallBoostY, spikeBoostX, spikeBoostY;
    public float boostCooldown = 3f;
    public GameObject shieldSprite;
    private GameObject shieldPivot;
    private ShieldPivot shieldPivotScript;
    private PlayerController playerController;
    private Rigidbody2D rb;
    private bool canBoost = true;
    private bool colliding = false;
    private bool charging = false;
    [HideInInspector]
    public int boostCharge = 0;
    public int maxBoost = 5;
    private int boostsLeft;

    void Start()
    {
        rb = transform.parent.parent.GetComponent<Rigidbody2D>();
        shieldPivot = transform.parent.gameObject;
        shieldPivotScript = GetComponentInParent<ShieldPivot>();
        playerController = transform.parent.parent.GetComponent<PlayerController>();
        boostsLeft = maxBoost;
    }

    void Update()
    {
        ShieldCharge();
    }

    private void FixedUpdate()
    {
        AirBoost();
    }

    private void ShieldCharge()
    {
        if (Input.GetMouseButtonDown(0))
        {
            boostCharge = 1;
            Debug.Log("Charge = " + boostCharge);
            charging = true;
            // shieldSprite.transform.localScale -= new Vector3(0.1F, 0.1F, 0.1F);
            StopCoroutine(BoostRegen());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            charging = false;
            // shieldSprite.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
            boostCharge = 0;
            StartCoroutine(BoostRegen());
        }
        if (Input.GetMouseButton(0) && Input.GetKeyDown(KeyCode.W) && boostCharge < maxBoost && boostsLeft > 0)
        {
            boostCharge++;
            boostsLeft--;
            Debug.Log("Charge = " + boostCharge);
        }
    }

    IEnumerator BoostRegen()
    {
        yield return new WaitForSeconds(.1f);
        while (boostsLeft < maxBoost && charging == false)
        {
            yield return new WaitForSeconds(.1f);
            boostsLeft++;
        }
    }

    private void AirBoost()
    {
        if (Input.GetMouseButtonUp(0) && playerController.dead == false && canBoost == true && colliding == false && playerController.spiked == false)
        {
            canBoost = false;
            Invoke("BoostRefresh", boostCooldown);

            shieldPivotScript.boost = shieldPivotScript.boost.normalized;
            rb.velocity = Vector2.zero;
            rb.AddForce(-shieldPivotScript.boost * boostFactor[boostCharge - 1], ForceMode2D.Impulse);
            Debug.Log("Air Boost");
        }
    }

    private void GroundBoost()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButtonUp(0) && playerController.dead == false && canBoost == true)
        {
            canBoost = false;
            Invoke("BoostRefresh", boostCooldown);

            shieldPivotScript.boost = shieldPivotScript.boost.normalized;

            Debug.Log("Collision Boost");
            if (playerController.spiked == false)
            {
                if (collision.gameObject.layer == 10)
                {
                    Debug.Log("Ground Boost");
                    shieldPivotScript.boost = new Vector2(shieldPivotScript.boost.x + ((shieldPivotScript.boost.x < 0f) ? -groundBoostX : groundBoostX), shieldPivotScript.boost.y + ((shieldPivotScript.boost.y < 0f) ? -groundBoostY : groundBoostY));
                }
                else if (collision.gameObject.layer == 11)
                {
                    Debug.Log("Wall Boost");
                    shieldPivotScript.boost = new Vector2(shieldPivotScript.boost.x + ((shieldPivotScript.boost.x < 0f) ? -wallBoostX : wallBoostX), shieldPivotScript.boost.y + ((shieldPivotScript.boost.y < 0f) ? -wallBoostY : wallBoostY));
                }
                rb.velocity = Vector2.zero;
                rb.AddForce(-shieldPivotScript.boost * boostFactor[boostCharge - 1], ForceMode2D.Impulse);
            }
            else
            {
                playerController.spike = false;
                playerController.spiked = false;
                shieldPivotScript.boost = new Vector2(shieldPivotScript.boost.x + ((shieldPivotScript.boost.x < 0f) ? -spikeBoostX : spikeBoostX), shieldPivotScript.boost.y + ((shieldPivotScript.boost.y < 0f) ? -spikeBoostY : spikeBoostY));
                rb.AddForce(-shieldPivotScript.boost * boostFactor[boostCharge - 1], ForceMode2D.Impulse);
                Debug.Log("Spike Boost");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            colliding = true;
        }
        else if (collision.gameObject.layer == 11)
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            colliding = false;
        }
        else if (collision.gameObject.layer == 11)
        {
            colliding = false;
        }
    }

    void BoostRefresh()
    {
        canBoost = true;
    }
}
