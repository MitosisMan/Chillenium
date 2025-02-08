using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTest : MonoBehaviour {
    void Start () {
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        byte[,] grid = null;

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