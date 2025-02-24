using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BGhoulState : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected BGhoul bGhoul;
    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void Setup(Rigidbody2D _rb, Animator _anim, BGhoul _bGhoul)
    {
        rb = _rb;
        anim = _anim;
        bGhoul = _bGhoul;
    }

    public void Initialise()
    {
        isComplete = false;
        startTime = Time.time;
    }
}
