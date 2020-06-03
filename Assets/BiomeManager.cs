using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateBiome(bool firstGen)
    {
        if(firstGen == true)
        {

        }
        Vector3 playerPos = player.transform.position;

    }
    public void CreatePoints(int h, int w)
    {

        int randomx = 3;// 3 is going to be Random (This is the random point where a rectangle will start)
        int randomy = 3; // 3 is going to be Random (This is the random point where a rectangle will start)
        Vector2 topr = new Vector2(randomx, randomy); // The Top Right point
        int secp = randomy - h;
        Vector2 botr = new Vector2(randomx, secp); // The Bottom Right point
        int secp2 = randomx - w;
        Vector2 topl = new Vector2(secp2, randomy); // The Top Left point
        int secp3 = randomx - w;
        Vector2 botl = new Vector2(secp3, secp); // The Bottom Left point

        // Find the High and Low
        float lowX = botl.x;
        float highX = topr.x;
        float lowY = botl.y;
        float highY = topr.y;


        Debug.Log(topr);
        Debug.Log(botr);
        Debug.Log(topl);
        Debug.Log(botl);

    }
}
