using UnityEngine;
using System.Collections.Generic;

public class GrassManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    public GameObject grasssmall;
    public GameObject grassmedium;
    public GameObject grasslarge;

    [Header("Terrain")]
    public LayerMask groundLayer;

    [Header("Chunk Settings")]
    public int chunkSize = 100;
    public int viewDistance = 2;

    [Header("Grass Settings")]
    public int grassPerChunk = 100;

    public float minHeight = -50f;
    public float maxHeight = 700f;

    public float maxSlope = 35f;

    [Header("Scale Variation")]
    public float minScale = 0.9f;
    public float maxScale = 1.0f;

    private Mesh smallMesh;
    private Mesh mediumMesh;
    private Mesh largeMesh;

    private Material smallMaterial;
    private Material mediumMaterial;
    private Material largeMaterial;

    private Vector3 smallBaseScale;
    private Vector3 mediumBaseScale;
    private Vector3 largeBaseScale;

    private Dictionary<Vector2Int, GrassChunk> activeChunks =
        new Dictionary<Vector2Int, GrassChunk>();

    private Dictionary<Vector2Int, GrassChunk> chunkCache =
        new Dictionary<Vector2Int, GrassChunk>();

    private Vector2Int currentPlayerChunk;

    void Start()
    {
        activeChunks.Clear();

        smallMesh = grasssmall.GetComponent<MeshFilter>().sharedMesh;
        mediumMesh = grassmedium.GetComponent<MeshFilter>().sharedMesh;
        largeMesh = grasslarge.GetComponent<MeshFilter>().sharedMesh;

        smallMaterial = grasssmall.GetComponent<MeshRenderer>().sharedMaterial;
        mediumMaterial = grassmedium.GetComponent<MeshRenderer>().sharedMaterial;
        largeMaterial = grasslarge.GetComponent<MeshRenderer>().sharedMaterial;

        smallBaseScale = grasssmall.transform.localScale;
        mediumBaseScale = grassmedium.transform.localScale;
        largeBaseScale = grasslarge.transform.localScale;

        Debug.Log("Small Scale = " + smallBaseScale);
        Debug.Log("Medium Scale = " + mediumBaseScale);
        Debug.Log("Large Scale = " + largeBaseScale);

        Debug.Log("Cached Chunks: " + chunkCache.Count);

        UpdateChunks();
    }

    void Update()
    {
        Vector2Int newChunk = GetPlayerChunk();

        if (newChunk != currentPlayerChunk)
        {
            UpdateChunks();
        }

        RenderGrass();
    }

    Vector2Int GetPlayerChunk()
    {
        return new Vector2Int(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.z / chunkSize)
        );
    }

    void UpdateChunks()
    {
        currentPlayerChunk = GetPlayerChunk();

        activeChunks.Clear();

        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int z = -viewDistance; z <= viewDistance; z++)
            {
                Vector2Int coord =
                    currentPlayerChunk + new Vector2Int(x, z);

                GrassChunk chunk;

                if (chunkCache.TryGetValue(coord, out chunk))
                {
                    // Ambil dari cache
                }
                else
                {
                    chunk = GenerateChunk(coord);
                    chunkCache.Add(coord, chunk);
                }

                activeChunks.Add(coord, chunk);
            }
        }
    }

    GrassChunk GenerateChunk(Vector2Int coord)
    {
        GrassChunk chunk = new GrassChunk(coord);

        float startX = coord.x * chunkSize;
        float startZ = coord.y * chunkSize;

        for (int i = 0; i < grassPerChunk; i++)
        {
            Vector3 rayStart = new Vector3(
                Random.Range(startX, startX + chunkSize),
                1500f,
                Random.Range(startZ, startZ + chunkSize)
            );

            if (!Physics.Raycast(rayStart, Vector3.down,
                out RaycastHit hit,
                3000f,
                groundLayer))
                continue;

            if (hit.point.y < minHeight ||
                hit.point.y > maxHeight)
                continue;

            float slope =
                Vector3.Angle(hit.normal, Vector3.up);

            if (slope > maxSlope)
                continue;

            float roll = Random.Range(0f, 100f);

            List<Matrix4x4> targetList;

            if (roll < 70f)
            {
                targetList = chunk.smallMatrices;
            }
            else if (roll < 90f)
            {
                targetList = chunk.mediumMatrices;
            }
            else
            {
                targetList = chunk.largeMatrices;
            }

            float randomScale =
    Random.Range(minScale, maxScale);

            Vector3 baseScale;

            if (roll < 70f)
            {
                baseScale = smallBaseScale;
            }
            else if (roll < 90f)
            {
                baseScale = mediumBaseScale;
            }
            else
            {
                baseScale = largeBaseScale;
            }

            Matrix4x4 matrix =
                Matrix4x4.TRS(
                    hit.point,
                    Quaternion.Euler(
                        0f,
                        Random.Range(0f, 360f),
                        0f),
                    baseScale * randomScale
                );

            targetList.Add(matrix);
        }

        return chunk;
    }

    void RenderGrass()
    {
        foreach (var chunk in activeChunks.Values)
        {
            DrawBatch(
                smallMesh,
                smallMaterial,
                chunk.smallMatrices);

            DrawBatch(
                mediumMesh,
                mediumMaterial,
                chunk.mediumMatrices);

            DrawBatch(
                largeMesh,
                largeMaterial,
                chunk.largeMatrices);
        }
    }

    void DrawBatch(
        Mesh mesh,
        Material material,
        List<Matrix4x4> matrices)
    {
        int batchSize = 1023;

        for (int i = 0; i < matrices.Count; i += batchSize)
        {
            int count =
                Mathf.Min(batchSize,
                matrices.Count - i);

            Graphics.DrawMeshInstanced(
                mesh,
                0,
                material,
                matrices.GetRange(i, count)
            );
        }
    }
}