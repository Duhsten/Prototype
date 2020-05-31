using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class devui : MonoBehaviour
{
    public player playerInfo;
    public Biome biome;
    public Text playerloc;
    public Text seed;
    public Text currentBio;
    public Text worldsave;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        currentBio.text = biome.CurrentBiome;
        worldsave.text = PlayerPrefs.GetString("GameSave");
        seed.text = PlayerPrefs.GetInt("Seed").ToString();
        playerloc.text = playerInfo.playerLocation;
    }
}
