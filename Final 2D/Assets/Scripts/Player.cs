using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _kickLeft;
    [SerializeField] private GameObject _kickRight;
    [SerializeField] private GameController _gameController;
    // Add member variables to store references to this GameObject's 
    //      Rigidbody, Animator, and SpriteRenderer.
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;

    private bool _facingLeft;
    private bool _isGrounded;

    // ------------------------------------------------------------------------
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        _rb.linearVelocity = new Vector2(
            move * _speed,
            _rb.linearVelocity.y
        );

        if (move != 0)
        {
            _facingLeft = move < 0;
            _spriteRenderer.flipX = _facingLeft;
        }

        bool jumpPressed = Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.UpArrow);

        if (jumpPressed && _isGrounded)
        {
            _isGrounded = false;
            _rb.linearVelocity = new Vector2(
                _rb.linearVelocity.x,
                _jump
            );
        }
        if (_animator != null)
        {
            _animator.SetBool("Walking", move != 0);
            _animator.SetBool("Grounded", _isGrounded);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Kick();
            }
        }
    }

    // ------------------------------------------------------------------------
    private void Kick()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Kick")) return;

        _animator.SetTrigger("Kick");

        // Either activate left or right kick based on character facing
        GameObject attack = _facingLeft ? _kickLeft : _kickRight;
        if (attack != null)
        {
            attack.SetActive(true);
        }
    }

    // ------------------------------------------------------------------------
    // Use a built-in Unity method to detect COLLISION.
    // Use this to avoid allowing the Player to double jump- they should
    //      collide with an object with the Ground tag before being able to
    //      jump again.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    _isGrounded = true;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
