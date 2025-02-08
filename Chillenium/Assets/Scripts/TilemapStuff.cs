using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTest : MonoBehaviour {

    public byte[,] grid = new byte[10, 10]; // this is the array of things we own
    // assuming the map is a 8x8 and that the border is one tile wide
    
    void Start () {
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null) {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    grid[x,y] = 1;

                } else {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }        
    }   
}