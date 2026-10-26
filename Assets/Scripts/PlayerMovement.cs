using UnityEngine;

public class PlayerMovement : BaseMovement
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Jump _jump;

    private void Update()
    {
        _direction = _inputService.GetDirection();

        FlipCheck();

        _jump.JumpCheck(_inputService.isTryingToJump());
    }

    private void FixedUpdate()
    {
        Move();
    }
}