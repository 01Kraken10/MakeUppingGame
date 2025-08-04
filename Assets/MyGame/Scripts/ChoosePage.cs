using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Timeline;

public class ChoosePage : MonoBehaviour
{

    [System.Serializable]
    public class MarkerUI
    {
        public float Xposition;
        public Sprite MarkerOpenSprites;
        public Sprite MarkerCloseSprites;
        public Image image;
        public RectTransform rectTransform;
    }


    [Header("Markers")]
    public MarkerUI[] Markers;
    public float openY = 50f;
    public float closeY = 40f;
    public Vector2 openSize = new Vector2(110, 143);
    public Vector2 closeSize = new Vector2(110, 123);
    public float animDuration = 0.25f;
    public Ease animEase = Ease.OutQuad;

    int currentIndex = -1;
    [Header("Pages")]
    public RectTransform[] Pages;

    public void SetPage(int pageIndex)
    {
        if (pageIndex == currentIndex) return;

        CloseAll();
        MarkerUI marker = Markers[pageIndex];
        marker.image.sprite = marker.MarkerOpenSprites;
        marker.rectTransform.DOAnchorPos(new Vector2(marker.Xposition, openY), animDuration).SetEase(animEase);
        marker.rectTransform.DOSizeDelta(openSize, animDuration).SetEase(animEase);
        Pages[pageIndex].DOScale(Vector2.one, animDuration).SetEase(animEase);

        currentIndex = pageIndex;
    }

    void CloseAll()
    {
        for(int i = 0; i < Markers.Length; i++)
        {
            MarkerUI marker = Markers[i];
            marker.image.sprite = marker.MarkerCloseSprites;
            marker.rectTransform.DOAnchorPos(new Vector2(marker.Xposition, closeY), animDuration).SetEase(animEase);
            marker.rectTransform.DOSizeDelta(closeSize, animDuration).SetEase(animEase);
            Pages[i].DOScale(Vector2.zero, animDuration).SetEase(animEase);
        }
    }
}
