using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float Speed=5f;
    public float Angle = 30f;
    private float? reflectAngle = null;
    public GameSceneController controller;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Quaternion.Euler(0,0,Angle)*Vector2.right*Speed;
    }

    void FixedUpdate()
    {
        if (reflectAngle != null)
        {
            rb2d.velocity = Quaternion.Euler(0, 0, (float)reflectAngle) * Vector2.right * Speed;
            reflectAngle = null;
        }
        if (rb2d.velocity.y == 0)
            rb2d.velocity = Quaternion.Euler(0, 0, Angle) * Vector2.right * Speed*(rb2d.velocity.y<=0?1:-1);
        if (rb2d.velocity.magnitude <= Speed * 2 / 3)
            rb2d.velocity = rb2d.velocity.normalized * Speed;
    }

    private void ContantToPlayer(Collision2D collision)
    {

        var collisionRatio = (collision.collider.bounds.center.x - transform.position.x)/ collision.collider.bounds.extents.x;
        reflectAngle = 90+75f* collisionRatio;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.y < collision.collider.bounds.center.y)
                return;
            ContantToPlayer(collision);
        } else
        if (collision.gameObject.CompareTag("Bottom"))
        {
            controller.BallDestroy(gameObject);
        }

    }
}
