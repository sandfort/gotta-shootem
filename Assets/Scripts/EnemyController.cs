using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D _body;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _body.MovePosition(_body.position + Vector2.down * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}