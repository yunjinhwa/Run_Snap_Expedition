using System.Collections;
using UnityEngine;

public class CharactorAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

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
        yield return new WaitForSeconds(1f);

        animator.Play("Jump_Fall");
        yield return new WaitForSeconds(1f);

        animator.Play("Run");
        currentRoutine = null;
    }

    private IEnumerator SlideRoutine()
    {
        animator.Play("Slide");
        yield return new WaitForSeconds(1f);

        animator.Play("Run");
        currentRoutine = null;
    }
}