using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    SpriteRenderer rend;
    public float MaxAlpha;
    public float Duration;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
        StartFadeIn();
    }

    IEnumerator FadeInStart()
    {
        for(float f = .05f; f <= Duration; f+= .05f)
        {
            float percentDone = f / Duration;
            Color c = rend.material.color;
            c.a = MaxAlpha * percentDone;
            rend.material.color = c;
            yield return new WaitForSeconds(.05f);

        }
    }

    public void StartFadeIn()
    {
        StartCoroutine("FadeInStart");
    }

}
