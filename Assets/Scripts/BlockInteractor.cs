using UnityEngine;

public class BlockInteractor : MonoBehaviour
{
    public float reachDistance = 6f;
    public GameObject[] placeablePrefabs;
    private int currentBlockIndex = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック：破壊
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, reachDistance))
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.CompareTag("Block"))
                {
                    Destroy(hitObj);
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) // 右クリック：設置
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, reachDistance))
            {
                Transform hitTransform = hit.collider.transform;

                // 当たったブロックの位置を整数化（1x1x1前提）
                Vector3 hitBlockPos = Vector3Int.RoundToInt(hitTransform.position);

                // 法線方向に1ブロックずらす
                Vector3 placePos = hitBlockPos + hit.normal;

                // 丸めてピッタリにする
                placePos = Vector3Int.RoundToInt(placePos);

                Instantiate(placeablePrefabs[currentBlockIndex], placePos, Quaternion.identity);
            }
        }


        if (Input.GetKeyDown(KeyCode.E)) // ブロック切り替え
        {
            currentBlockIndex = (currentBlockIndex + 1) % placeablePrefabs.Length;
        }
    }
}
