using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float Speed=5f;
    public float Angle = 30f;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Quaternion.Euler(0,0,Angle)*Vector2.right*Speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb2d.velocity.x == 0 || rb2d.velocity.y == 0)
            rb2d.velocity = Quaternion.Euler(0, 0, Angle) * Vector2.right * Speed;
        if (rb2d.velocity.magnitude <= Speed * 2 / 3)
            rb2d.velocity = rb2d.velocity.normalized * Speed;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
