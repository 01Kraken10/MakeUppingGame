using UnityEngine;

/// Класс представляет объект, который можно взять рукой (например, косметику).
/// Содержит информацию для анимации, позиционирования и взаимодействия с макияжем.
public class TakebleObject : MonoBehaviour
{
    // Название анимации, связанной с этим объектом
    public string AnimName;

    // Ссылка на скрипт Following — отвечает за движение к руке
    public Following following;

    // Позиции, через которые рука должна провести объект
    public Transform HeldObjectPos;  // Начальная точка, где объект берётся
    public Transform MiddlePos;      // Промежуточная точка (может быть null)
    public Transform FinishPos;      // Конечная точка (точка в центре экрана)

    // Тип макияжа, который наносится этим объектом
    public enum MakeUpType
    {
        Cream,      // Удаляет прыщи
        Blush,      // Румяна
        Eyeshadow,  // Тени
        Lipstick,   // Помада
        Cleanser    // Смывает макияж
    }

    public MakeUpType makeUpType;

    // Индекс используемого варианта (например, цвет)
    public int VariantIndex;

    /// Берёт объект в руку — включает следование за рукой.
    public void PickUp(Transform holdPoint)
    {
        following.HoldPosition = holdPoint;
        following.IsHeld = true;
    }


    /// Отпускает объект — отключает следование.
    public void Drop()
    {
        following.IsHeld = false;
    }
}
