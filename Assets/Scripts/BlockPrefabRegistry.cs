using UnityEngine;
using System.Collections.Generic;

public static class BlockPrefabRegistry
{
    static Dictionary<BlockType, GameObject> prefabMap;

    public static void Initialize()
    {
        prefabMap = new Dictionary<BlockType, GameObject>
        {
            { BlockType.Grass, Resources.Load<GameObject>("GrassBlock") },
            { BlockType.Dirt, Resources.Load<GameObject>("DirtBlock") },
            { BlockType.Stone, Resources.Load<GameObject>("StoneBlock") }
        };
    }

    public static GameObject GetPrefab(BlockType type)
    {
        prefabMap ??= new();
        return prefabMap.TryGetValue(type, out var prefab) ? prefab : null;
    }
}
