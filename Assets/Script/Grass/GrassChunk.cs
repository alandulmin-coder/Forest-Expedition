using UnityEngine;
using System.Collections.Generic;

public class GrassChunk
{
    public Vector2Int chunkCoord;

    public List<Matrix4x4> smallMatrices = new();
    public List<Matrix4x4> mediumMatrices = new();
    public List<Matrix4x4> largeMatrices = new();

    public GrassChunk(Vector2Int coord)
    {
        chunkCoord = coord;
    }
}