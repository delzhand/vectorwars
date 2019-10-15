using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float duration;
    private float elapsed;

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public float GetPercentage()
    {
        float percentage = Mathf.Min(1, elapsed / duration);
        if (percentage == 1)
        {
            Destroy(this);
        }
        return percentage;
    }
}
