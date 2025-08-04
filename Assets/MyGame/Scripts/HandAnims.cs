using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static TakebleObject;

public class Hand : MonoBehaviour
{
    public Animator HandAnim;
    public TakebleObject HeldObject;
    public Transform HoldPosition, MakeUpPosition;
    public bool IsReadyToDrag;
    public Vector2 DragOffset;
    public float HandFollowSpeed, HandMoveSpeed;

    private Camera cam;
    public FaceMakeUpManager face;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))//здесь проверка при опускании на лицо
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Face")
                {
                    face = hit.collider.GetComponent<FaceMakeUpManager>();
                    transform.DOMove(MakeUpPosition.position, 0.25f)
                        .SetEase(Ease.InOutQuad)
                        .OnComplete(() =>
                        {
                            HandAnim.SetTrigger(HeldObject.AnimName + "MakeUp");
                            HandAnim.enabled = true;
                        });
                }
            }
        }

        if (IsReadyToDrag && Input.GetMouseButton(0))
        {
            // Draging
            Vector2 dragPos = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.Lerp(transform.position, dragPos + DragOffset, HandFollowSpeed * Time.deltaTime);
        }
    }

    public void TakeObject(TakebleObject TakeObj)
    {
        if (HeldObject == null)
        {
            HeldObject = TakeObj;

            transform.DOMove(HeldObject.HeldObjectPos.position, HandMoveSpeed)
            .OnComplete(() =>
            {
                HeldObject.PickUp(HoldPosition);
                if (HeldObject.MiddlePos != null)
                {
                    transform.DOMove(HeldObject.MiddlePos.position, HandMoveSpeed)
                    .OnComplete(() =>
                    {
                        if (HeldObject.AnimName != string.Empty)
                        {
                            HandAnim.enabled = true;
                            HandAnim.SetTrigger(HeldObject.AnimName);
                        }
                    });
                }
                else
                {
                    GoToFinish();
                }
            });
        }
    }

    public void GoToFinish()
    {
        transform.DOMove(HeldObject.FinishPos.position, HandMoveSpeed)
        .OnComplete(() =>
        {
            IsReadyToDrag = true;
        });
    }

    public void GoToBack()
    {
        transform.DOMove(HeldObject.HeldObjectPos.position, HandMoveSpeed)
        .OnComplete(() =>
        {
            //Invoke("DropObject", 5f);
            DropObject();
        });
    }

    public void MakeUp()
    {
        if(face != null && HeldObject != null)
        {
            face.ApplyMakeUp(HeldObject.makeUpType, HeldObject.VariantIndex);
        }
    }

    public void DropObject()
    {
            transform.DOMove(new Vector2(0, -20), HandMoveSpeed);
        if (HeldObject != null)
        {
            HeldObject.Drop();
            HeldObject = null;
            IsReadyToDrag = false;
            HandAnim.enabled = true;
        }
    }
}
