using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{

    public static EventSystem instance; //singletoon

    /** Animation Action for IdleState, RunState, JumpState, AttackState, HurtState, dan DieState */
    public Action<bool> PlayerOnMove;
    public Action<bool> PlayerOnJump;
    public Action PlayerOnAttack;
    public Action PlayerOnHurt;
    public Action PlayerOnDead;

    //Player
    public Action<int> PlayerHealthStatus;

    private void Awake()
    {
        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

}
