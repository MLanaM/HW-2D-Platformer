using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private float _offset = 1.5f;
    private float _smoothSpeedX = 1f;
    private float _smoothSpeedY = 2f;

    private float _minX = -5.1f;
    private float _maxX = 11.1f;

    private float _minY = 0f;

    void LateUpdate()
    {
        Vector3 nextPosition = transform.position;

        TransformPosition(ref nextPosition.x, _target.position.x, _smoothSpeedX, _minX, _maxX);
        TransformPosition(ref nextPosition.y, _target.position.y, _smoothSpeedY, _minY);

        transform.position = nextPosition;
    }

    private void TransformPosition(ref float current, float target, float smoothSpeed, float min, float max = float.MaxValue)
    {
        if (Mathf.Abs(target - current) <= _offset)
            return;

        current = Mathf.Lerp(current, target, smoothSpeed * Time.deltaTime);
        current = Mathf.Clamp(current, min, max);
    }
}