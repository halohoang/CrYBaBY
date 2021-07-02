﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimator : MonoBehaviour
{
    // ------ Variables ------ //

    [SerializeField] SpriteAnimation startingAnimation;

    [SerializeField] new SpriteRenderer renderer;

    public event Action<Sprite> OnSpriteChange;

    BasicSpriteAnimator basicSA = new BasicSpriteAnimator();

    public SpriteAnimation Animation { get => basicSA.Animation; }
    public SpriteRenderer Renderer { get => renderer; }

    public SpriteAnimation OrginalAnimation { get => basicSA.OriginalAnimation; }


    // ------ Functions ------ //
    private void Start()
    {

        if (renderer == null)
        {
            renderer = GetComponentInChildren<SpriteRenderer>();
        }

        if(startingAnimation != null)
        {
            Play(startingAnimation);
        }
    }

    public void Play(SpriteAnimation ani, bool resetSame = true)
    {
        basicSA.Play(ani, resetSame);
    }

    public bool IsDone()
    {
        return basicSA.IsDone();
    }

    private void Update()
    { 
        if (renderer == null)
            return;

        Sprite before = renderer.sprite;
        var sprite = basicSA.Update(Time.deltaTime);

        if (sprite != before)
            OnSpriteChange?.Invoke(sprite);

        if (sprite != null)
        renderer.sprite = sprite;
    }
}

public class BasicSpriteAnimator
{

    private SpriteAnimation animation;
    private SpriteAnimation original;

    public SpriteAnimation Animation { get => animation; }
    public SpriteAnimation OriginalAnimation { get => original; }

    public void Play(SpriteAnimation ani, bool resetSame = true)
    {
        if(ani == null)
        {
            animation = ani;
            return;
        }
        else if (animation != null && !resetSame && ani == original)
        {
            return;
        }

        original = ani;
        animation = MonoBehaviour.Instantiate(ani); //Safe
    }

    public bool IsDone()
    {
        return animation == null;
    }

    public Sprite Update(float deltaTime)
    { 
        if (animation == null)
            return null;

        var fr = animation.Next(deltaTime);

        if (animation.IsDone())
        {
            animation = null;
            original = null;
        }

        return fr;
    }
}
