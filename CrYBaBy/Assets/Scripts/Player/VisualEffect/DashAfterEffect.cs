using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAfterEffect : MonoBehaviour
{
    // ------ Variables ------ //

    //After Image active time
    [SerializeField]
    private float _activeTime = 0.1f;
    private float _timeActivated;

    private Transform _player;

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _playerSpriteRenderer;

    //After Image Color
    private Color _color;
    private float _alpha;
    [SerializeField]
    private float _alphaSet = 0.8f;
    private float _alphaMutiplier = 0.85f; 

    // ------ Functions ------ //
    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();

        _alpha = _alphaSet;
        _spriteRenderer.sprite = _playerSpriteRenderer.sprite;
        transform.position = _player.position;
        transform.rotation = _player.rotation;
        _timeActivated = Time.time;
    }

    private void Update()
    {
        _alpha *= _alphaMutiplier;
        _color = new Color(1f,1f,1f,_alpha);
        _spriteRenderer.color = _color;

        if (Time.time >= (_timeActivated + _activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
