using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSideToSide : MonoBehaviour
{
    [SerializeField] bool moveX, moveY;


    [SerializeField] Vector2 start, end, dest;

    [SerializeField] float t, moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveX && moveY)
        {
            dest = new Vector2(Mathf.Lerp(start.x, end.x, t), Mathf.Lerp(start.y, end.y, t));

        }
        else if (moveX)
        {
            dest = new Vector2(Mathf.Lerp(start.x, end.x, t), transform.position.y);

        }
        else if (moveY)
        {
            dest = new Vector2(transform.position.x, Mathf.Lerp(start.y, end.y, t));

        }

        transform.position = dest;

        t = Mathf.PingPong(Time.time * moveSpeed, 1);
    }
}
