using UnityEngine;

public class EnemyMovement : BaseMovement
{
    [SerializeField] private float _patrolDistance = 5f;

    private float _startX;

    protected override void Awake()
    {
        base.Awake();

        _startX = transform.position.x;
        _direction = 1f;
    }

    protected override void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - _startX) >= _patrolDistance)
            TurnAround();

        base.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            TurnAround();
    }

    private void TurnAround()
    {
        _direction *= -1;
        _startX = transform.position.x;
    }
}