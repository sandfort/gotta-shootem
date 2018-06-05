using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    public float MaxSpeed;
    public float NeutralTolerance;
    public GameObject ShotType;

    private Rigidbody2D _body;
    private Vector2 _velocity;
    private int _health;
    private readonly SpriteRenderer[] _damageSprites = new SpriteRenderer[3];

    private void Start()
    {
        _health = 3;
        _body = GetComponent<Rigidbody2D>();
        _velocity = Vector2.zero;

        _damageSprites[0] = transform.Find("Heavy Damage").GetComponent<SpriteRenderer>();
        _damageSprites[1] = transform.Find("Medium Damage").GetComponent<SpriteRenderer>();
        _damageSprites[2] = transform.Find("Light Damage").GetComponent<SpriteRenderer>();

        ResetDamage();
    }

    private void CheckHealth()
    {
        if (_health < 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (_health < 3)
        {
            ResetDamage();
            _damageSprites[_health].enabled = true;
        }
    }

    private void ResetDamage()
    {
        foreach (var sprite in _damageSprites)
        {
            sprite.enabled = false;
        }
    }

    private void Update()
    {
        CheckHealth();

        var direction = CalculateDirection();
        if (StickNeutral())
        {
            _velocity = Vector2.zero;
        }
        else
        {
            _velocity = _velocity + direction;
        }

        if (_velocity.magnitude > MaxSpeed * Time.deltaTime)
        {
            _velocity = _velocity.normalized * (MaxSpeed * Time.deltaTime);
        }

        var position = ClampToScreen((Vector2) _body.transform.position + _velocity);
        _body.MovePosition(position);

        if (Input.GetButtonDown("Fire1"))
        {
            FireShot();
        }
    }

    private static Vector2 ClampToScreen(Vector2 v)
    {
        return new Vector2(Mathf.Clamp(v.x, -3.5f, 3.5f), Mathf.Clamp(v.y, -4.5f, 4.5f));
    }

    private void FireShot()
    {
        Instantiate(ShotType, _body.position, Quaternion.identity);
    }

    private bool StickNeutral()
    {
        return StickHorizontalNeutral() && StickVerticalNeutral();
    }

    private bool StickVerticalNeutral()
    {
        return Math.Abs(Input.GetAxisRaw("Vertical")) < NeutralTolerance;
    }

    private bool StickHorizontalNeutral()
    {
        return Math.Abs(Input.GetAxisRaw("Horizontal")) < NeutralTolerance;
    }

    private static Vector2 CalculateDirection()
    {
        float xMovement = 0;
        float yMovement = 0;

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            xMovement = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            xMovement = -1;
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            yMovement = 1;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            yMovement = -1;
        }

        return new Vector2(xMovement, yMovement).normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy"))
        {
            _health -= 1;
        }
    }
}