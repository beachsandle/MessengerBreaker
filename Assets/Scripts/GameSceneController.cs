using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class GameSceneController : MonoBehaviour
{
    //상수
    private readonly float Y_ZERO = 3.375f;
    private readonly float X_ZERO = -2.625f;
    private readonly float BLOCK_LENGTH = 0.75f;
    private readonly int LEVEL_LENGTH = 4;
    private readonly int COLUMNS = 8;
    private readonly int START_LINE = 3;
    //
    //내부 변수
    private List<GameObject> balls = new List<GameObject>();
    private List<List<GameObject>> gameBoard=new List<List<GameObject>>();
    private GameObject blockBox;
    private int lineCount;
    [ReadOnly]
    public int score = 0;
    //
    //외부 변수
    public int StageLevel=1;
    public int BlockHP = 1;
    public GameObject Ball;
    public GameObject[] Blocks;
    //
    //내부 속성
    private int RandomBlockIndex
    {
        get
        {
            return Random.Range(0, Blocks.Length);
        }
    }
    //
    //behaviour
    void Start()
    {
        blockBox = new GameObject();
        blockBox.name = "blockBox";
        lineCount = LEVEL_LENGTH;
        var StartPosition = Vector3.zero;
        var StartAngle = Random.Range(15f, 75f)+90*Random.Range(0,2);
        SpanBall(StartPosition, StartAngle);
        StartAngle = Random.Range(20f, 160f);
        SpanBall(StartPosition, StartAngle);
        for(int row=0;row< START_LINE; row++)
            SpanBlockLine();
    }
    void FixedUpdate()
    {
        RespanBlockLine();
    }
    //
    //내부 함수
    private void SpanBall(Vector3 position,float angle)
    {
        var ball = Instantiate(Ball, position, Quaternion.Euler(0,0,0));
        var controller = ball.GetComponent<BallController>();
        controller.Angle = angle;
        controller.Speed = GameManager.BallSpeed;
        controller.controller = this;
        balls.Add(ball);
    }
    private void SpanBlock(int x)
    {
        var spanPos = new Vector3(X_ZERO + BLOCK_LENGTH * x, Y_ZERO , 0);
        var block = Instantiate(Blocks[RandomBlockIndex], spanPos, Quaternion.Euler(0, 0, 0) , blockBox.transform);
        var controller = block.GetComponent<BlockController>();
        controller.MaxHP = BlockHP;
        controller.controller = this;
        gameBoard[0].Add(block);
    }
    private void Pushballs()
    {
        balls.ForEach(ball => {
            if (ball.transform.position.y>= -1.8)
                ball.transform.position = ball.transform.position + Vector3.down * BLOCK_LENGTH;
        });
    }
    private void PushBlockLine()
    {
        foreach (var line in gameBoard)
            foreach (var block in line)
                block.transform.position = block.transform.position + Vector3.down * BLOCK_LENGTH;
    }
    private void SpanBlockLine()
    {
        //Pushballs();
        PushBlockLine();
        gameBoard.Insert(0,new List<GameObject>());
        for (int col = 0; col < COLUMNS; col++)
            SpanBlock(col);
    }
    private void RespanBlockLine()
    {
        if (gameBoard[START_LINE-1].Count <= 2)
        {
            SpanBlockLine();
            --lineCount;
            if (lineCount == 0)
            {
                ++StageLevel;
                BlockHP = StageLevel;
                lineCount = LEVEL_LENGTH;
            }
        }
    }
    //
    //외부 함수
    public void BallDestroy(GameObject ball)
    {
        balls.Remove(ball);
        Destroy(ball);
    }
    public void BlockDestroy(GameObject block)
    {
        score += block.GetComponent<BlockController>().MaxHP * 100;
        gameBoard.ForEach(list=>list.Remove(block));
        gameBoard.RemoveAll(list => list.Count == 0);
        Destroy(block);
    }
    //
}
