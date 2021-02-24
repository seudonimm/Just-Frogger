using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class TriangleController : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float followSpeed;

    [SerializeField] Line laser;

    [SerializeField] public RaycastHit2D hit;

    [SerializeField] LaserState laserState;

    [SerializeField] float timer, timerDefault;

    // Start is called before the first frame update
    void Start()
    {
        hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y + 30f), LayerMask.NameToLayer("Player"));
        timer = timerDefault;

    }

    // Update is called once per frame
    void Update()
    {
        LaserControl();
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
                transform.position = new Vector3(17f, Vector3.MoveTowards(transform.position, player.transform.position, followSpeed).y, -1f);


                if (timer <= 0)
                {
                    //laser = Instantiate(laser, transform.position, transform.rotation);
                    laserState = LaserState.Startup;
                    timer = timerDefault;
                }
                break;

            case LaserState.Startup:

                laser.Start = new Vector3(5, 0, 0);
                laser.End = new Vector3(-50, 0, 0);

                //laser.transform.position = transform.position;
                laser.Color = Color.gray;
                if (timer <= 0)
                {
                    laserState = LaserState.Active;
                    timer = timerDefault;

                }
                break;

            case LaserState.Active:

                laser.Color = Color.red;

                laser.ColorStart = Color.red;
                laser.ColorEnd = Color.red;
                hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y + 30f), LayerMask.NameToLayer("Player"));

                if (timer <= 0)
                {
                    laserState = LaserState.Cooldown;
                    timer = timerDefault;

                }

                break;

            case LaserState.Cooldown:

                laser.Start = Vector3.zero;
                laser.End = Vector3.zero;
                hit = Physics2D.Linecast(transform.position, transform.position);

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