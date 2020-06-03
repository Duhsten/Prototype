using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
[Serializable]
public class Biomes

{
    [SerializeField]
    public string Biome { get; set; }
    [SerializeField]
    public int x { get; set; }
    public int y { get; set; }
    public Rectangle recc { get; set; }
    // Start is called before the first frame update

}
[SerializeField]
public class BiomeList
{
    [SerializeField]
    public List<Biomes> list { get; set; }
}

