using System.Collections;
using System.Collections.Generic;
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

    int highScore = 0, yourScore = 0;

    public int obstacleNumber = 0;

    public static bool gameStopped;
    public static bool changePlane;

    float nextScoreIncrease = 0f;

    private int Times = 0;
    private int lastTime = 233;

    private float timeLeft = 1f;

    // Use this for initialization
    void Start()
    {     
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        restartButton.SetActive(false);
        yourScore = 0;
        gameStopped = true;
        changePlane = false;

        highScore = PlayerPrefs.GetInt("highScore");
        nextSpawn = Time.time + spawnRate;
        nextBoost = Time.unscaledTime + timeToBoost;
        Time.timeScale = 0;//暂停
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
       // Debug.Log(timeLeft);
        if (timeLeft < 0)
        {
            if (!gameStopped)
            {
                IncreaseYourScore();
            }
            highScoreText.text = "High Score: " + highScore;
            yourScoreText.text = "Your Score: " + yourScore;

            if (Time.time > nextSpawn)
                SpawnObstacle();

            if (Time.unscaledTime > nextBoost && !gameStopped)
                BoostTime();
        }
    }

    public void DinoHit()
    {
        if (yourScore > highScore)
            PlayerPrefs.SetInt("highScore", yourScore);
        Time.timeScale = 0;//暂停游戏场景！！！
        gameStopped = true;
        restartButton.SetActive(true);
    }

    void SpawnObstacle()
    {
        
        nextSpawn = Time.time + spawnRate;

        int randomObstacle = Random.Range(0, obstacles.Length);
        if (randomObstacle==lastTime)
        {
            Times++;
        }
        else
        {
            Times = 0;
        }
        //确保不要连续生成三个一样的随机数
        if (Times>=2)
        {
            SpawnObstacle();
        }
        else
        {
            Instantiate(obstacles[randomObstacle], spawnPoint.position, Quaternion.identity);
            lastTime = randomObstacle;
            obstacleNumber++;
            if (obstacleNumber>5)
            {
                changePlane = true;
                obstacleNumber = 0;
                //恐龙那边会把changeplane变回false
            }
        }
    }

    void BoostTime()
    {
        nextBoost = Time.unscaledTime + timeToBoost;
        Time.timeScale += 0.15f;
    }

    void IncreaseYourScore()
    {
        if (Time.unscaledTime > nextScoreIncrease)
        {
            yourScore += 1;
            nextScoreIncrease = Time.unscaledTime + 1;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
