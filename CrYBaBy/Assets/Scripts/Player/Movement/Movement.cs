using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // ------ Variables ------ //

    //Movement
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce;
    private int _amountJumpedLeft;
    private bool _canJump;

    ////Dash abilities
    //bool _isDashing;
    //[SerializeField] private float _dashTime;
    //[SerializeField] private float _dashSpeed;
    //[SerializeField] private float _distanceBetweenImages;
    //[SerializeField] private float _dashCoolDown; //Dash Cool down
    //private float _dashTimeLeft;
    //private float _lastImageXPosition;
    //private float _lastDash = -100f; //LastTime you dash

    //Landed or not
    private int _groundMask;
    private bool _isJumpPressed;
    private bool _isGrounded;

    //Animation
    private SpriteAnimator _spriteAnimator;
    private float _xAxis;
    private float _yAxis;
    private Rigidbody2D _rb2d;
    bool _faceRight = true;



    [SerializeField] private AnimationManager _animationManager;


    // ------ Functions ------ //


    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteAnimator = GetComponent<SpriteAnimator>();
        //_animator = GetComponent<Animator>();
        _groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    //CheckDash
    //private void CheckDash()
    //{
    //    if (_isDashing)
    //    {
    //        if (_dashTimeLeft > 0)
    //        {
    //            _rb2d.velocity = new Vector2(_dashSpeed, _rb2d.velocity.y);
    //            _dashTimeLeft -= Time.deltaTime;
    //            if (Mathf.Abs(transform.position.x - _lastImageXPosition) > _distanceBetweenImages)
    //            {
    //                PlayerAfterImagePool.Instance.GetFromPool();
    //                _lastImageXPosition = transform.position.x;
    //            }
    //        }
    //        if (_dashTimeLeft <= 0)
    //        {
    //            _isDashing = false;

    //        }
    //    }
    //}
    //Dash Function
    //private void AttemptDash()
    //{
    //    _isDashing = true;
    //    _dashTimeLeft = _dashTime;
    //    _lastDash = Time.time;

    //    PlayerAfterImagePool.Instance.GetFromPool();
    //    _lastImageXPosition = transform.position.x;
    //}

    void Update()
    {
        //Movement left and right 
        _xAxis = Input.GetAxisRaw("Horizontal");

        //Jump
        if (Input.GetKeyDown(KeyCode.Space)) CheckJump();
        
        //Check Left and Right
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

        //Dash
        //CheckDash();
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    Debug.Log("Dash");
        //    if (Time.time >= (_lastDash + _dashCoolDown))
        //    {
        //        AttemptDash();
        //    }
        //}

    }

    private void FixedUpdate() //Physic movement
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

        Vector2 vel = new Vector2(0, _rb2d.velocity.y);

        //Move horizontal

        if (_xAxis < 0) //Flip the sprites reverse
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
    void Jump()
    {
        _animationManager.Play(AnimationType.Jump);

        _rb2d.AddForce(new Vector2(0, _jumpForce));
    }

    void CheckJump()
    {
        if (_isGrounded == true) Jump();
    }

}
