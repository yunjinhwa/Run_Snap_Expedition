using System.Collections;
using UnityEngine;

public class CharactorAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpUpDuration = 0.35f;
    [SerializeField] private float jumpFallDuration = 0.35f;
    [SerializeField] private float slideDuration = 1f;

    private Coroutine currentRoutine;

    public void Jump()
    {
        StartAnimationRoutine(JumpRoutine());
    }

    public void Slide()
    {
        StartAnimationRoutine(SlideRoutine());
    }

    private void StartAnimationRoutine(IEnumerator routine)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(routine);
    }

    private IEnumerator JumpRoutine()
    {
        animator.Play("Jump_Up");
        yield return new WaitForSeconds(jumpUpDuration);

        animator.Play("Jump_Fall");
        yield return new WaitForSeconds(jumpFallDuration);

        animator.Play("Run");
        currentRoutine = null;
    }

    private IEnumerator SlideRoutine()
    {
        animator.Play("Slide");
        yield return new WaitForSeconds(slideDuration);

        animator.Play("Run");
        currentRoutine = null;
    }
}