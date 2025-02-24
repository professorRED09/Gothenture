using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingEyeState : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;

    protected Rigidbody2D body;
    protected Animator animator;

    protected FlyingEye flyingE;
    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void Setup(Rigidbody2D _body, Animator _animator, FlyingEye _flyingE)
    {
        body = _body;
        animator = _animator;
        flyingE = _flyingE;
    }
}
