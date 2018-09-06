using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEditor;
using UnityEngine;


public class DinoControl : MonoBehaviour
{

    Animator anim;
    Rigidbody2D rb;

    public GameObject manager;
    public GameObject CheckBar;
    [SerializeField]
    float jumpForce = 650f;

    private KeyCode ke1 { get; set; }
    private KeyCode ke2 { get; set; }
    private int[] pattern;

    private int[] pattern0 = { 276, 273, 275, 274 };//right up down left
    private int[] pattern1 = { 97, 119, 100, 115 }; //a,w,d,s
    private int[] pattern2 = { 102, 116, 104, 103 };//f,t,h,g
    private int[] pattern3 = { 106, 105, 108, 107 };//j,i,k,l
    private int[] pattern4 = { 260, 264, 262, 261 };//4 8 6 5

    // 设置两个index
    public int number, number1, number2, plan;

    //private float downTime = 0;
  
    // Use this for initialization
    void Start()
    {
        number = 0;
        plan = 0;
        pattern = pattern0;
        ke1 = (KeyCode)pattern[1];
        number1 = 2;
        //ke2 = (KeyCode)pattern[3];
        //number2 = 4;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Time.time > downTime)
        //{
        //    anim.SetBool("isDown", false);
        //}

        if (GameControl.gameStopped != true)
        {//游戏在运行

            if (Input.GetKeyDown(ke1) && rb.velocity.y == 0)//确保恐龙不是在空中跳了又跳
                rb.AddForce(Vector2.up * jumpForce);
            //if (Input.GetKeyDown(ke2) && rb.velocity.y == 0)
            //{
            //    anim.SetBool("isDown", true);//我在unity里设置的就是isDown
            //    downTime = Time.time + 2f;
            //}
        }
    }

    public void SetKey(int trial)
    {
        number = trial;
        Debug.Log(number);
        switch (number)
        {
            case 1:
                pattern = pattern1;
                break;
            case 2:
                pattern = pattern2;
                break;
            case 3:
                pattern = pattern2;//相同形状， 显示颜色不同。
                break;
            case 4:
                pattern = pattern3;
                break;
            default:
                pattern = pattern0;
                break;
        }
        ChangeKey();
    }

    public void ChangeKey()
    {

        plan=plan+Random.Range(1,3);

        if (plan > 3)
        {
            plan = plan-4;
        }

        switch (plan)
        {
            case 0:
                ke1 = (KeyCode)pattern[1];
                number1 = 2;
                //ke2 = (KeyCode)pattern[3];
                //number2 = 4;
                //GameControl.changePlane = false;
                Debug.Log("change the plan!!!!!!!!!!!");
                break;
            case 1:
                ke1 = (KeyCode)pattern[2];
                number1 = 3;
                //ke2 = (KeyCode)pattern[0];
                //number2 = 1;
                //GameControl.changePlane = false;
                Debug.Log("change the plan!!!!!!!!!!!");
                break;
            case 2:
                ke1 = (KeyCode)pattern[3];
                number1 = 4;
                //ke2 = (KeyCode)pattern[1];
                //number2 = 2;
                //GameControl.changePlane = false;
                Debug.Log("change the plan!!!!!!!!!!!");
                break;
            case 3:
                ke1 = (KeyCode)pattern[0];
                number1 = 1;
                //ke2 = (KeyCode)pattern[2];
                //number2 = 3;
                //GameControl.changePlane = false;
                Debug.Log("change the plan!!!!!!!!!!!");
                break;
        }
        Debug.Log(plan);
        Debug.Log(ke1);
    }
}
