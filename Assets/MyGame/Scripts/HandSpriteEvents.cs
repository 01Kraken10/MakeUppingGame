using UnityEngine;

// Этот скрипт используется для вызова методов из анимации руки.
// Он висит на дочернем объекте (спрайте), на котором находится Animator.
// Поскольку Animator мешает перемещению объекта через DOTween,
// его вынесли на дочерний объект, а основной логикой управляет родительский объект с компонентом Hand.
// Методы этого скрипта вызываются как Animation Events внутри анимации.
public class HandSpriteEvents : MonoBehaviour
{
    // Метод вызывается в конце промежуточной анимации анимации (нанесение косметики на кисточку)
    public void OnMakeupAnimationFinished()
    {
        GetComponentInParent<Hand>()?.GoToFinish();
    }

    // Метод вызывается в момент нанесения макияжа
    public void MakeupAnimation()
    {
        GetComponentInParent<Hand>()?.MakeUp();
    }

    // Метод вызывается, когда нужно вернуть руку назад
    public void GoBackAnimation()
    {
        GetComponentInParent<Hand>()?.GoToBack();
    }
}
