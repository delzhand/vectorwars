using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraPosition
{
    Left,
    Center,
    Right,
}
public class CameraController : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 target;
    private Vector3 angleOrigin;
    private Vector3 angleTarget;
    private CameraPosition cameraPosition = CameraPosition.Center;

    public void Start()
    {
        target = Vector3.zero;
        angleTarget = PositionToVector(CameraPosition.Center);
    }

    private void Update()
    {
        Timer t = GetComponent<Timer>();
        if (t && t.GetPercentage() < 1)
        {
            transform.localPosition = Vector3.Lerp(origin, target, Curve.CosInverse(t.GetPercentage()));
            Vector3 angle = Vector3.Lerp(angleOrigin, angleTarget, Curve.CosInverse(t.GetPercentage()));
            transform.Find("CameraRun").transform.localPosition = new Vector3(0, 0, angle.x);
            transform.Find("CameraRun/CameraRise").transform.localPosition = new Vector3(0, angle.y, 0);
            transform.localRotation = Quaternion.Euler(0, angle.z, 0);
        }
        else
        {
            transform.localPosition = target;
            transform.Find("CameraRun").transform.localPosition = new Vector3(0, 0, angleTarget.x);
            transform.Find("CameraRun/CameraRise").transform.localPosition = new Vector3(0, angleTarget.y, 0);
            transform.localRotation = Quaternion.Euler(0, angleTarget.z, 0);
        }
        Camera.main.transform.LookAt(transform.position);
    }

    public void SetTarget(Vector3 target, float duration)
    {
        origin = transform.position;
        this.target = target;
        gameObject.AddComponent<Timer>().SetDuration(duration);
    }

    public void SetAngle(CameraPosition cpos, float duration)
    {
        angleOrigin = PositionToVector(cameraPosition);
        angleTarget = PositionToVector(cpos);
        cameraPosition = cpos;
        gameObject.AddComponent<Timer>().SetDuration(duration);
    }

    private static Vector3 PositionToVector(CameraPosition c)
    {
        switch (c)
        {
            case CameraPosition.Left:
                return new Vector3(-2, 10, 0);
            case CameraPosition.Right:
                return new Vector3(-2, 10, -90);
            default:
                return new Vector3(-10, 10, -45);
        }
    }
}
