using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static TakebleObject; // Подключение enum MakeUpType из другого скрипта

public class FaceMakeUpManager : MonoBehaviour
{
    // Ссылки на возможные варианты макияжа для разных зон лица
    public SpriteRenderer[] BlushVariants;     // Румяна
    public SpriteRenderer[] EyeshadowVariants; // Тени
    public SpriteRenderer[] LipstickVariants;  // Помада
    public SpriteRenderer Acne;                // Прыщи, отображаются отдельным спрайтом

    // Применение макияжа определённого типа по индексу
    public void ApplyMakeUp(MakeUpType type, int index)
    {
        switch (type)
        {
            case MakeUpType.Cream:
                // Крем убирает прыщи (анимация исчезновения)
                Acne.DOFade(0, 0.75f);
                break;

            case MakeUpType.Cleanser:
                // Очищающее средство сбрасывает весь макияж
                ResetMakeUp();
                break;

            case MakeUpType.Blush:
                // Включаем нужный вариант румян
                EnableOnly(BlushVariants, index);
                break;

            case MakeUpType.Eyeshadow:
                // Включаем нужный вариант теней
                EnableOnly(EyeshadowVariants, index);
                break;

            case MakeUpType.Lipstick:
                // Включаем нужный вариант помады
                EnableOnly(LipstickVariants, index);
                break;
        }
    }

    // Сброс всего макияжа
    public void ResetMakeUp()
    {
        DisableObjects(BlushVariants);
        DisableObjects(LipstickVariants);
        DisableObjects(EyeshadowVariants);

        // Прыщи снова становятся видимыми
        Acne.DOFade(1, 0.75f);
    }

    // Активирует только один спрайт в группе, остальные оставляет как есть
    void EnableOnly(SpriteRenderer[] group, int index)
    {
        for (int i = 0; i < group.Length; i++)
        {
            if (i == index)
            {
                group[i].DOFade(1, 0.75f); // Плавное появление выбранного варианта
            }
        }
    }

    // Скрывает все спрайты в группе
    void DisableObjects(SpriteRenderer[] group)
    {
        foreach (SpriteRenderer obj in group)
        {
            obj.DOFade(0, 0.75f); // Плавное исчезновение
        }
    }
}
