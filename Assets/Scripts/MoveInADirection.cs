using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInADirection : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

    }
}
