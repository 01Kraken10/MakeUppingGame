using UnityEngine;

public class TakebleObject : MonoBehaviour
{
    public string AnimName;
    public Following following;

    public Transform HeldObjectPos, MiddlePos, FinishPos;

    public enum MakeUpType
    {
        Cream,
        Blush,
        Eyeshadow,
        Lipstick,
        Cleanser
    }

    public MakeUpType makeUpType;
    public int VariantIndex;

    public void PickUp(Transform holdPoint)
    {
        following.HoldPosition = holdPoint;
        following.IsHeld = true;
    }

    public void Drop()
    {
        following.IsHeld = false;
    }
}
