using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Avatar").GetComponent<Animator>();
        
        EventSystem.instance.PlayerOnMove += PlayerMoveHandler;
        EventSystem.instance.PlayerOnJump += PlayerJumpHandler;
        EventSystem.instance.PlayerOnAttack += PlayerAttackHandler;
        EventSystem.instance.PlayerOnHurt += PlayerHurtHandler;
        EventSystem.instance.PlayerOnDead += PlayerDeadHandler;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        EventSystem.instance.PlayerOnMove -= PlayerMoveHandler;
        EventSystem.instance.PlayerOnJump -= PlayerJumpHandler;
        EventSystem.instance.PlayerOnAttack -= PlayerAttackHandler;
        EventSystem.instance.PlayerOnHurt -= PlayerHurtHandler;
        EventSystem.instance.PlayerOnDead -= PlayerDeadHandler;
    }

    private void OnDestroy()
    {
        
    }

    private void PlayerDeadHandler()
    {
        anim?.SetTrigger("isDead");
    }

    private void PlayerHurtHandler()
    {
        anim?.SetTrigger("isHurting");
    }

    private void PlayerAttackHandler()
    {
        anim?.SetTrigger("isAttacking");
    }

    private void PlayerJumpHandler(bool isJump)
    {
        anim?.SetBool("isJump", isJump);
    }

    private void PlayerMoveHandler(bool isRunning)
    {
        anim?.SetBool("isRunning", isRunning);
    }

}
