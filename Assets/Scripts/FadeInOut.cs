using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] float t, alpha, min, max;
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        alpha = Mathf.Lerp(min, max, t);
        t = Mathf.PingPong(Time.time, 1);
        text.alpha = alpha;

        //transform.localScale = new Vector3(size, size, size);
    }
}
