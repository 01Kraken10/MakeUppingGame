using UnityEngine;

public class HandSpriteEvents : MonoBehaviour
{
    public void OnMakeupAnimationFinished()
    {
        GetComponentInParent<Hand>()?.GoToFinish();
    }
    public void MakeupAnimation()
    {
        GetComponentInParent<Hand>()?.MakeUp();
    }
    public void GoBackAnimation()
    {
        GetComponentInParent<Hand>()?.GoToBack();
    }
}