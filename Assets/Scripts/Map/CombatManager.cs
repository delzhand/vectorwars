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
        
    }

    public static CombatManager GetController()
    {
        return GameObject.FindGameObjectWithTag("CombatController").GetComponent<CombatManager>();
    }
}
