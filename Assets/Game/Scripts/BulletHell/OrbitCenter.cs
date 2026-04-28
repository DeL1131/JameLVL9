using UnityEngine;

public class OrbitCenter : MonoBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private float _lifeTime;
    private float _timer;

    public void Initialize(Vector2 direction, float speed, float lifeTime)
    {
        _direction = direction.normalized;
        _speed = speed;
        _lifeTime = lifeTime;
        _timer = 0f;
    }

    private void Update()
    {
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);

        _timer += Time.deltaTime;

        if (_timer >= _lifeTime)
            Destroy(gameObject);
    }
}