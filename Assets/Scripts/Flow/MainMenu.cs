using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HomeClicked()
    {
        StateManager.DismissSubscreens();
    }

    public void DeployClicked()
    {
        StateManager.DismissSubscreens();
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/Deploy Menu"), StateManager.GetController().UiRoot, false);
        g.name = "Deploy Menu";
    }

    public void VectorsClicked()
    {
        StateManager.DismissSubscreens();
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/Vector Menu"), StateManager.GetController().UiRoot, false);
        g.name = "Vector Menu";
    }

    public void GenerateClicked()
    {
        StateManager.DismissSubscreens();
    }

    public void ConfigClicked()
    {
        StateManager.DismissSubscreens();
    }
}
