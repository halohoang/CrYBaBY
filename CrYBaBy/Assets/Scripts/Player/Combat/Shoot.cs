using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //variables
    private SpriteAnimator _spriteAnimator;
    private bool _isAttackedPressed;
    private bool isAttacking;
    [SerializeField] private SpriteAnimation _anMelee;

    //functions
    private void Start()
    {
        _spriteAnimator = GetComponent<SpriteAnimator>();
    }
    private void Update()
    {
        //Check Attack
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayAnimation(_anMelee);
            _isAttackedPressed = true;
        }
    }
    private void PlayAnimation(SpriteAnimation spriteAnimation, bool resetsame = false)
    {
        _spriteAnimator.Play(spriteAnimation, resetsame);

    }
}

