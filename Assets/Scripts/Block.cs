using UnityEngine;

public class Block : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject); // ç∂ÉNÉäÉbÉNÇ≈îjâÛ
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 placePos = hit.point + hit.normal / 2f;
                placePos = new Vector3(
                    Mathf.Round(placePos.x),
                    Mathf.Round(placePos.y),
                    Mathf.Round(placePos.z)
                );
                Instantiate(gameObject, placePos, Quaternion.identity);
            }
        }
    }
}
