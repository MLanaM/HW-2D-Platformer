using UnityEngine;

public class PlayerMovement : BaseMovement
{
    private const string Jump = nameof(Jump);

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private float _groundCheckRadius = 0.1f;
    private float _jumpForce = 8f;

    private bool _isGrounded;

    protected override void Update()
    {
        _direction = Input.GetAxis(Horizontal);
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        JumpCheck();

        base.Update();

        _animator.SetBool(Jump, !_isGrounded);
    }

    private void JumpCheck()
    {
        if (Input.GetButtonDown(Jump) && _isGrounded)
            _rigidbody.linearVelocityY = _jumpForce;
    }
}