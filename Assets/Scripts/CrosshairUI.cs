using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    void Start()
    {
        GameObject cross = new GameObject("Crosshair");
        cross.transform.SetParent(transform);

        RectTransform rect = cross.AddComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = new Vector2(8, 8);

        Image image = cross.AddComponent<Image>();
        image.color = Color.white;

        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
    }
}
