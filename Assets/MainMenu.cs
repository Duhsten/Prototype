using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Canvas newGameCanvas;
    public Canvas loadGameCanvas;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
    public void newGame()
    {
        newGameCanvas.enabled = true;
    }
    public void loadGame()
    {
        loadGameCanvas.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
