using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FormSquadsClicked()
    {
        StateManager.DismissSubscreens();
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/Squad Subscreen"), StateManager.GetController().UiRoot, false);
        g.name = "Squad Subscreen";
    }
}
