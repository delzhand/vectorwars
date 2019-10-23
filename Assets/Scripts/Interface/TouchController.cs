using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public delegate void TouchEvent(Vector2 origin);
    public event TouchEvent startTouch;
    public event TouchEvent endTouch;

    private int touchIndex = -1;

    public static TouchController GetController()
    {
        foreach (TouchController tc in FindObjectsOfType<TouchController>())
        {
            return tc;
        }
        return null;
    }


    void Update()
    {
        GetTouchIndex();
    }

    public Vector2? Position()
    {
        Vector2 v = Vector2.zero;
        if (touchIndex >= 0)
        {
            return Input.touches[touchIndex].position;
        }
        return null;
    }

    public Vector2? Delta()
    {
        Vector2 v = Vector2.zero;
        if (touchIndex >= 0)
        {
            return Input.touches[touchIndex].deltaPosition;
        }
        return null;
    }

    private void GetTouchIndex()
    {
        if (touchIndex == -1)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                if (Input.touches[i].phase == TouchPhase.Began)
                {
                    touchIndex = i;
                    if (startTouch != null)
                    {
                        startTouch(Input.touches[i].position);
                    }
                    return;
                }
            }
        }
        else if (Input.touches[touchIndex].phase == TouchPhase.Ended)
        {
            if (endTouch != null)
            {
                endTouch(Input.touches[touchIndex].position);
            }
            touchIndex = -1;
        }
    }
}
