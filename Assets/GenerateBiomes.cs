using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GenerateBiomes : MonoBehaviour
{
    public GameLog game = new GameLog();
    public List<Biomes> biomeList = new List<Biomes>();
    private int count;
    private GameObject newrect;
    public Canvas Draw;
    public GameObject rect;
    public List<Rectangle> biomes = new List<Rectangle>();
    public bool n;
    public int countb;
    public int genMax;
    public bool fin = false;
    // Start is called before the first frame update
    void Start()
    {
        
        int seed = PlayerPrefs.GetInt("Seed");
        Random.seed = seed;
        biomes.Clear();
    

       
        generateBiome(Random.Range(100, 500), Random.Range(100, 500), Random.Range(-10000, 10000), Random.Range(-10000, 10000));
       
    }


    // Update is called once per frame
    void Update()
    {
        generateBiome(Random.Range(100, 500), Random.Range(100, 500), Random.Range(-10000, 10000), Random.Range(-10000, 10000));

    }
    public void generateBiome(int h, int w, int randomX, int randomY)
    {

        if (genMax > 1000)
        {
            writeBiome();
            fin = true;
        }
        if (genMax <= 1000)
        {

            // Creates a Rectangle and then checks if it intersects with anything and doesnt add it to the list of it does
            Rectangle rec = new Rectangle(randomX, randomY, w, h);
            
            if (checkList(rec) == false)
            {
             


            }
            // Creates Visual
            if (checkArea(randomX, randomY) == false && checkList(rec) == false)
            {
                biomes.Add(rec);
                
                newrect = Instantiate(rect, new Vector3(randomX, randomY, 0), Quaternion.Euler(0, 0, 0), Draw.transform) as GameObject;

                newrect.name = "Biome# " + countb;
                genMax = genMax + 1;
                countb = countb + 1;
                RectTransform rectran = newrect.GetComponent<RectTransform>();
                Text biomeText = newrect.GetComponentInChildren<Text>();
                rectran.sizeDelta = new Vector2(w, h);
                Image image = newrect.GetComponent<Image>();
                //
                int randomC = Random.Range(0, 101);
                if (randomC <= 50)
                {
                    image.color = new Color32(255, 255, 255, 255);
                    biomeText.text = "Desert";
                    biomeList.Add(new Biomes { Biome = "Desert", x = randomX, y = randomY, recc = rec });
                    Debug.Log("Creating Biome: " + "Desert, " + "(" + randomX + ", " + randomY + ")");

                }
                else
                {
                    image.color = new Color32(255, 255, 255, 255);
                    biomeText.text = "Plains";
                    biomeList.Add(new Biomes { Biome = "Plains", x = randomX, y = randomY, recc = rec });
                    Debug.Log("Creating Biome: " + "Plains, " + "(" + randomX + ", " + randomY + ")");
                }

            }
            if (n == true)
            {
                Debug.Log("Destroyed Object");

            }
            else n = false;

        }
          



           



        
        
    }
    public void writeBiome()
    {

        Debug.Log("Converting");
        string save = PlayerPrefs.GetString("GameSave");
        string filejson = @"Saves/" + save + "/biomedata.json";
        using (StreamWriter file = File.CreateText(filejson))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            //serialize object directly into file stream
            serializer.Serialize(file, biomeList);
        }

    }

    public bool checkList(Rectangle r)
    {
        bool localcheck = false;
        foreach (Rectangle biom in biomes)
        {
            if (biom.IntersectsWith(r))
            {
                localcheck = true;

               // game.Log("Intersection checkList");

            }
            else if (r.IntersectsWith(biom))
            {

                localcheck = true;
              //  game.Log("Intersection checkList");
            }
        }
        return localcheck;
    }
 
    public bool checkArea(int x, int y)
    {
        bool localcheck= false;
        
        foreach (Rectangle biom in biomes)
        {
            if (biom.X == x && biom.Y == y)
            {
                //game.Log("Intersection checkArea");
                localcheck = true;
            }
            else if(biom.Contains(x,y))
            {
                localcheck = true;
               // game.Log("Intersection checkArea");
            }
            else n = false;
           
        }
        return localcheck;


    }

}
