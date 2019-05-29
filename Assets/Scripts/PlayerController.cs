using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float barSpeed = 5f;

    void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (transform.position.x > -2))
        {
            transform.Translate(Vector3.left * barSpeed * Time.deltaTime);
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (transform.position.x < 2))
        {
            transform.Translate(Vector3.right * barSpeed * Time.deltaTime);
        }
    }
}
