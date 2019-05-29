using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

enum BLOCK_TYPE { KAKAO,LINE,FEBOOK,SLACK}
public class GameSceneController : MonoBehaviour
{
    //상수
    private readonly float Y_ZERO = 4.125f;
    private readonly float X_ZERO = -2.625f;
    private readonly float BLOCK_LENGTH = 0.75f;
    private readonly int LEVEL_LENGTH = 4;
    private readonly int COLUMNS = 8;
    private readonly int START_LINE = 3;
    //
    //내부 변수
    private List<GameObject> balls = new List<GameObject>();
    private List<List<GameObject>> gameBoard=new List<List<GameObject>>();
    private GameObject[] Blocks;
    private GameObject blockBox;
    private int lineCount;
    private int score = 0;
    //
    //외부 변수
    public int StageLevel=1;
    public int BlockHP = 1;
    public Text LevelText;
    public Text ScoreText;
    public Camera MainCamera;
    public GameObject Ball;
    public GameObject KakaoBlock;
    public GameObject LineBlock;
    public GameObject FacebookBlock;
    public GameObject SlackBlock;
    //
    //내부 속성
    //
    //behaviour
    void Start()
    {
        MainCamera.orthographicSize=3f/((float)Screen.width/Screen.height);
        Blocks = new GameObject[4] {KakaoBlock,LineBlock,FacebookBlock,SlackBlock };
        blockBox = new GameObject();
        blockBox.name = "blockBox";
        lineCount = LEVEL_LENGTH;
        var StartPosition = new Vector3(0,-2,0);
        var StartAngle = Random.Range(15f, 175f);
        SpanBall(StartPosition, StartAngle);
        StartAngle = Random.Range(15f, 175f);
        SpanBall(StartPosition, StartAngle);
        for(int row=0;row< START_LINE; row++)
            SpanBlockLine(0.01f);
        LevelText.text = " Level : 1";
        ScoreText.text = "Score : 0";
    }
    void FixedUpdate()
    {
        if (gameBoard[START_LINE - 1].Count <= 2)
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
    private void SpanBlock(int x,BLOCK_TYPE btype)
    {

        var spanPos = new Vector3(X_ZERO + BLOCK_LENGTH * x, Y_ZERO , 0);
        var block = Instantiate(Blocks[(int)btype], spanPos, Quaternion.Euler(0, 0, 0) , blockBox.transform);
        var controller = block.GetComponent<BlockController>();
        controller.MaxHP = BlockHP;
        controller.controller = this;
        gameBoard[0].Add(block);
    }
    private IEnumerator CoPushBlockLine(List<GameObject> line,float fallingTime)
    {
        var downtime = 0f;
        var movement = Vector3.down * BLOCK_LENGTH / fallingTime;
        while (true)
        {
            line.ForEach(block => block.transform.Translate( movement * Time.deltaTime));
            downtime += Time.deltaTime;
            if (downtime > fallingTime)
            {
                line.ForEach(block => block.transform.Translate(movement * (fallingTime- downtime)));
                break;
            }
            yield return null;
        }
    }
    private void PushBlockLine(float fallingTime)
    {
        foreach (var line in gameBoard)
            StartCoroutine(CoPushBlockLine(line, fallingTime));
    }

    private void SpanBlockLine(float fallingTime=0.2f)
    {
        gameBoard.Insert(0,new List<GameObject>());
        for (int col = 0; col < COLUMNS; col++)
            SpanBlock(col,BLOCK_TYPE.KAKAO);
        PushBlockLine(fallingTime);
    }
    private void RespanBlockLine()
    {

        SpanBlockLine();
        --lineCount;
        if (lineCount == 0)
        {
            ++StageLevel;
            BlockHP = StageLevel;
            lineCount = LEVEL_LENGTH;
            LevelText.text = $" Level : {StageLevel.ToString()}";
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
        ScoreText.text = $"Score : {score.ToString()}";
        gameBoard.ForEach(list=>list.Remove(block));
        gameBoard.RemoveAll(list => list.Count == 0);
        Destroy(block);
    }
    //
}
