using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Drawing;


public class Biome : MonoBehaviour
{
    public List<Biomes> biomeList;
        public SpawnEntity SpawnEntity;

    [Header("Desert Biome")]

    public Gradient desertGradient;

    public AudioSource desertAmbient;
    [Header("Plains Biome")]
    public GameObject[] grass;
    public Gradient plainsGradient;
    [Header("Other Settings")]
    public string CurrentBiome;
    public player Player;
    public GameObject terrainTilePrefab;
    private bool rad;
    public MeshCollider test;

    //random shit
    private int random2;


    GameHandle game = new GameHandle();
    private Vector3 terrainSize = new Vector3(20, 1, 20);
    public TerrainControllerSimple terrianControl;
    private Vector2 startOffset;
    // Start is called before the first frame update
    void Start()
    {
        biomeList = JsonConvert.DeserializeObject<List<Biomes>>(File.ReadAllText(@"Saves/" + PlayerPrefs.GetString("GameSave") + "/biomedata.json"));

        listBiomes();
       
    }
    public void loadBiome()
    {

    }
    //Testing a way to approach biomes
    public GameObject plainsBiome(int xIndex, int yIndex)
    {
        Biome biome = new Biome();
        GameObject terrain = Instantiate(
         terrainTilePrefab,
         new Vector3(terrainSize.x * xIndex, terrainSize.y, terrainSize.z * yIndex),
         Quaternion.identity
        );
        terrain.name = TrimEnd(terrain.name, "(Clone)") + " [" + xIndex + " , " + yIndex + "]";

        terrianControl.terrainTiles.Add(new Vector2(xIndex, yIndex), terrain);
        //  Debug.Log("Plain Biome");
        GenerateMeshSimple gm = terrain.GetComponent<GenerateMeshSimple>();
        gm.TerrainSize = terrainSize;
        gm.Gradient = plainsGradient;
        gm.NoiseScale = 3f;
        gm.CellSize = 1f;
        gm.NoiseOffset = NoiseOffsetPlains(xIndex, yIndex);
        gm.Generate();
       

        return terrain;



    }
    public GameObject desertBiome(int xIndex, int yIndex)
    {
        desertAmbient.Play();
        Biome biome = new Biome();
        GameObject terrain = Instantiate(
         terrainTilePrefab,
         new Vector3(terrainSize.x * xIndex, terrainSize.y, terrainSize.z * yIndex),
         Quaternion.identity
        );
        terrain.name = TrimEnd(terrain.name, "(Clone)") + " [" + xIndex + " , " + yIndex + "]";

        terrianControl.terrainTiles.Add(new Vector2(xIndex, yIndex), terrain);
        // Debug.Log("Hill Biome");
        GenerateMeshSimple gm = terrain.GetComponent<GenerateMeshSimple>();
        gm.TerrainSize = new Vector3(20, 1, 20); ;
        gm.Gradient = desertGradient;
        gm.NoiseScale = 3f;
        gm.CellSize = 1f;
        gm.NoiseOffset = NoiseOffsetHill(xIndex, yIndex);
        gm.Generate();

        SpawnEntity.dustStorm(terrain.transform.position);
        SpawnEntity.Rock(terrain.transform.position);
        SpawnEntity.Mountain(terrain.transform.position);
        return terrain;
    }
   

    public void listBiomes()
    {
    
     
    }
    // Update is called once per frame
    void Update()
    {
        int seed = PlayerPrefs.GetInt("Seed");
        Random.seed = seed;
    }
    public GameObject CreateTile(int xIndex, int yIndex)
    {
        foreach (Biomes bio in biomeList)
        {
            Vector2 point = new Vector2(terrainSize.x * xIndex, terrainSize.z * yIndex);
            float x = terrainSize.x * xIndex;
            float y = terrainSize.z * yIndex;
            Point s = new Point(System.Convert.ToInt32(x), System.Convert.ToInt32(y));
            if (bio.recc.Contains(s))
            {
                if (bio.Biome == "Plains")
                {
                    Debug.Log("Generating Plains");
                    return plainsBiome(xIndex, yIndex);

                }
                else if (bio.Biome == "Desert")
                {
                    Debug.Log("Generating Desert");
                    return desertBiome(xIndex, yIndex);
                }
            }
        }
        try
        {
            
            Biomes result = biomeList.Find(x => x.x == terrainSize.x * xIndex && x.y == terrainSize.z * yIndex);
            if (result.x == terrainSize.x * xIndex && result.y == terrainSize.z * yIndex)
            {
                if (result.Biome == "Plains")
                {
                    Debug.Log("Generating Plains");
                    return plainsBiome(xIndex, yIndex);
                    
                }
                else if (result.Biome == "Desert")
                {
                    Debug.Log("Generating Desert");
                    return desertBiome(xIndex, yIndex);
                }
            }
        }
        catch
        {

        }
        Debug.Log("TESTTTTT");
        return plainsBiome(xIndex, yIndex);

    }
    private static string TrimEnd(string str, string end)
    {

        if (str.EndsWith(end))
            return str.Substring(0, str.LastIndexOf(end));
        return str;
    }
    private Vector2 NoiseOffsetHill(int xIndex, int yIndex)
    {
        Vector2 noiseOffset = new Vector2(
            (xIndex * 3f + startOffset.x) % 256,
            (yIndex * 3f + startOffset.y) % 256
        );
        //account for negatives (ex. -1 % 256 = -1). needs to loop around to 255
        if (noiseOffset.x < 0)
            noiseOffset = new Vector2(noiseOffset.x + 256, noiseOffset.y);
        if (noiseOffset.y < 0)
            noiseOffset = new Vector2(noiseOffset.x, noiseOffset.y + 256);
        return noiseOffset;
    }
    private Vector2 NoiseOffsetPlains(int xIndex, int yIndex)
    {
        Vector2 noiseOffset = new Vector2(
            (xIndex * 3f + startOffset.x) % 256,
            (yIndex * 3f + startOffset.y) % 256
        );
        //account for negatives (ex. -1 % 256 = -1). needs to loop around to 255
        if (noiseOffset.x < 0)
            noiseOffset = new Vector2(noiseOffset.x + 256, noiseOffset.y);
        if (noiseOffset.y < 0)
            noiseOffset = new Vector2(noiseOffset.x, noiseOffset.y + 256);
        return noiseOffset;
    }
}

