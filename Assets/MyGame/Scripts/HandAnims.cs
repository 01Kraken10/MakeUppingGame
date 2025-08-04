using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static TakebleObject;


// скрипт для управления рукой (передвижение, анимации, хватаниее объектов и т.п.)
public class Hand : MonoBehaviour
{
    public Animator HandAnim; // Аниматор руки
    public TakebleObject HeldObject; // Объект, который в данный момент держится
    public Transform HoldPosition, MakeUpPosition; // Позиции удержания и макияжа
    public bool IsReadyToDrag; // Флаг, можно ли сейчас двигать рукой
    public Vector2 DragOffset; // Смещение от курсора при перетаскивании
    public float HandFollowSpeed, HandMoveSpeed; // Скорость следования и скорость движения при анимациях

    private Camera cam; // Камера сцены
    public FaceMakeUpManager face; // Ссылка на лицо, куда наносится макияж

    private void Start()
    {
        cam = Camera.main; // Получаем основную камеру
    }

    private void Update()
    {
        // Если отпущен палец от экрана — проверка, был ли клик по лицу
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.tag == "Face")
            {
                face = hit.collider.GetComponent<FaceMakeUpManager>();

                // Двигаем руку в позицию макияжа, а затем проигрываем анимацию
                transform.DOMove(MakeUpPosition.position, 0.25f)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        HandAnim.SetTrigger(HeldObject.AnimName + "MakeUp");
                        HandAnim.enabled = true;
                    });
            }
        }

        // Если рука готова к перетаскиванию и палец на экране — двигаем руку за курсором
        if (IsReadyToDrag && Input.GetMouseButton(0))
        {
            Vector2 dragPos = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.Lerp(transform.position, dragPos + DragOffset, HandFollowSpeed * Time.deltaTime);
        }
    }

    // Метод для поднятия предмета (вызывается при нажатии на объекты с которыми можно взаимодействовоать)
    public void TakeObject(TakebleObject TakeObj) 
    {
        if (HeldObject == null) // если ничего не держим
        {
            HeldObject = TakeObj;

            // Перемещаем руку к позиции, где лежит объект
            transform.DOMove(HeldObject.HeldObjectPos.position, HandMoveSpeed)
            .OnComplete(() =>
            {
                // Объект "поднимается" (меняет родителя и т.п.)
                HeldObject.PickUp(HoldPosition);

                // Если задана промежуточная позиция — двигаемся к ней (промещуточными позициями считаются например позиция цветов теней)
                if (HeldObject.MiddlePos != null)
                {
                    transform.DOMove(HeldObject.MiddlePos.position, HandMoveSpeed)
                    .OnComplete(() =>
                    {
                        // Если задано имя анимации — проигрываем её
                        if (!string.IsNullOrEmpty(HeldObject.AnimName))
                        {
                            HandAnim.enabled = true;
                            HandAnim.SetTrigger(HeldObject.AnimName);
                        }
                    });
                }
                else
                {
                    // Если нет промежуточной точки — идём сразу к финальной (финальная точка - точка на середине экрана)
                    GoToFinish();
                }
            });
        }
    }

    // Двигаем руку в финальную позицию объекта
    public void GoToFinish()
    {
        transform.DOMove(HeldObject.FinishPos.position, HandMoveSpeed)
        .OnComplete(() =>
        {
            IsReadyToDrag = false; // 
        });
    }

    // Возвращаемся обратно к исходной позиции (вызывается в анимации)
    public void GoToBack()
    {
        transform.DOMove(HeldObject.HeldObjectPos.position, HandMoveSpeed)
        .OnComplete(() =>
        {
            DropObject(); // Сразу сбрасываем объект
        });
    }

    // Применение макияжа (отправка данных в FaceMakeUpManager) (вызывается в анимации)
    public void MakeUp()
    {
        if(face != null && HeldObject != null)
        {
            face.ApplyMakeUp(HeldObject.makeUpType, HeldObject.VariantIndex);
        }
    }

    // Сброс объекта и перемещение руки вниз, за экран (вызывается в анимации)
    public void DropObject()
    {
        transform.DOMove(new Vector2(0, -20), HandMoveSpeed); // Уводим руку вниз

        if (HeldObject != null)
        {
            HeldObject.Drop(); // Отпускаем объект
            HeldObject = null;
            IsReadyToDrag = false;
            HandAnim.enabled = true;
        }
    }
}
