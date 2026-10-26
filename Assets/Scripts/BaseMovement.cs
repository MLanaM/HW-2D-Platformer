using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class BaseMovement : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Rigidbody2D _rigidbody;

    protected float _direction;

    protected virtual void Awake()
    {
        if (TryGetComponent<Animator>(out Animator animator))
            _animator = animator;

        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
            _rigidbody = rigidbody;
    }

    protected virtual void Move()
    {
        _rigidbody.linearVelocity = new Vector2(_direction * _moveSpeed, _rigidbody.linearVelocity.y);
        AnimatorUpdate();
    }

    protected virtual void FlipCheck()
    {
        if (_direction != 0)
            Flip(_direction < 0);
    }

    private void Flip(bool lookLeft)
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        rotation.y = lookLeft ? 180f : 0f;

        transform.rotation = Quaternion.Euler(rotation);
    }

    private void AnimatorUpdate() =>
        _animator.SetFloat(InputConstans.Horizontal, Mathf.Abs(_direction));
}
