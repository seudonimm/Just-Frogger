using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Shapes;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Player player;

    [SerializeField] Disc playerDisc;
    [SerializeField] Collider2D playerCollider;
    public bool lose;
    [SerializeField] bool hitByObstacle;
    [SerializeField] float invulnTimer, invulnTimerDefault;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] bool moveUp, moveDown, moveLeft, moveRight;
    [SerializeField] float moveSpeed;

    [SerializeField] Disc hurtRing, invulnOverRing;
    [SerializeField] List<Disc> playerLivesRings;
    [SerializeField] int timesHit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!lose)
        {
            GetInput();
        }


        if (hitByObstacle)
        {
            playerDisc.Color = new Color(playerDisc.Color.r, playerDisc.Color.g, playerDisc.Color.b, .5f);
            invulnTimer -= Time.deltaTime;

            if(invulnTimer <= 0)
            {
                hitByObstacle = false;
                Instantiate(invulnOverRing, transform.position, transform.rotation);

                playerDisc.Color = Color.white;
                playerCollider.enabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (moveLeft)
        {
            rb.AddForce(Vector2.left * moveSpeed * Time.fixedDeltaTime);

        }
        else if (moveRight)
        {
            rb.AddForce(Vector2.right * moveSpeed * Time.fixedDeltaTime);

        }

        if (moveUp)
        {
            rb.AddForce(Vector2.up * moveSpeed * Time.fixedDeltaTime);

        }
        else if (moveDown)
        {
            rb.AddForce(Vector2.down * moveSpeed * Time.fixedDeltaTime);

        }
        rb.velocity = Vector2.zero;
    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }

    }

    void PlayerHit()
    {
        Instantiate(hurtRing, transform.position, transform.rotation);
        hitByObstacle = true;
        invulnTimer = invulnTimerDefault;
        playerCollider.enabled = false;


        playerLivesRings[timesHit].enabled = false;
        timesHit++;

        if(timesHit >= 3)
        {
            lose = true;
        }

    }

    public void HitByRayCast()
    {
        if (!hitByObstacle)
        {
            PlayerHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle") && !hitByObstacle)
        {
            PlayerHit();
        }
        if(col.gameObject.CompareTag("Lose Block"))
        {
            lose = true;
        }
    }
}
