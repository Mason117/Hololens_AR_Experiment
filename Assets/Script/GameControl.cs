using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameControl : MonoBehaviour
{

    public static GameControl instance = null;

    [SerializeField]
    GameObject restartButton;//重新开始

    [SerializeField]
    Text highScoreText;

    [SerializeField]
    Text yourScoreText;

    [SerializeField]
    GameObject[] obstacles;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float spawnRate = 2f;
    float nextSpawn;

    [SerializeField]
    float timeToBoost = 5f;
    float nextBoost;
    private static float timeLimt = 200;
    private static float deductTime = 0;
    private static int trial = 1;
    int timeRecord = 0, UserScore = 0;

    public static bool gameStopped;
    public static bool changePlane;
    private static string path;
    float nextScoreIncrease = 0f;

    private int Times = 0;
    private int lastTime = 233;


    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        restartButton.SetActive(false);
        UserScore = 0;
        gameStopped = true;
        changePlane = false;

        nextSpawn = Time.time + spawnRate;
        nextBoost = Time.unscaledTime + timeToBoost;
        Time.timeScale = 0;//暂停

        path = Application.dataPath + "/Task2_Log" + System.DateTime.Now.Hour.ToString() + "_" + System.DateTime.Now.Minute.ToString() + ".txt";
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameStopped)
        {
            highScoreText.text = "Time Spnd: " + (int)(Time.time-deductTime);
            yourScoreText.text = "Your Mistakes: " + UserScore;

        if (Time.time > nextSpawn)
            SpawnObstacle();

        if (Time.unscaledTime > nextBoost && !gameStopped)
            BoostTime();

        if (Time.time > timeLimt)
        {
            // StopGame();
            RestartGame();
        }
        }

    }

    public void StopGame()
    {
        Time.timeScale = 0;//暂停游戏场景！！！
        gameStopped = true;
        restartButton.SetActive(true);
    }

    public void DinoHit()
    {
        UserScore++;
        GetComponent<NewNetWorkC>().Sending(0,0,1);
        Myprint(trial, UserScore, Time.time - deductTime);
    }

    void SpawnObstacle()
    {
        nextSpawn = Time.time + spawnRate;
        int randomObstacle = Random.Range(0, obstacles.Length);
        if (randomObstacle == lastTime)
        {
            Times++;
        }
        else
        {
            Times = 0;
        }
        //确保不要连续生成三个一样的随机数
        if (Times >= 2)
        {
            SpawnObstacle();
        }
        else
        {
            Instantiate(obstacles[randomObstacle], spawnPoint.position, Quaternion.identity);
            lastTime = randomObstacle;
        }
    }

    void BoostTime()
    {
        nextBoost = Time.unscaledTime + timeToBoost;
        Time.timeScale += 0.3f;
    }

    void IncreaseYourScore()
    {
        if (Time.unscaledTime > nextScoreIncrease)
        {
            nextScoreIncrease = Time.unscaledTime + 1;
        }
    }

    public void RestartGame()
    {

        SceneManager.LoadScene("SampleScene");
        deductTime += 200;
        timeLimt += 200;
        trial += 1;
    }

    public static void Myprint(int info, int info1, float info2)
    {
        StreamWriter sw;
        sw = new StreamWriter(path, true);
        if (info1==1)
        {
            string fileTitle = "Date Time for Trial " + info + " : " + System.DateTime.Now.ToString();
            sw.WriteLine(fileTitle);
            string lineInfo = "Hit" +"\t" + "Time ";
            sw.WriteLine(lineInfo);
            sw.WriteLine(info1 + "\t" + info2 );
        }
        else
        {
            sw.WriteLine(info1 + "\t" + info2);
        }
        sw.Flush();
        sw.Close();
    }
}
