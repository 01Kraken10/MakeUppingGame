using UnityEngine;

public class Following : MonoBehaviour // этот скрипт навешен на все объекты которые будут следовать за рукой (помады, кисточки, крем и т.п.)
{
    // Флаг, указывает держится ли объект рукой
    public bool IsHeld;

    // Позиция, за которой должен следовать объект
    public Transform HoldPosition;

    // Скорость следования
    public float FollowSpeed = 5f;

    private void Update()
    {
        // Если объект удерживается — он будет плавно перемещаться к целевой позиции
        if (IsHeld)
        {
            // Lerp плавно двигает объект к нужной позиции
            transform.position = Vector3.Lerp(transform.position, HoldPosition.position, FollowSpeed * Time.deltaTime);
        }
    }
}
