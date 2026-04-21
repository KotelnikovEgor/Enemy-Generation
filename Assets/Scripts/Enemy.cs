using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Target _target;
    private Vector3 _direction;

    private readonly float _limitDistance = 0.1f;

    public event Action<Enemy> CaughtTarget;

    private void Update()
    {
        _direction = (_target.transform.position - transform.position).normalized;
        transform.Translate(_direction * _speed * Time.deltaTime);

        float distance = (_target.transform.position - transform.position).sqrMagnitude;
        if (distance < _limitDistance)
            CaughtTarget?.Invoke(this);
    }

    public void SetTarget(Target target)
    {
        _target = target;
    }
}
