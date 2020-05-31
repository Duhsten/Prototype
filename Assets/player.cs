using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject playerPref;
    public Vector3 Location;
    public string playerLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Location =  playerPref.transform.position;
        string x = playerPref.transform.position.x.ToString();
        string y = playerPref.transform.position.y.ToString();
        string z = playerPref.transform.position.z.ToString();
        playerLocation = x + ", " + y + ", " + z;
    }
}
