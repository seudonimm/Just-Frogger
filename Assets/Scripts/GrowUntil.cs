using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class GrowUntil : MonoBehaviour
{
    [SerializeField] Disc disc;
    [SerializeField] float maxRadius, rate, t;

    // Start is called before the first frame update
    void Start()
    {
        t = 1;
    }

    // Update is called once per frame
    void Update()
    {
        disc.Radius += Time.deltaTime * rate;

        var discColor = disc.Color;
        discColor.a = Mathf.Lerp(0, 1, t);
        disc.Color = discColor;

        t -= Time.deltaTime * 3;

        if (t <= 0)
        {
            Destroy(gameObject);
            //StartCoroutine(FadeAway());
        }
    }

    IEnumerator FadeAway()
    {
        disc.Color = new Color(disc.Color.r, disc.Color.g, disc.Color.b, Mathf.Lerp(0, 1, t));

        yield return new WaitForSeconds(t);

        Destroy(gameObject);

    }
}
