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
    private Vector3 angleOrigin;
    private Vector3 angleTarget;
    private float zoomOrigin;
    private float zoomTarget;
    private CameraPosition cameraPosition = CameraPosition.Center;

    private Vector3 dragOrigin;
    public float dragThreshold = .1f;

    public static CameraController GetController()
    {
        foreach (CameraController c in FindObjectsOfType<CameraController>())
        {
            return c;
        }
        return null;
    }

    public void Start()
    {
        zoomTarget = 3.4f;
        angleTarget = PositionToVector(CameraPosition.Center);
        TouchController tc = TouchController.GetController();
        tc.startTouch += StartTouch;
        tc.endTouch += EndTouch;
    }

    private void Update()
    {
        Timer t = GetComponent<Timer>();
        if (t && t.GetPercentage() < 1)
        {
            //transform.localPosition = Vector3.Lerp(origin, target, Curve.CosInverse(t.GetPercentage()));

            Vector3 angle = Vector3.Lerp(angleOrigin, angleTarget, Curve.CosInverse(t.GetPercentage()));
            transform.Find("CameraRun").transform.localPosition = new Vector3(0, 0, angle.x);
            transform.Find("CameraRun/CameraRise").transform.localPosition = new Vector3(0, angle.y, 0);
            transform.localRotation = Quaternion.Euler(0, angle.z, 0);

            Camera.main.orthographicSize = Mathf.Lerp(zoomOrigin, zoomTarget, t.GetPercentage());
        }
        else
        {
            //origin = target;
            //transform.localPosition = target;

            transform.Find("CameraRun").transform.localPosition = new Vector3(0, 0, angleTarget.x);
            transform.Find("CameraRun/CameraRise").transform.localPosition = new Vector3(0, angleTarget.y, 0);
            angleOrigin = angleTarget;
            transform.localRotation = Quaternion.Euler(0, angleTarget.z, 0);

            Camera.main.orthographicSize = zoomTarget;
            zoomOrigin = zoomTarget;
        }
        Camera.main.transform.LookAt(transform.position);
    }

    private void LateUpdate()
    {
        TouchController tc = TouchController.GetController();
        Vector2? vX = tc.Position();
        Vector2? vD = tc.Delta();
        if (vX.HasValue && vD.HasValue && vD.Value.magnitude > dragThreshold)
        {
            Vector2 v = vX.Value;
            Ray raycast = Camera.main.ScreenPointToRay(v);
            RaycastHit r;
            if (Physics.Raycast(raycast, out r, Mathf.Infinity))
            {
                Debug.DrawRay(raycast.origin, r.point - raycast.origin, Color.red, 3);
                Vector3 difference = r.point - dragOrigin;
                Vector3 target = transform.position - difference;
                transform.position = Vector3.Lerp(transform.position, target, .75f);
                dragOrigin = r.point;
            }
        }
    }

    public void StartTouch(Vector2 v)
    {
        Ray raycast = Camera.main.ScreenPointToRay(v);
        RaycastHit r;
        if (Physics.Raycast(raycast, out r, Mathf.Infinity))
        {
            dragOrigin = r.point;
            Debug.DrawRay(raycast.origin, r.point - raycast.origin, Color.red, 3);
        }
    }

    public void EndTouch(Vector2 v)
    {
    }

    //public void SetTarget(Vector3 target, float duration)
    //{
    //    origin = transform.position;
    //    this.target = target;
    //    gameObject.AddComponent<Timer>().SetDuration(duration);
    //}

    public void SetAngle(CameraPosition cpos, float duration)
    {
        angleOrigin = PositionToVector(cameraPosition);
        angleTarget = PositionToVector(cpos);
        cameraPosition = cpos;
        gameObject.AddComponent<Timer>().SetDuration(duration);
    }

    public void SetZoom(float zoom, float duration)
    {
        this.zoomOrigin = Camera.main.orthographicSize;
        this.zoomTarget = zoom;
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
