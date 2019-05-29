using UnityEngine;


public class BlockController : MonoBehaviour
{
    private int shell;
    public GameSceneController controller;
    public int Level = 1;
    public int HP = 1;
    public BLOCK_TYPE bType;
    private void Start()
    {
        HP = Level * (bType == BLOCK_TYPE.LINE ? 3 : bType == BLOCK_TYPE.SLACK ? 2 : 1);
        shell = bType == BLOCK_TYPE.FEBOOK ? 2 : 0;
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
