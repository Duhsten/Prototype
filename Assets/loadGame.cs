using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class loadGame : MonoBehaviour
{
    public Canvas loadGameCanvas;
    GameHandle game = new GameHandle();
    public GameObject loadSaveSlot;
    public GameObject SavePanel;
    public string sN;
    // Start is called before the first frame update
    void Start()
    {
        loadSaves();
       
    }
    public void Cancel()
    {
        loadGameCanvas.enabled = false;
    }
    public void loadSaves()
    {
        
        try
        {

            string[] dirs = Directory.GetDirectories(@"Saves");
            Debug.Log("Total Saves: " + dirs.Length);
            foreach (string dir in dirs) // For each Save Folder
            {
                string[] spearator = { "Saves", "\\" };
                int count = 2;

                // using the method 
                string[] strlist = dir.Split(spearator, count,
                       StringSplitOptions.RemoveEmptyEntries);

                foreach (string dira in strlist)
                {
                    sN = dira;
                }

                string s = File.ReadAllText(dir + "/" + "seed");
                string d;
                
                GameObject loadedSave = Instantiate(loadSaveSlot, SavePanel.transform);
                loadedSave.GetComponent<loadsavehandle>();
                loadsavehandle ls = loadedSave.GetComponent<loadsavehandle>();
                ls.updateSlot(sN, s, "test");

                Debug.Log("Loaded Save: " + dir);
                

            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
