using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTest : MonoBehaviour {

    public byte[,] grid; // this is the array of things we own
    // assuming the map is a 5x5 and that the border is one tile wide
    
    void Start () {
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        grid = new byte[bounds.size.x, bounds.size.y];
        Debug.Log(bounds.size.x + " x " + bounds.size.y);

        for (int y = 0; y < bounds.size.y; y++) {
            for (int x = 0; x < bounds.size.x; x++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null) {
                    grid[x, y] = 1;

                } else {
                    grid[x,y] = 0;
                }
            }
        }
    }   
}