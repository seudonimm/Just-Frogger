using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeAlpha : MonoBehaviour
{
    public bool fadeIn, fadeOut;
    public float t, min, max = 1;
    [SerializeField] Image spr;
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (fadeOut)
        {
            t -= Time.deltaTime;
        }

        if (fadeIn)
        {
            t += Time.deltaTime;
        }

        if (spr)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, Mathf.Lerp(0, max, t));
        }
        else
        {
            text.alpha = Mathf.Lerp(0, max, t);
        }

        t = Mathf.Clamp01(t);
    }
}
