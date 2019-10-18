using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntryPoint : MonoBehaviour
{
    public VectorCombat VCombat;

    // Start is called before the first frame update
    void Start()
    {
        GameObject vectorAvatar = (GameObject)Instantiate(Resources.Load("Character/DemoVector"), GetComponent<Tile>().GetTop(), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
