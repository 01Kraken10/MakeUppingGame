using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Для анимаций через DOTween
using UnityEngine.Timeline;

// Скрипт управления выбором страницы в интерактивной "книге"
public class ChoosePage : MonoBehaviour
{
    // Класс для хранения информации о маркере страницы
    [System.Serializable]
    public class MarkerUI
    {
        public float Xposition; // Позиция маркера по X
        public Sprite MarkerOpenSprites; // Спрайт, когда маркер "активен"
        public Sprite MarkerCloseSprites; // Спрайт, когда маркер "неактивен"
        public Image image; // Компонент Image маркера
        public RectTransform rectTransform; // RectTransform маркера (для анимации)
    }

    [Header("Markers")]
    public MarkerUI[] Markers; // Массив маркеров для всех страниц
    public float openY = 50f; // Позиция маркера по Y в открытом состоянии
    public float closeY = 40f; // Позиция маркера по Y в закрытом состоянии
    public Vector2 openSize = new Vector2(110, 143); // Размер маркера при открытии
    public Vector2 closeSize = new Vector2(110, 123); // Размер маркера при закрытии
    public float animDuration = 0.25f; // Длительность анимации
    public Ease animEase = Ease.OutQuad; // Тип сглаживания для анимации

    int currentIndex = -1; // Индекс текущей выбранной страницы (-1 = ничего не выбрано)

    [Header("Pages")]
    public RectTransform[] Pages; // Массив RectTransform для каждой страницы

    // Метод выбора страницы по индексу
    public void SetPage(int pageIndex)
    {
        if (pageIndex == currentIndex) return; // Если уже выбрана — ничего не делаем

        CloseAll(); // Закрываем все страницы и сбрасываем маркеры

        // Открываем новую страницу
        MarkerUI marker = Markers[pageIndex];
        marker.image.sprite = marker.MarkerOpenSprites; // Ставим активный спрайт

        // Анимируем перемещение и размер маркера
        marker.rectTransform.DOAnchorPos(new Vector2(marker.Xposition, openY), animDuration).SetEase(animEase);
        marker.rectTransform.DOSizeDelta(openSize, animDuration).SetEase(animEase);

        // Анимируем открытие страницы (масштабируем до 1)
        Pages[pageIndex].DOScale(Vector2.one, animDuration).SetEase(animEase);

        currentIndex = pageIndex; // Обновляем текущий индекс
    }

    // Метод закрытия всех страниц и возврата маркеров в неактивное состояние
    void CloseAll()
    {
        for (int i = 0; i < Markers.Length; i++)
        {
            MarkerUI marker = Markers[i];
            marker.image.sprite = marker.MarkerCloseSprites; // Ставим закрытый спрайт

            // Анимируем возврат в исходную позицию и размер
            marker.rectTransform.DOAnchorPos(new Vector2(marker.Xposition, closeY), animDuration).SetEase(animEase);
            marker.rectTransform.DOSizeDelta(closeSize, animDuration).SetEase(animEase);

            // Прячем страницу (масштабируем до 0)
            Pages[i].DOScale(Vector2.zero, animDuration).SetEase(animEase);
        }
    }
}
