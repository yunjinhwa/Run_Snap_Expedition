using UnityEngine;

public class CharactorController : MonoBehaviour
{
    [SerializeField] private CharactorAnimation charactorAnimation;

    public void Jump()
    {
        charactorAnimation.Jump();

        // 여기서 실제 점프 물리 처리도 같이 가능
        // 예: Rigidbody 힘 주기
    }
}