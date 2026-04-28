using System.Collections.Generic;
using UnityEngine;

public class Patrol2D : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] private List<Transform> _points;

    [Header("Movement")]
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _reachDistance = 0.1f;

    [Header("Wait")]
    [SerializeField] private float _waitTime = 0f;

    private int _currentIndex;
    private float _waitTimer;

    private void Update()
    {
        if (_points == null || _points.Count == 0)
            return;

        Transform target = _points[_currentIndex];

        Vector2 currentPos = transform.position;
        Vector2 targetPos = target.position;

        float distance = Vector2.Distance(currentPos, targetPos);

        // Если дошли до точки
        if (distance <= _reachDistance)
        {
            if (_waitTime > 0f)
            {
                _waitTimer += Time.deltaTime;

                if (_waitTimer < _waitTime)
                    return;

                _waitTimer = 0f;
            }

            _currentIndex++;

            if (_currentIndex >= _points.Count)
                _currentIndex = 0;

            return;
        }

        // Движение
        Vector2 direction = (targetPos - currentPos).normalized;
        transform.position += (Vector3)(direction * _speed * Time.deltaTime);
    }
}