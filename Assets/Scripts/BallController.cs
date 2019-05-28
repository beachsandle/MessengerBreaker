using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float Speed=5f;
    public float Angle = 30f;
    private float bentAngle = 0f;
    public GameSceneController controller;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Quaternion.Euler(0,0,Angle)*Vector2.right*Speed;
    }

    void FixedUpdate()
    {
        if (bentAngle != 0)
        {
            var currentAngle = Vector2.Angle(Vector2.right, rb2d.velocity)+bentAngle;
            bentAngle = 0f;
            if (currentAngle < 15f)
                currentAngle = 15f;
            else if (currentAngle > 165f)
                currentAngle = 165f;
            rb2d.velocity = Quaternion.Euler(0, 0, currentAngle) * Vector2.right * Speed;
        }
        if (rb2d.velocity.x == 0 || rb2d.velocity.y == 0)
            rb2d.velocity = Quaternion.Euler(0, 0, Angle) * Vector2.right * Speed*(rb2d.velocity.y<=0?1:-1);
        if (rb2d.velocity.magnitude <= Speed * 2 / 3)
            rb2d.velocity = rb2d.velocity.normalized * Speed;
    }

    private void SetBentAngle(Collision2D collision)
    {
        if (transform.position.y < collision.collider.bounds.center.y)
            return;
        var xdist = -transform.position.x+ collision.collider.bounds.center.x;
        bentAngle = 30f*xdist / collision.collider.bounds.extents.x;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetBentAngle(collision);
        }
        if (collision.gameObject.CompareTag("Bottom"))
        {
            controller.BallDestroy(gameObject);
        }

    }
}
