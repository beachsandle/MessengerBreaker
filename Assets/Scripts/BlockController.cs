using UnityEngine;


public class BlockController : MonoBehaviour
{
    private int shell=0;

    public GameSceneController controller;
    public BLOCK_TYPE bType;
    public int Level = 1;
    public int HP = 1;

    private void Start()
    {
        HP = Level * (bType == BLOCK_TYPE.LINE||bType==BLOCK_TYPE.FEBOOK ? 2 : 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (shell != 0)
                --shell;
            else
                HP -= GameManager.Attack;

            if (HP <= 0)
                controller.BlockDestroy(this);
        }
    }
}
