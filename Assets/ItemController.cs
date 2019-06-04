using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public ITEM_TYPE iType;
    public GameSceneController controller;
    void Update()
    {
        transform.position += Vector3.down * 3f * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            controller.PickupItem(this);
            Destroy(gameObject);
        } else
        if (collision.gameObject.CompareTag("Bottom"))
        {
            Destroy(gameObject);
        }
    }
}
