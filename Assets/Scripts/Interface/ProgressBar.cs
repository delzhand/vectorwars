using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float max = 100f;

    private float width;

    public void Start()
    {
        width = transform.Find("BarBorder/BarBackground").GetComponent<RectTransform>().rect.width;
    }

    public void SetValue(float current)
    {
        float percent = current / max;
        Vector2 v = transform.Find("BarBorder/BarBackground/Progress").GetComponent<RectTransform>().sizeDelta;
        v.x = width * percent;
        transform.Find("BarBorder/BarBackground/Progress").GetComponent<RectTransform>().sizeDelta = v;
    }
}
