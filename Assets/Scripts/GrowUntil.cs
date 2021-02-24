using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class GrowUntil : MonoBehaviour
{
    [SerializeField] Disc disc;
    [SerializeField] float maxRadius, rate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        disc.Radius += Time.deltaTime * rate;

        if(disc.Radius >= maxRadius)
        {
            Destroy(gameObject);
        }
    }
}
