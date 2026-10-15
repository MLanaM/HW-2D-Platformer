using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class BaseMovement : MonoBehaviour
{
    protected const string Horizontal = nameof(Horizontal);

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

    protected virtual void Update()
    {
        if (_direction != 0)
            Flip(_direction < 0);

        _animator.SetFloat(Horizontal, Mathf.Abs(_direction));
    }

    protected virtual void FixedUpdate() => 
        Move();

    protected virtual void Move() =>
        _rigidbody.linearVelocity = new Vector2(_direction * _moveSpeed, _rigidbody.linearVelocity.y);

    protected virtual void Flip(bool lookLeft)
    {
        Vector3 scale = transform.localScale;

        scale.x = lookLeft ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
}
