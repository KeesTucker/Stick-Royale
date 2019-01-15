using UnityEngine;

[CreateAssetMenu(fileName = "New Biome", menuName = "World/Biome")]
public class Biome : ScriptableObject
{
    new public string name = "New Biome";
    public int BiomeIndex;
    public Color grassColor;
    public Color baseColor;
    public Color trunkColor;
    public Color rockColor;

    public Sprite[] detail;
    public GameObject[] trees;
}
