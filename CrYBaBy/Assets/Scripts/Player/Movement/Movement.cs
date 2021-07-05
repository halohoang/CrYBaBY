using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // ------ Variables ------ //

    //Movement
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce;


    //Animation
    private SpriteAnimator _spriteAnimator;
    private float _xAxis;
    private float _yAxis;
    private Rigidbody2D _rb2d;
    bool _faceRight = true;
    [SerializeField] private AnimationManager _animationManager;

    //GroundCheck
    private int _groundMask;
    private bool _isGrounded;

    //WallCheck
    public LayerMask WhatIsWall;
    private bool _isTouchingFront;
    public Transform FrontCheck;
    private bool _wallSliding;
    public float CheckRadius;
    [SerializeField] private float _wallSlidingSpeed;



    // ------ Functions ------ //


    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteAnimator = GetComponent<SpriteAnimator>();
        _groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    //CheckWall
    void WallCheck()
    {
        _isTouchingFront = Physics2D.OverlapCircle(FrontCheck.position, CheckRadius, WhatIsWall);
    }
    void WallSlide()
    {
        if (_isTouchingFront == true && _isGrounded == false && _xAxis != 0)
        {
            _wallSliding = true;
        }
        else { _wallSliding = false; }

        if (_wallSliding == true)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, Mathf.Clamp(_rb2d.velocity.y, _wallSlidingSpeed, float.MaxValue));
        }
    }

    void FaceMousePosition()
    {
        var delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (delta.x >= 0 && !_faceRight) // Face right
        {
            transform.localScale = new Vector3(5, 5, 5);
            _faceRight = true;
        }
        if (delta.x < 0 && _faceRight) // Face left
        {
            transform.localScale = new Vector3(-5, 5, 5);
            _faceRight = false;
        }
    }
    void Update()
    {
        //Movement left and right 
        _xAxis = Input.GetAxisRaw("Horizontal");
        //Jump
        if (Input.GetKeyDown(KeyCode.Space)) CheckJump();

        //Check Left and Right
        FaceMousePosition();
        MoveHorizontal();

    }


    void GroundCheck()
    {
        //Ground Check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, _groundMask);
        if (hit.collider != null)
        {
            _isGrounded = true;

        }
        else
        {
            _isGrounded = false;
        }
    }
    void MoveHorizontal()
    {
        //Move horizontal
        Vector2 vel = new Vector2(0, _rb2d.velocity.y);
        if (_xAxis < 0)
        {
            vel.x = -_moveSpeed;
            _animationManager.Play(AnimationType.Run);
        }
        else if (_xAxis > 0)
        {
            vel.x = _moveSpeed;
            _animationManager.Play(AnimationType.Run);


        }
        else
        {
            vel.x = 0;
            _animationManager.Play(AnimationType.Idle);
        }
        //assign the new velocity to the rigidbody
        _rb2d.velocity = vel;
    }
    void CheckJump()
    {
        if (_isGrounded == true) Jump();
    }
    void Jump()
    {
        _animationManager.Play(AnimationType.Jump);

        _rb2d.AddForce(new Vector2(0, _jumpForce));
    }

    private void FixedUpdate() //Physic movement
    {
        GroundCheck();
    }
}
