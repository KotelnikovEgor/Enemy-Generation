using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _limitPosition = 0f;

    private Vector3 _direction;

    public event Action<Enemy> FellOverEdge;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _direction.normalized);

        if (transform.position.y < _limitPosition)
            FellOverEdge?.Invoke(this);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
