using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum BLOCK_TYPE { KAKAO, LINE, FEBOOK, SLACK }
public enum ITEM_TYPE { ATTACK, BALL, BAR, BOMB}


public class GameSceneController : MonoBehaviour
{
    //상수
    private readonly float Y_ZERO = 3.375f;
    private readonly float X_ZERO = -2.625f;
    private readonly float BLOCK_SIZE = 0.75f;
    private readonly float FALLING_TIME = 0.2f;
    private readonly float DROP_DELAY = 0.2f;
    private readonly int LEVEL_LENGTH = 4;
    private readonly int COLUMNS = 8;
    private readonly int START_LINE = 3;
    private readonly int LINE_BLOCK_COUNT = 1;
    private readonly int FEBOOK_BLOCK_COUNT = 2;
    //
    //내부 변수
    private List<GameObject> balls = new List<GameObject>();
    private List<GameObject[]> blockLines = new List<GameObject[]>();
    private GameObject[] Blocks;
    private GameObject[] Items;
    private GameObject blockContainner;
    private int lineCount;
    private int score = 0;
    private int StageLevel = 1;
    //
    //외부 변수
    public Text LevelText;
    public Text ScoreText;
    public Camera MainCamera;
    public GameObject Player;
    public GameObject Ball;
    public GameObject KakaoBlock;
    public GameObject LineBlock;
    public GameObject FacebookBlock;
    public GameObject SlackBlock;
    public GameObject AttackItem;
    public GameObject BallItem;
    //
    //내부 속성
    //
    //behaviour
    void Start()
    {
        MainCamera.orthographicSize = 3f / ((float)Screen.width / Screen.height);
        Blocks = new GameObject[4] { KakaoBlock, LineBlock, FacebookBlock, SlackBlock };
        Items = new GameObject[2] { AttackItem,BallItem };
        blockContainner = new GameObject();
        blockContainner.name = "blockContainner";
        lineCount = LEVEL_LENGTH;
        SpanBall(Random.Range(15f, 175f));
        StartBlockLine();
        LevelText.text = " Level : 1";
        ScoreText.text = "Score : 0";
    }
    //
    //내부 함수
    private void SpanBall(float angle,Vector3? position= null)
    //position에서 angle로 발사되는 공 생성
    {
        var ball = Instantiate(Ball, position ?? new Vector3(0, -2, 0), Quaternion.Euler(0, 0, 0));
        var controller = ball.GetComponent<BallController>();
        controller.Angle = angle;
        controller.Speed = GameManager.BallSpeed;
        controller.controller = this;
        balls.Add(ball);
    }
    private void SpanBlock(int x, int y, bool effect, BLOCK_TYPE btype = BLOCK_TYPE.KAKAO)
    // x,y에 btype 블럭 생성
    {
        int spanRow = effect ? y - 1 : y;
        var spanPos = new Vector3(X_ZERO + x * BLOCK_SIZE, Y_ZERO - spanRow * BLOCK_SIZE, 0);
        var block = Instantiate(Blocks[(int)btype], spanPos, Quaternion.Euler(0, 0, 0), blockContainner.transform);
        var controller = block.GetComponent<BlockController>();
        controller.Level = StageLevel;
        controller.controller = this;
        controller.bType = btype;
        blockLines[y][x] = block;
    }
    private void SpanItem(GameObject block,ITEM_TYPE itype)
    //block 위치에 itype 아이템 생성
    {
        var spanPos = block.transform.position;
        var item= Instantiate(Items[(int)itype], spanPos, Quaternion.Euler(0, 0, 0));
        var controller = item.GetComponent<ItemController>();
        controller.controller = this;
        controller.iType = itype;
    }
    private IEnumerator CoPushBlockLine(GameObject[] line)
    //[coroutine]line을 FALLING_TIME에 걸쳐 BLOCK_SIZE만큼 하강
    {
        yield return new WaitForSeconds(DROP_DELAY);
        var downtime = 0f;
        var movement = Vector3.down * BLOCK_SIZE / FALLING_TIME;
        while (true)
        {
             line.Meet(block => block != null).ToList().ForEach(block => block.transform.Translate(movement * Time.deltaTime));
            downtime += Time.deltaTime;
            if (downtime > FALLING_TIME)
            {
                line.Meet(block => block != null).ToList().ForEach(block => block.transform.Translate(movement * (FALLING_TIME - downtime)));
                break;
            }
            yield return null;
        }
    }
    private void PushBlockLine()
    //blockLines의 모든 라인을 하강
    {
        foreach (var line in blockLines)
            StartCoroutine(CoPushBlockLine(line));
    }

    private void SpanBlockLine(int y = 0, bool special = false, bool effect = true)
    //y행에 블럭라인 생성
    {
        var columns = Enumerable.Range(0, COLUMNS).ToList().Shuffle();
        blockLines.Insert(y, new GameObject[8]);
        for (int x = 0; x < LINE_BLOCK_COUNT; x++)
        {
            SpanBlock(columns[0], y, effect, BLOCK_TYPE.LINE);
            columns.RemoveAt(0);
        }
        for (int x = 0; x < FEBOOK_BLOCK_COUNT; x++) { 
            SpanBlock(columns[0], y, effect,BLOCK_TYPE.FEBOOK);
            columns.RemoveAt(0);
        }
        if (special)
        {
            SpanBlock(columns[0], y, effect, BLOCK_TYPE.SLACK);
            columns.RemoveAt(0);
        }
        while (columns.Count != 0)
        {
            SpanBlock(columns[0], y, effect);
            columns.RemoveAt(0);
        }
        if (effect)
            PushBlockLine();
    }
    private void StartBlockLine()
    {
        SpanBlockLine(0, true, false);
        for(int i=1;i<START_LINE;++i)
            SpanBlockLine(i, false, false);
    }
    private void RespanBlockLine()
    {
        --lineCount;
        if (lineCount == 0)
        {
            ++StageLevel;
            lineCount = LEVEL_LENGTH;
            LevelText.text = $" Level : {StageLevel.ToString()}";
            SpanBlockLine(0,true);
        }
        else
            SpanBlockLine(0);
    }
    //
    //외부 함수
    public void BallDestroy(GameObject ball)
    {
        balls.Remove(ball);
        Destroy(ball);
    }
    public void BlockDestroy(BlockController blockController)
    {
        score += blockController.Level * 100;
        switch(blockController.bType)
        {
            case BLOCK_TYPE.LINE:
                RespanBlockLine();
                if (Random.Range(0, 100) >= 50)
                    SpanItem(blockController.gameObject, ITEM_TYPE.BALL);
                break;

            case BLOCK_TYPE.FEBOOK:
                score += blockController.Level * 100;
                break;

            case BLOCK_TYPE.SLACK:
                if (Random.Range(0, 100) >= 50)
                    SpanItem(blockController.gameObject, ITEM_TYPE.ATTACK);
                else
                    SpanItem(blockController.gameObject, ITEM_TYPE.BALL);
                break;
        }
        ScoreText.text = $"Score : {score.ToString()}";
        blockLines.RemoveAll(line => line.CountIf(element => element != null) == 0);
        Destroy(blockController.gameObject);
    }

    public void PickupItem(ItemController controller)
    {
        switch (controller.iType)
        {
            case ITEM_TYPE.ATTACK:
                ++GameManager.Attack;
                break;
            case ITEM_TYPE.BALL:
                SpanBall(Random.Range(15f, 175f),Player.transform.position);
                break;
        }
    }
    //
}
