using System.Collections;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharactorAnimation charactorAnimation;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D bodyCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;

    [Header("Slide")]
    [SerializeField] private float slideDuration = 0.8f;
    [SerializeField] private float slideHeightMultiplier = 0.5f;

    [Header("Damage")]
    [SerializeField] private float invincibleDuration = 1.5f;

    private bool isGrounded;
    private bool isSliding;
    private bool isInvincible;
    private int groundContactCount;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (bodyCollider == null)
            bodyCollider = GetComponent<CapsuleCollider2D>();

        if (charactorAnimation == null)
            charactorAnimation = GetComponent<CharactorAnimation>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (bodyCollider != null)
        {
            originalColliderSize = bodyCollider.size;
            originalColliderOffset = bodyCollider.offset;
        }

        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, 0f);
    }

    public void Jump()
    {
        if (!isGrounded || isSliding || rb == null)
            return;

        Vector2 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        charactorAnimation.Jump();
    }

    public void Slide()
    {
        if (!isGrounded || isSliding)
            return;

        StartCoroutine(SlideRoutine());
    }

    private IEnumerator SlideRoutine()
    {
        isSliding = true;
        charactorAnimation.Slide();

        if (bodyCollider != null)
        {
            float newHeight = originalColliderSize.y * slideHeightMultiplier;
            float heightDiff = originalColliderSize.y - newHeight;

            bodyCollider.size = new Vector2(originalColliderSize.x, newHeight);
            bodyCollider.offset = new Vector2(
                originalColliderOffset.x,
                originalColliderOffset.y - heightDiff * 0.5f
            );
        }

        yield return new WaitForSeconds(slideDuration);

        if (bodyCollider != null)
        {
            bodyCollider.size = originalColliderSize;
            bodyCollider.offset = originalColliderOffset;
        }

        isSliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContactCount++;
            isGrounded = groundContactCount > 0;
            return;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isInvincible)
            {
                TakeDamage(1);
            }

            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
            return;

        groundContactCount--;

        if (groundContactCount < 0)
            groundContactCount = 0;

        isGrounded = groundContactCount > 0;
    }

    private void TakeDamage(int damage)
    {
        if (isInvincible)
            return;

        StateManager.Instance.AddHitCount(1);
        StateManager.Instance.MinusHP(damage);
        Debug.Log("피격 / 현재 HP: " + StateManager.Instance.HP);

        if (StateManager.Instance.HP <= 0)
        {
            Debug.Log("게임 오버");
        }

        StartCoroutine(InvincibleRoutine());
    }

    private IEnumerator InvincibleRoutine()
    {
        isInvincible = true;

        float elapsed = 0f;

        while (elapsed < invincibleDuration)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(0.12f);
            elapsed += 0.12f;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        isInvincible = false;
    }
}