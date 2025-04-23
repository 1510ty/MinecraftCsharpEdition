using UnityEngine;

public class BlockInteractor : MonoBehaviour
{
    public float reachDistance = 6f;
    public GameObject[] placeablePrefabs;
    private int currentBlockIndex = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���N���b�N�F�j��
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

        if (Input.GetMouseButtonDown(1)) // �E�N���b�N�F�ݒu
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, reachDistance))
            {
                Transform hitTransform = hit.collider.transform;

                // ���������u���b�N�̈ʒu�𐮐����i1x1x1�O��j
                Vector3 hitBlockPos = Vector3Int.RoundToInt(hitTransform.position);

                // �@��������1�u���b�N���炷
                Vector3 placePos = hitBlockPos + hit.normal;

                // �ۂ߂ăs�b�^���ɂ���
                placePos = Vector3Int.RoundToInt(placePos);

                Instantiate(placeablePrefabs[currentBlockIndex], placePos, Quaternion.identity);
            }
        }


        if (Input.GetKeyDown(KeyCode.E)) // �u���b�N�؂�ւ�
        {
            currentBlockIndex = (currentBlockIndex + 1) % placeablePrefabs.Length;
        }
    }
}
