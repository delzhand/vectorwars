using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    SpriteRenderer rend;

    public float Duration;
    private float initialAlpha;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        initialAlpha = rend.material.color.a;
        StartFadeOut();
    }

    public void SetDuration(float f)
    {
        Duration = f;
    }

    IEnumerator FadeOutStart()
    {
        for (float f = Duration; f >= -.05f; f -= .05f)
        {
            float percentDone = f / Duration;
            Color c = rend.material.color;
            c.a = initialAlpha * percentDone;
            rend.material.color = c;
            yield return new WaitForSeconds(.05f);

        }
    }

    public void StartFadeOut()
    {
        StartCoroutine("FadeOutStart");
    }
}
