using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

public class Biome : MonoBehaviour
{

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
        spawnEntity(grass[1], terrain.transform.position, 0, 20, 1, 1, true);

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
    private void spawnEntity(GameObject entity, Vector3 position, int rotationY, float spawnrate, int spreadX, int spreadZ, bool foliage)
    {
        int seed = PlayerPrefs.GetInt("Seed");
        Random.seed = seed;
        int randomChance = Random.Range(0, 101);

        if (randomChance < spawnrate)
        {
            int rY = rotationY;

            if (rotationY == 0)
            {
                rY = Random.Range(0, 181) * 2;

            }

            Vector3 outputPos = new Vector3(position.x + spreadX, position.y, position.z + spreadZ);
            Instantiate(entity, outputPos, Quaternion.Euler(0, rY, 0));
            Debug.Log("Spawning Entity: " + entity.ToString() + "@ " + outputPos.ToString());

        }





    }


    public void listBiomes()
    {

        /** Broken lol
         using (StreamReader r = new StreamReader(game.coreFiles() + @"biomes.json"))
            {
                string json = r.ReadToEnd();
                List<Biomes> ro = JsonConvert.DeserializeObject<List<Biomes>>(json);
                Debug.Log(ro[1].biomeName);
            }
        
        
        
        **/
    }
    // Update is called once per frame
    void Update()
    {
        int seed = PlayerPrefs.GetInt("Seed");
        Random.seed = seed;
    }
    public GameObject CreateTile(int xIndex, int yIndex)
    {
        Vector3 test = new Vector3(40, 0, 0);
        if (Player.Location.x >= test.x)
        {
            CurrentBiome = "Plains";
            return plainsBiome(xIndex, yIndex);
        }
        else
        {
            CurrentBiome = "desert";
            return desertBiome(xIndex, yIndex);
        }
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

