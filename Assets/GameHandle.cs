using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public string gameDir = @"D:/Games/Prototype/";
    public string coreFiles()
    {
        return  gameDir + @"" + @"Game/";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
