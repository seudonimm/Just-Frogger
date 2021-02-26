using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowAndShrink : MonoBehaviour
{
    [SerializeField] float t, size, min, max;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        size = Mathf.Lerp(min, max, t);
        t = Mathf.PingPong(Time.time, 1);
        transform.localScale = new Vector3(size, size, size);
    }
}
