using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    // Animator hashes
    private readonly int moveX = Animator.StringToHash("MoveX");

    private readonly int moveY = Animator.StringToHash("MoveY");
    private readonly int moving = Animator.StringToHash("Moving");
    private readonly int dead = Animator.StringToHash("Dead");
    private readonly int revive = Animator.StringToHash("Revive");
    private readonly int attacking = Animator.StringToHash("Attacking");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDeadAnimation()
    {
        animator.SetTrigger(dead);
    }

    public void SetMoveBoolTransition(bool value)
    {
        animator.SetBool(moving, value);
    }

    public void SetMoveAnimation(Vector2 moveDirection)
    {
        animator.SetFloat(moveX, moveDirection.x);
        animator.SetFloat(moveY, moveDirection.y);
    }
    public void SetAttackingAnimation(bool isAttacking)
    {
        animator.SetBool(attacking, isAttacking);
    }

    public void ResetPlayerAnimation()
    {
        SetMoveAnimation(Vector2.down);
        animator.SetTrigger(revive);
    }
}