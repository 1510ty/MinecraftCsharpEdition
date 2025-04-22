using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public GameObject blockPrefab;
    public int chunkSize = 16;

    public void Generate()
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                GameObject block = Instantiate(blockPrefab, transform);
                block.transform.localPosition = new Vector3(x, 0, z);
            }
        }
    }
}
