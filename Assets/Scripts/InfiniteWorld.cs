using System.Collections.Generic;
using UnityEngine;

public class InfiniteWorld : MonoBehaviour
{
    public Transform player;
    public GameObject chunkPrefab;
    public GameObject blockPrefab;

    public int chunkSize = 16;
    public int renderDistance = 2;

    private Dictionary<Vector2Int, GameObject> chunkDict = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int currentChunk;

    void Start()
    {
        currentChunk = GetPlayerChunk();
        UpdateChunks();
    }

    void Update()
    {
        Vector2Int newChunk = GetPlayerChunk();
        if (newChunk != currentChunk)
        {
            currentChunk = newChunk;
            UpdateChunks();
        }
    }

    Vector2Int GetPlayerChunk()
    {
        Vector3 pos = player.position;
        return new Vector2Int(Mathf.FloorToInt(pos.x / chunkSize), Mathf.FloorToInt(pos.z / chunkSize));
    }

    void UpdateChunks()
    {
        HashSet<Vector2Int> neededChunks = new HashSet<Vector2Int>();

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector2Int coord = currentChunk + new Vector2Int(x, z);
                neededChunks.Add(coord);

                if (!chunkDict.ContainsKey(coord))
                {
                    GameObject chunk = new GameObject($"Chunk_{coord.x}_{coord.y}");
                    chunk.transform.position = new Vector3(coord.x * chunkSize, 0, coord.y * chunkSize);
                    chunk.transform.parent = transform;

                    ChunkGenerator gen = chunk.AddComponent<ChunkGenerator>();
                    gen.blockPrefab = blockPrefab;
                    gen.chunkSize = chunkSize;
                    gen.Generate();

                    chunkDict[coord] = chunk;
                }
            }
        }

        // Unload chunks outside range
        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var pair in chunkDict)
        {
            if (!neededChunks.Contains(pair.Key))
            {
                Destroy(pair.Value);
                toRemove.Add(pair.Key);
            }
        }

        foreach (var coord in toRemove)
        {
            chunkDict.Remove(coord);
        }
    }
}
