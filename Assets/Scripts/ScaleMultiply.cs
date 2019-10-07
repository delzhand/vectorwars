using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMultiply : MonoBehaviour
{
    public float Scale = 1;
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = initialScale * Scale;
    }
}
