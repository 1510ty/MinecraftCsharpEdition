using UnityEngine;

public class Chunk : MonoBehaviour
{
    public static int chunkSize = 16;
    public static int chunkHeight = 128;

    public BlockType[,,] blocks = new BlockType[chunkSize, chunkHeight, chunkSize];

    public void Generate(Vector2Int coord, float seed)
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                int worldX = coord.x * chunkSize + x;
                int worldZ = coord.y * chunkSize + z;
                float noise = Mathf.PerlinNoise((worldX + seed) * 0.05f, (worldZ + seed) * 0.05f);
                int height = Mathf.FloorToInt(noise * (chunkHeight * 0.5f)) + 20;

                for (int y = 0; y < chunkHeight; y++)
                {
                    if (y > height)
                        blocks[x, y, z] = BlockType.Air;
                    else if (y == height)
                        blocks[x, y, z] = BlockType.Grass;
                    else if (y > height - 3)
                        blocks[x, y, z] = BlockType.Dirt;
                    else
                        blocks[x, y, z] = BlockType.Stone;
                }
            }
        }

        BuildMesh();
    }

    void BuildMesh()
    {
        // このサンプルではブロックの GameObject を生成（最適化は後で）
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    var type = blocks[x, y, z];
                    if (type == BlockType.Air) continue;

                    GameObject prefab = BlockPrefabRegistry.GetPrefab(type);
                    if (prefab != null)
                    {
                        Instantiate(prefab, transform.position + new Vector3(x, y, z), Quaternion.identity, transform);
                    }
                }
            }
        }
    }
}