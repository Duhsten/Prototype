using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devbackend : MonoBehaviour
{
    public Canvas DevMenu;
    // Start is called before the first frame update
    void Start()
    {
         DevMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if(DevMenu.enabled == true)
            {
                DevMenu.enabled = false;
            }
            else if(DevMenu.enabled == false)
            {
                DevMenu.enabled = true;
            }
        }
    }
}
