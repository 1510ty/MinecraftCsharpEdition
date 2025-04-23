using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject chunkPrefab;
    public int renderDistance = 4;
    private Transform player;
    private Dictionary<Vector2Int, Chunk> loadedChunks = new();
    private float seed;

    void Start()
    {
        BlockPrefabRegistry.Initialize();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        seed = Random.Range(0f, 10000f);
    }

    void Update()
    {
        Vector2Int playerChunk = GetPlayerChunk();

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector2Int chunkCoord = playerChunk + new Vector2Int(x, z);
                if (!loadedChunks.ContainsKey(chunkCoord))
                {
                    GameObject obj = Instantiate(chunkPrefab, new Vector3(chunkCoord.x * Chunk.chunkSize, 0, chunkCoord.y * Chunk.chunkSize), Quaternion.identity);
                    Chunk chunk = obj.GetComponent<Chunk>();
                    chunk.Generate(chunkCoord, seed);
                    loadedChunks[chunkCoord] = chunk;
                }
            }
        }
    }

    Vector2Int GetPlayerChunk()
    {
        Vector3 pos = player.position;
        return new Vector2Int(
            Mathf.FloorToInt(pos.x / Chunk.chunkSize),
            Mathf.FloorToInt(pos.z / Chunk.chunkSize)
        );
    }
}