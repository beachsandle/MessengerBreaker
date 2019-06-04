using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float barSpeed = 5f;

    void FixedUpdate()
    {
        var coll = GetComponent<BoxCollider2D>();
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (coll.bounds.center.x - coll.bounds.extents.x > -3))
        {
            transform.Translate(Vector3.left * barSpeed * Time.deltaTime);
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (coll.bounds.center.x + coll.bounds.extents.x < 3))
        {
            transform.Translate(Vector3.right * barSpeed * Time.deltaTime);
        }
    }
}
