using UnityEngine;

public class Following : MonoBehaviour
{
    public bool IsHeld;
    public Transform HoldPosition;
    public float FollowSpeed = 5f;
    private void Update()
    {
        if (IsHeld)
        {
            transform.position = Vector3.Lerp(transform.position, HoldPosition.position, FollowSpeed * Time.deltaTime);
        }
    }
}
