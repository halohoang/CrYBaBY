using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //variables
    private SpriteAnimator _spriteAnimator;
    private bool _isAttackedPressed;
    private bool isAttacking;

    [SerializeField] private AnimationManager _animationManager;


    //functions
    private void Start()
    {
        _spriteAnimator = GetComponent<SpriteAnimator>();
    }
    private void Update()
    {
        //Check Attack
        if (Input.GetMouseButtonDown(0))
        {
            //PlayAnimation(_anMelee);
            _isAttackedPressed = true;
            _animationManager.Play(AnimationType.Melee);
            
        }
    }

}

