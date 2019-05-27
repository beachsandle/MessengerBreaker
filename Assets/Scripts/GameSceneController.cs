using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class GameSceneController : MonoBehaviour
{
    public float TEST_BallSpeed = 10f;
    public int StageLevel=1;
    public int BlockHP = 1;
    public GameObject Ball;
    public GameObject[] Blocks;
    private List<GameObject> Balls = new List<GameObject>();
    private List<List<GameObject>> GameBoard=new List<List<GameObject>>();
    private readonly float yZero = 2.625f;
    private readonly float xZero = -2.625f;
    private readonly float blockWidth = 0.75f;
    void Start()
    {
        var StartPosition = Vector3.zero;
        var StartAngle = Random.Range(20f, 160f);
        SpanBall(StartPosition, StartAngle);
        StartAngle = Random.Range(20f, 160f);
        SpanBall(StartPosition, StartAngle);
        SpanBlockLine();
        SpanBlockLine();
        SpanBlockLine();
    }

    void FixedUpdate()
    {
        foreach (var line in GameBoard)
        {
            line.RemoveAll(obj => obj == null);
        }
        if(GameBoard[2].Count<=2)
            SpanBlockLine();
        GameBoard.RemoveAll(list => list.Count == 0);
    }
    private int RandomBlockIndex
    {
        get
        {
            return Random.Range(0, Blocks.Length);
        }
    }

    private void SpanBall(Vector3 position,float angle)
    {
        var ball = Instantiate(Ball, position, Quaternion.Euler(0,0,0));
        ball.GetComponent<BallController>().Angle = angle;
        ball.GetComponent<BallController>().Speed = TEST_BallSpeed;
        Balls.Add(ball);
    }
    private void SpanBlock(int x,int y)
    {
        var spanPos = new Vector3(xZero + blockWidth * x, yZero - blockWidth * y, 0);
        var block = Instantiate(Blocks[RandomBlockIndex], spanPos, Quaternion.Euler(0, 0, 0) );
        GameBoard[0].Add(block);
    }

    private void SpanBlockLine()
    {
        PushBalls();
        PushBlockLine();
        GameBoard.Insert(0,new List<GameObject>());
        for (int i = 0; i < 8; i++)
            SpanBlock(i, 0);
    }
    private IEnumerator CoSpanBlockLine(float waitsecond)
    {
        yield return new WaitForSeconds(waitsecond);
        while (true)
        {
            SpanBlockLine();
            yield return new WaitForSeconds(waitsecond);
        }

    }
    private void PushBlockLine()
    {
        foreach (var line in GameBoard)
        {
            foreach (var block in line)
            {
                block.transform.position = block.transform.position + Vector3.down * blockWidth;
            }
        }
    }
    private void PushBalls()
    {
        Balls.ForEach(ball => {
            if (ball.transform.position.y>= -1.8)
                ball.transform.position = ball.transform.position + Vector3.down * blockWidth;
        });
    }
}
