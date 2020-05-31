using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewWorld : MonoBehaviour
{
    public InputField worldName;
    public InputField seedInput;
    public int Seed;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log(Seed);
        Debug.Log("Start");
    }
    public void Create()
    {
        if(seedInput.text == "")
        {
         
           float s = Random.Range(0f, 9999999999f);
           Seed = (int)System.Math.Round(s);
           Debug.Log(Seed);
           PlayerPrefs.SetInt("Seed", Seed);

        }
        else
        {
            PlayerPrefs.SetInt("Seed", System.Int32.Parse(seedInput.text));
        }
     

        if(!Directory.Exists("Saves"))
        {
            Directory.CreateDirectory("Saves");
        }
        SaveManager sav = new SaveManager();
        sav.createSave(worldName.text, Seed);
        SceneManager.LoadScene("Simple/Simple");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
