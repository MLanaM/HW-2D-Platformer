using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private float _jumpForce = 8f;
    private bool _isJumping = false;

    public void JumpCheck(bool isTryingToJump)
    {
        bool isGrounded = _groundCheck.IsGrounded();

        if (_isJumping && isGrounded && _rigidbody.linearVelocityY <= 0.1f)
        {
            _isJumping = false;
            _animator.SetBool(InputConstans.Jump, _isJumping);
        }

        if (isTryingToJump && isGrounded)
        {
            _isJumping = true;
            _rigidbody.linearVelocityY = _jumpForce;
            _animator.SetBool(InputConstans.Jump, _isJumping);
        }
    }
}
