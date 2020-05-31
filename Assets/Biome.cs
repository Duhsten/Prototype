using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

public class Biome : MonoBehaviour
{
    private bool rad;
    public MeshCollider test;
    public GameObject[] grass;
    public GameObject[] mountain;
    public GameObject rock;
    public string CurrentBiome;
    public player Player;
    public GameObject terrainTilePrefab;
    public Gradient plainsGradient;
    public Gradient hillsGradient;
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
        Debug.Log("Plain Biome");
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
    public GameObject hillsBiome(int xIndex, int yIndex)
    {
        Biome biome = new Biome();
        GameObject terrain = Instantiate(
         terrainTilePrefab,
         new Vector3(terrainSize.x * xIndex, terrainSize.y, terrainSize.z * yIndex),
         Quaternion.identity
        );
        terrain.name = TrimEnd(terrain.name, "(Clone)") + " [" + xIndex + " , " + yIndex + "]";

        terrianControl.terrainTiles.Add(new Vector2(xIndex, yIndex), terrain);
        Debug.Log("Hill Biome");
        GenerateMeshSimple gm = terrain.GetComponent<GenerateMeshSimple>();
        gm.TerrainSize = new Vector3(20, 1, 20); ;
        gm.Gradient = hillsGradient;
        gm.NoiseScale = 3f;
        gm.CellSize = 1f;
        gm.NoiseOffset = NoiseOffsetHill(xIndex, yIndex);
        gm.Generate();

        // Biome Enviroment Handle
        // Mountain Entity 
        float rate;
        if (Random.Range(0, 101) <= 5) // Rate at which Mountains will Spawn
        {
            int x;
            int z;
            if (rad == true)
            {
                spawnEntity(mountain[1], terrain.transform.position, 0, 100f, Random.Range(100, 200), Random.Range(100, 200), false);
                rad = !rad;

            }
            else if (rad == false)
            {
                spawnEntity(mountain[1], terrain.transform.position, 0, 100f, Random.Range(-100, -200), Random.Range(-100, -200), false);
                rad = !rad;

            }

        }
        else
        {
            //  spawnEntity(mountain[1], terrain.transform.position, 0, 30f, Random.Range(-4f, 4f), Random.Range(-4f, 4f), false);

        }
        return terrain;



    }
    private void spawnEntity(GameObject entity, Vector3 position, int rotationY, float spawnrate, int spreadX, int spreadZ, bool foliage )
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
        
            Vector3 outputPos = new Vector3(position.x + spreadX, 0, position.z + spreadZ);
            Instantiate(entity, outputPos, Quaternion.Euler(0, rY, 0));

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
            CurrentBiome = "Hills";
            return hillsBiome(xIndex, yIndex);
        }
    }
    private static string TrimEnd(string str, string end)
        {
        Debug.Log("Trimming");
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

