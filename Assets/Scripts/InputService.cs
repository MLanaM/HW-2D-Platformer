using UnityEngine;

public class InputService : MonoBehaviour
{
    public float GetDirection() =>
        Input.GetAxis(InputConstans.Horizontal);

    public bool isTryingToJump() =>
        Input.GetButtonDown(InputConstans.Jump);
}
