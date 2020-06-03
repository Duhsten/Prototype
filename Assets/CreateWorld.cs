using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateWorld : MonoBehaviour
{
    public GenerateBiomes genbio;
    public Text updatetext;
    public GameObject genfin;
    // Start is called before the first frame update
    void Start()
    {
       
        updatetext.text = "Generating World";
    }
    
    // Update is called once per frame
    void Update()
    {
        if (genbio.fin == true)
        {
            genfin.SetActive(true);
            updatetext.text = "Loading World";
            Thread.Sleep(100);
            SceneManager.LoadScene("Simple/Simple");
        }

    }
}
