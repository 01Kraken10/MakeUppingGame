using UnityEngine;

public class ShelfScaling : MonoBehaviour
{
    public RectTransform MainCanvas;
    private void Start()
    {
        RectTransform MyTransform = GetComponent<RectTransform>();
        MyTransform.localScale = new Vector2(MainCanvas.sizeDelta.x / 1080, MainCanvas.sizeDelta.x / 1080);
        MyTransform.anchoredPosition = new Vector2(0, 200 * MainCanvas.sizeDelta.y / 1920);
    }
}
