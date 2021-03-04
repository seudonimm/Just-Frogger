using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class TriangleController : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float followSpeed;

    [SerializeField] Line laser, laserReady;

    [SerializeField] public RaycastHit2D hit;

    [SerializeField] LaserState laserState;

    [SerializeField] float timer, timerDefault, laserReadyTimer;

    [SerializeField] bool right, left, top, bottom;
    [SerializeField] float xPosition, yPosition;

    // Start is called before the first frame update
    void Start()
    {
        hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y + 30f), LayerMask.NameToLayer("Player"));
        timer = timerDefault;

        laserReady.Start = new Vector3(0, 0, 0);
        laserReady.End = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            xPosition = 17f;
        }
        else if (left)
        {
            xPosition = -19f;
        }

        if (top)
        {
            yPosition = 10f;
        }
        else if (bottom)
        {
            yPosition = -10f;
        }

        if (player)
        {
            LaserControl();
        }
        timer -= Time.deltaTime;
        if(hit.collider != null)
        {
            Debug.Log("hitting");
        }
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            hit.collider.SendMessage("HitByRayCast");
            Debug.Log("hitting player");
        }

    }

    void LaserControl()
    {
        switch (laserState)
        {
            case LaserState.Follow:

                laser.Start = Vector3.zero;
                laser.End = Vector3.zero;
                if (right || left)
                {
                    transform.position = new Vector3(xPosition, Vector3.MoveTowards(transform.position, player.transform.position, followSpeed).y, -1f);
                }
                if(top || bottom)
                {
                    transform.position = new Vector3(Vector3.MoveTowards(transform.position, player.transform.position, followSpeed).x, yPosition, -1f);
                }


                if (laserState == LaserState.Follow)
                {
                    if (right)
                    {
                        laser.Start = new Vector3(Mathf.Lerp(5, 0, timer), 0, 0);
                        laser.End = new Vector3(Mathf.Lerp(-50, 0, timer), 0, 0);
                    }
                    if (left)
                    {
                        laser.Start = new Vector3(Mathf.Lerp(-5, 0, timer), 0, 0);
                        laser.End = new Vector3(Mathf.Lerp(50, 0, timer), 0, 0);
                    }
                    if (top)
                    {
                        laser.Start = new Vector3(0, Mathf.Lerp(5, 0, timer), 0);
                        laser.End = new Vector3(0, Mathf.Lerp(-50, 0, timer), 0);
                    }
                    if (bottom)
                    {
                        laser.Start = new Vector3(0, Mathf.Lerp(-5, 0, timer), 0);
                        laser.End = new Vector3(0, Mathf.Lerp(50, 0, timer), 0);
                    }
                    laser.Color = Color.gray;

                    laserReady.Start = laser.Start;
                    laserReady.End = laser.End;

                    laserReady.Thickness = Mathf.Lerp(1, 0.2f, timer) * 5f;
                    laserReady.Color = new Color(Color.grey.r, Color.grey.g, Color.grey.b, Mathf.Lerp(.3f, 0, timer));

                }

                if (timer <= 0)
                {
                    //laser = Instantiate(laser, transform.position, transform.rotation);
                    laserState = LaserState.Startup;
                    timer = timerDefault;
                }

                break;

            case LaserState.Startup:

                laserReady.Thickness = Mathf.Lerp(0.2f, 1, timer) * 5f;
                laserReady.Color = Color.Lerp(new Color(Color.red.r, Color.red.g, Color.red.b, Mathf.Lerp(0, .3f, timer)), new Color(Color.grey.r, Color.grey.g, Color.grey.b, Mathf.Lerp(0, .3f, timer)), timer);
                //laser.transform.position = transform.position;
                if (timer <= 0)
                {
                    laserState = LaserState.Active;
                    timer = timerDefault/2f;

                }
                break;

            case LaserState.Active:
                laserReady.Start = laser.Start;
                laserReady.End = laser.End;

                laser.Color = Color.red;

                laser.ColorStart = Color.red;
                laser.ColorEnd = Color.red;
                if (right)
                {
                    hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x - 50f, transform.position.y), 1 << LayerMask.NameToLayer("Player"));
                }
                if (left)
                {
                    hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x + 50f, transform.position.y), 1 << LayerMask.NameToLayer("Player"));
                }
                if (top)
                {
                    hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 50f), 1 << LayerMask.NameToLayer("Player"));
                }
                if (bottom)
                {
                    hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y + 50f), 1 << LayerMask.NameToLayer("Player"));
                }


                if (timer <= 0)
                {
                    laserState = LaserState.Cooldown;
                    timer = timerDefault;

                }

                break;

            case LaserState.Cooldown:

                laser.Start = Vector3.zero;
                laser.End = Vector3.zero;
                hit = Physics2D.Linecast(transform.position, transform.position, 1 << LayerMask.NameToLayer("None"));

                if (timer <= 0)
                {
                    laserState = LaserState.Follow;
                    timer = timerDefault;

                }

                break;
        }
    }
}

public enum LaserState
{
    Follow,
    Startup,
    Active,
    Cooldown
}