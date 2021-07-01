using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Variables
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce;
    private int _groundMask;

    private SpriteAnimator _spriteAnimator;
    private float _xAxis;
    private float _yAxis;
    private Rigidbody2D _rb2d;
    bool _faceRight = true;

    private bool _isJumpPressed;
    private bool _isGrounded;


    [SerializeField] private AnimationManager _animationManager;


    //Functions

    //void SpritesFaceMousePosition()
    //{


    //}

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteAnimator = GetComponent<SpriteAnimator>();
        //_animator = GetComponent<Animator>();
        _groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        //Movement left and right 
        _xAxis = Input.GetAxisRaw("Horizontal");

        //Check Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumpPressed = true;

        }
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

        //Jump
        if (_isJumpPressed && _isGrounded)
        {
            _animationManager.Play(AnimationType.Jump);

            _rb2d.AddForce(new Vector2(0, _jumpForce));
            _isJumpPressed = false;
            Debug.Log("jump");
        }

        //assign the new velocity to the rigidbody
        _rb2d.velocity = vel;
    }
}
