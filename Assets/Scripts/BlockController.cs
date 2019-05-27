using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public int HP=1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            HP -= GameManager.Attack;
            if (HP <= 0)
                Destroy(gameObject);
        }
    }
}
