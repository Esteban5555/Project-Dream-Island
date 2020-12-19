using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{
    public Tilemap WalkableTilemap;
    public Vector3Int[,] cells;
    BoundsInt bounds;
    Astar astar;
    // Start is called before the first frame update
    void Start()
    {
        WalkableTilemap.CompressBounds();
        bounds = WalkableTilemap.cellBounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatGrid() {
        cells = new Vector3Int[bounds.size.x, bounds.size.y];

        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                if (WalkableTilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    cells[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    cells[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
    }
}
