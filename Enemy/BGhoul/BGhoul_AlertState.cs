using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGhoul_AlertState : BGhoulState
{
    [Header("Reference")]

    //public AnimationClip walkAnim;

    [SerializeField] GameObject ghoul;
    [SerializeField] GameObject explodeFX;

    //public Transform pointA;
    //public Transform pointB;    

    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float delay;

    public override void Enter()
    {
        StartCoroutine(Attack());
    }

    public override void Do()
    {
        
    }

    public override void Exit()
    {

    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(delay);
        Instantiate(explodeFX, transform.position, Quaternion.identity);
        Destroy(ghoul);
    }




}
