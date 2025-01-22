using System;
using UnityEngine;
public class GenerateMap : MonoBehaviour
{
    public int width = 256;         // Width of the terrain
    public int height = 256;        // Height of the terrain
    public float scale = 20f;       // Scale of the terrain features
    public float heightMultiplier = 2f;  // Multiplier for terrain height
    public float offsetX = 100f;    // Horizontal offset for noise
    public float offsetY = 100f;    // Vertical offset for noise

    private Terrain terrain;        // Reference to the terrain

    void Start()
    {
        terrain = GetComponent<Terrain>();
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        // Generate a heightmap based on Perlin noise
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Get the Perlin noise value for each point in the grid
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;

                // Use Mathf.PerlinNoise to generate smooth noise
                float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);

                // Set the height at each point in the terrain
                heights[x, y] = perlinValue * heightMultiplier;
            }
        }

        // Apply the heightmap to the terrain
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
