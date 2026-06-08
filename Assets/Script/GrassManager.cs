using UnityEngine;
using System.Collections.Generic;

public class GrassManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject grasslarge;
    public GameObject grassmedium;
    public GameObject grasssmall;

    [Header("Terrain Area")]
    public float minX = 1712f;
    public float maxX = 7762f;

    public float minZ = -2402f;
    public float maxZ = 3622f;

    [Header("Grass Settings")]
    public int grassCount = 5000;

    public float minHeight = -50f;
    public float maxHeight = 700f;

    public float maxSlope = 35f;

    [Header("Scale Variation")]
    public float minScale = 0.9f;
    public float maxScale = 1.1f;

    [Header("Layer")]
    public LayerMask groundLayer;

    private GameObject grassParent;

    public void GenerateGrass()
    {
        ClearGrass();

        grassParent = new GameObject("GeneratedGrass");

        for (int i = 0; i < grassCount; i++)
        {
            Vector3 rayStart = new Vector3(
                Random.Range(minX, maxX),
                1500f,
                Random.Range(minZ, maxZ)
            );

            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 3000f, groundLayer))
            {
                float height = hit.point.y;

                if (height < minHeight || height > maxHeight)
                    continue;

                float slope = Vector3.Angle(hit.normal, Vector3.up);

                if (slope > maxSlope)
                    continue;

                GameObject prefab = GetRandomGrass();

                GameObject grass =
                    Instantiate(
                        prefab,
                        hit.point,
                        Quaternion.Euler(0f, Random.Range(0f, 360f), 0f),
                        grassParent.transform
                    );

                float scale = Random.Range(minScale, maxScale);

                grass.transform.localScale *= scale;
            }
        }
    }

    public void ClearGrass()
    {
        GameObject old = GameObject.Find("GeneratedGrass");

        if (old != null)
        {
#if UNITY_EDITOR
            DestroyImmediate(old);
#else
            Destroy(old);
#endif
        }
    }

    private GameObject GetRandomGrass()
    {
        float random = Random.Range(0f, 100f);

        if (random < 70f)
            return grasssmall;

        if (random < 90f)
            return grassmedium;

        return grasslarge;
    }
}