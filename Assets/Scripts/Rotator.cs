using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
    }


    void Rotate()
    {
        transform.Rotate(0, 0, 1 * rotateSpeed * Time.fixedDeltaTime);
    }

}
