using System.Collections;
using System.Collections.Generic;
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

    private int[] pattern1 = { 260,264,262,258};//4 8 6 2

    private int[] pattern2 = { 276, 273, 275, 274 };//Left,Up,right,down

    private int[] pattern3 = {104, 117, 106, 110};//h,u,j,n
    // 设置两个index
    public int number,number1, number2, plan;

    private bool canChangeLocal = false;
    private bool canChangeGlobal = false;
    // Use this for initialization
    void Start()
    {
        number = Random.Range(1, 3);
        switch (number)
        {
            case 1:
                pattern = pattern1;
                break;
            case 2:
                pattern = pattern2;
                break;
            case 3:
                pattern = pattern3;
                break;
        }
        ke1 = (KeyCode)pattern[1];
        number1 = 2;
        ke2 = (KeyCode)pattern[3];
        number2 = 4;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameControl.changePlane == true)
        {           
            canChangeGlobal = CheckBar.GetComponent<CheckPoint>().Check;
            if (canChangeLocal == true && canChangeGlobal == true)
            {
                plan++;
                if (plan > 4)
                {
                    plan = 0;
                }
                switch (plan)
                {
                    case 0:
                        ke1 = (KeyCode)pattern[1];
                        number1 = 2;
                        ke2 = (KeyCode)pattern[3];
                        number2 = 4;
                        GameControl.changePlane = false;
                        Debug.Log("change the plan!!!!!!!!!!!");
                        break;
                    case 1:
                        ke1 = (KeyCode)pattern[2];
                        number1 = 3;
                        ke2 = (KeyCode)pattern[0];
                        number2 = 1;
                        GameControl.changePlane = false;
                        Debug.Log("change the plan!!!!!!!!!!!");
                        break;
                    case 2:
                        ke1 = (KeyCode)pattern[0];
                        number1 = 1;
                        ke2 = (KeyCode)pattern[2];
                        number2 = 3;
                        GameControl.changePlane = false;
                        Debug.Log("change the plan!!!!!!!!!!!");
                        break;
                    case 3:
                        ke1 = (KeyCode)pattern[3];
                        number1 = 4;
                        ke2 = (KeyCode)pattern[1];
                        number2 = 2;
                        GameControl.changePlane = false;
                        Debug.Log("change the plan!!!!!!!!!!!");
                        break;
                }     
                Debug.Log(CheckBar.GetComponent<CheckPoint>().Checknumber);
            }
        }

        if (GameControl.gameStopped != true)
        {//游戏在运行

            if (Input.GetKeyDown(ke1) && rb.velocity.y == 0)//确保恐龙不是在空中跳了又跳
                rb.AddForce(Vector2.up * jumpForce);

            if (Input.GetKeyDown(ke2) && rb.velocity.y == 0)
            {
                anim.SetBool("isDown", true);//我在unity里设置的就是isDown
                canChangeLocal = false;
            }

            if (Input.GetKeyUp(ke2) && rb.velocity.y == 0)
            {
                anim.SetBool("isDown", false);
                canChangeLocal = true;
            }              
        }
    }
}
