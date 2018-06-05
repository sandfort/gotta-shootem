using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float Speed;
    public float LifeSpan;

    private Rigidbody2D _body;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LifeSpan -= Time.deltaTime;


        if (LifeSpan < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            _body.MovePosition(_body.position + Vector2.up * Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}