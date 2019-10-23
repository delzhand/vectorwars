using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtons : MonoBehaviour
{
    private CameraController cc;

    public void Start()
    {
        cc = CameraController.GetController();
    }

    public void ZoomIn()
    {
        cc.SetZoom(3.4f, .25f);
    }

    public void ZoomOut()
    {
        cc.SetZoom(6.25f, .25f);
    }

    public void PositionLeft()
    {
        cc.SetAngle(CameraPosition.Left, .25f);
    }

    public void PositionCenter()
    {
        cc.SetAngle(CameraPosition.Center, .25f);
    }

    public void PositionRight()
    {
        cc.SetAngle(CameraPosition.Right, .25f);
    }
}
