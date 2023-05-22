using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using TMPro;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Disc playerDisc;
    [SerializeField] Transform playerDiscTransform;
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
            var discColor = playerDisc.Color;
            discColor.a = .5f;
            playerDisc.Color = discColor;
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
        PlayerAnimaiton();
    }

    void PlayerAnimaiton()
    {
        

        if((moveLeft && moveUp) || (moveDown && moveRight))
        {
            playerDiscTransform.localScale = new Vector2(.9f, 1.1f);
            playerDiscTransform.localRotation = Quaternion.Euler(0, 0, 45f);
        }
        else if ((moveLeft && moveDown) || (moveUp && moveRight))
        {
            playerDiscTransform.localScale = new Vector2(.9f, 1.1f);
            playerDiscTransform.localRotation = Quaternion.Euler(0, 0, -45f);
        }
        else if (moveLeft || moveRight)
        {
            playerDiscTransform.localScale = new Vector2(1.1f, .9f);
            playerDiscTransform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        else if (moveUp || moveDown)
        {
            playerDiscTransform.localScale = new Vector2(.9f, 1.1f);
            playerDiscTransform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            playerDiscTransform.localScale = new Vector2(1, 1);
            playerDiscTransform.localRotation = Quaternion.Euler(0, 0, 0);

        }


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

        moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);
        moveUp = Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.S);

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
