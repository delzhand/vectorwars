using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public CameraController  MainCameraController;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.LookAt(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 touchPos;
        //if (GetEndedTouch(out touchPos))
        //{
        //    Ray raycast = Camera.main.ScreenPointToRay(touchPos);
        //    RaycastHit r;
        //    if (Physics.Raycast(raycast, out r, Mathf.Infinity))
        //    {
        //        Tile t = r.collider.transform.parent.parent.GetComponentInChildren<Tile>();
        //        if (t)
        //        {
        //            Console.Log(t.ToString());
        //            MainCameraController.SetTarget(t.GetTop(), .25f);
        //        }
        //    }
        //}
    }

    public static CombatManager GetController()
    {
        return GameObject.FindGameObjectWithTag("CombatController").GetComponent<CombatManager>();
    }
}
