using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] Transform cam; //camera
    [SerializeField] Vector3 camInitialPos;
    [SerializeField] float shakeDuration, defaultShakeDuration;// duration of screen shake
    [SerializeField] float shakeMagnitude;
    [SerializeField] float dampingSpeed; // how quickly shake effects stops

    [SerializeField] string shakeWhenThisHits; //tag of object that will cause the shake when collided with

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camInitialPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
        shakeDuration = Mathf.Clamp(shakeDuration, 0, defaultShakeDuration);
    }

    void HitByRayCast()
    {
        shakeDuration = defaultShakeDuration;
    }

    void Shake()
    {
        if (shakeDuration > 0)
        {
            cam.position = camInitialPos + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            cam.position = camInitialPos;
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(shakeWhenThisHits))
        {
            shakeDuration = defaultShakeDuration;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(shakeWhenThisHits))
        {
            shakeDuration = defaultShakeDuration;
        }

    }
}
