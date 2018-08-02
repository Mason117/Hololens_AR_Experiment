using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl2 : MonoBehaviour {

    public static GameControl2 instance = null;


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

    private int obstacleNumber = 0;

    public static bool gameStopped;
    public static bool changePlane;

    float nextScoreIncrease = 0f;

    private int Times = 0;
    private int lastTime = 233;

    private float timeLeft = 3f;
    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        gameStopped = true;
        changePlane = false;

        nextSpawn = Time.time + spawnRate;
        nextBoost = Time.unscaledTime + timeToBoost;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
            SpawnObstacle();

        if (Time.unscaledTime > nextBoost && !gameStopped)
            BoostTime();
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
            obstacleNumber++;
            if (obstacleNumber > 5)
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

}
