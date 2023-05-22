using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);

    }
}
