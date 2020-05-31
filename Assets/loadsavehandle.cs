using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadsavehandle : MonoBehaviour
{
    public Text saveName, seed, date;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void updateSlot(string save, string seedName, string dat)
    {
        saveName.text = save;
        seed.text = seedName;
        date.text = dat;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
