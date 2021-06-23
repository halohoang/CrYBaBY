using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private SpriteAnimator _spriteAnimator;
    void Start()
    {
        _spriteAnimator = GetComponent<SpriteAnimator>();
    }



    [SerializeField] private SpriteAnimation _anIdle;
    [SerializeField] private SpriteAnimation _anRun;
    [SerializeField] private SpriteAnimation _anJump;
    [SerializeField] private SpriteAnimation _anMelee;


    public void Play(AnimationType animationType)
    {
        if (_spriteAnimator.OrginalAnimation == _anMelee)
        {   
            return;
        }
        if (animationType == AnimationType.Idle)
        {
            _spriteAnimator.Play(_anIdle, false);

        }
        if (animationType == AnimationType.Run)
        {
            _spriteAnimator.Play(_anRun, false);

        }
        if (animationType == AnimationType.Jump)
        {
            _spriteAnimator.Play(_anJump, false);

        }
        if (animationType == AnimationType.Melee)
        {
            _spriteAnimator.Play(_anMelee, false);

        }
    }
}
public enum AnimationType
{
    Idle,
    Run,
    Jump,
    Melee
}
