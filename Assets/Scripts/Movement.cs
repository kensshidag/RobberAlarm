using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField, Min(1)] float _moveSpeed;
    [SerializeField, Min(1)] float _rotationSpeed;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float rotate = Input.GetAxis("Horizontal");
        transform.Rotate(_rotationSpeed * rotate * Time.deltaTime * Vector3.up);
    }

    private void Move()
    {
        float direction = Input.GetAxis("Vertical");
        transform.Translate(_moveSpeed * direction * Time.deltaTime * Vector3.forward);
    }
}
