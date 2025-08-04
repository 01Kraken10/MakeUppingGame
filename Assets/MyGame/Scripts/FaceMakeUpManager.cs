using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static TakebleObject;

public class FaceMakeUpManager : MonoBehaviour
{
    public SpriteRenderer[] BlushVariants;
    public SpriteRenderer[] EyeshadowVariants;
    public SpriteRenderer[] LipstickVariants;
    public SpriteRenderer Acne;

    public void ApplyMakeUp(MakeUpType type, int index)
    {
        switch (type)
        {
            case MakeUpType.Cream:
                Acne.DOFade(0, 0.75f);
                break;
            case MakeUpType.Cleanser:
                ResetMakeUp();
                break;
            case MakeUpType.Blush:
                EnableOnly(BlushVariants, index);
                break;

            case MakeUpType.Eyeshadow:
                EnableOnly(EyeshadowVariants, index);
                break;

            case MakeUpType.Lipstick:
                EnableOnly(LipstickVariants, index);
                break;
        }
    }

    public void ResetMakeUp()
    {
        DisableObjects(BlushVariants);
        DisableObjects(LipstickVariants);
        DisableObjects(EyeshadowVariants);
        Acne.DOFade(1, 0.75f);
    }

    void EnableOnly(SpriteRenderer[] group, int index)
    {
        for (int i = 0; i < group.Length; i++)
            if(i == index)
            {
                group[i].DOFade(1, 0.75f);
                //group[i].SetActive(i == index);
            }
            
    }

    void DisableObjects(SpriteRenderer[] group)
    {
        foreach (SpriteRenderer obj in group)
        {
            obj.DOFade(0, 0.75f);
        }
    }
}
