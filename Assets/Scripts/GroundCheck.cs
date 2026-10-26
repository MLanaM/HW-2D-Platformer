using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private float _groundCheckRadius = 0.1f;

    public bool IsGrounded() =>
        Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
}
