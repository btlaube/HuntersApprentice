using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerComponents playerComponents;

    public void SetSpeed(float speed)
    {
        playerComponents.animator.SetFloat("Speed", Mathf.Abs(speed));
    }
}