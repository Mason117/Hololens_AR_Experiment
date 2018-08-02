using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Random = System.Random;

public class NetWorkForTask3 : MonoBehaviour
{

    //电脑客户端task3！！！！！
    public NetworkClient client;
    public string Serverip = "192.168.12.203";  //hololens ip address
    public int Serverport = 8808;
    public const short RegisterHotsMsgId = 1003;

    private bool isAtstartup = true;
    private bool countdown = false;
    private bool testStart = false;

    private int number;
    private int lastNumber=0;
    private int numberM;
    private int testRecord=0;
    private int buttonRecord = 0;

    private float nextTime = 0f;
    private float testStartTime = 0f;
    private int aim;
    private float rate;


    private static int line = 0;
    private string[] plan;
    private string[] plan1={"a","w","d","s"};
    private string[] plan2 = { "1", "2", "3", "4" };
    private string[] plan3 = { "g", "y", "j", "l" };

    [SerializeField]
    public Text theTextInformation;

    public class Actionmsg : MessageBase
    {
        public float aimRate;
        public float rate;
        public int state;
    }

    void Start()
    {
        ChoosePlan();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown == true)
        {
            theTextInformation.enabled = true;
            int number1M = System.DateTime.Now.Minute;
            int number1 = System.DateTime.Now.Second;
            if (number1M != numberM)
            {
                number1 += 60;
            }
            theTextInformation.text = "Test start in " + (number - number1) + " seconds.";
            if (number1 - number == 0)
            {
                countdown = false;
                GenerateTest();
                testStart = true;
            }
        }

        if (testStart)
        {
            if (Time.time > nextTime)
            {
                GenerateTest();
            }

            if (Input.GetKeyDown(theTextInformation.text))
            {
                float timeInterval;
                buttonRecord++;
                if (buttonRecord==1)
                {
                    testStartTime = Time.time;//time.time以0.5秒为单位从一开始
                    timeInterval = 2f;
                }
                else
                {
                    timeInterval = Time.time - testStartTime;
                }
                rate = buttonRecord / timeInterval;
                Sending(aim, rate,1);
                Myprint( theTextInformation.text, aim.ToString(), rate.ToString());
            }
        }
    }

    void GenerateTest()
    {
        //generate the test button and target rate
        number = UnityEngine.Random.Range(1, 500);
        Random ro = new Random(number);
        number=ro.Next(1, 5);
        if (number==lastNumber || number==5)
        {
            GenerateTest();
            return;
        }
        else
        {
            lastNumber = number;
        }

        testRecord++;
        if (testRecord == 7)
        {
            //结束
            EndingTest();
            return;
        }

        //到底是整数的好还是小数的好现在不知道！！！！！！！！！！！！！！！！！！
        aim = UnityEngine.Random.Range(3, 8);
        theTextInformation.text = ReturnStr(number);
        Sending(aim, 0f,1);
        Debug.Log(number);
        nextTime = Time.time + 8f;
        //testStartTime = Time.time;
        buttonRecord = 0;
    }


    string ReturnStr(int num)
    {
        string temp = null;
        switch (num)
        {
            case 1:
                temp = plan[0];
                break;
            case 2:
                temp = plan[1];
                break;
            case 3:
                temp = plan[2];
                break;
            case 4:
                temp = plan[3];
                break;
        }
        return temp;
    }

    void Sending(float theinfor, float theRate, int thestate)
    {
        Actionmsg msg = new Actionmsg();
        msg.aimRate = theinfor;
        msg.rate = theRate;
        msg.state = thestate;
        client.Send(RegisterHotsMsgId, msg);
        //Debug.Log("Sending..."+ theinfor);
    }

    private void OnGUI()
    {
        if (isAtstartup)
        {
            Serverip = GUILayout.TextField(Serverip);
            int.TryParse(GUILayout.TextField(Serverport.ToString()), out Serverport);

            if (GUILayout.Button("Connet"))
            {
                client = new NetworkClient();
                client.RegisterHandler(MsgType.Connect, OnConnected);
                client.Connect(Serverip, Serverport);
            }
        }
    }

    void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
        isAtstartup = false;
        countdown = true;
        numberM = System.DateTime.Now.Minute;
        number = System.DateTime.Now.Second + 4;
    }

    public static void Myprint(string info, string info1, string info2)
    {
        line++;
        string path = Application.dataPath + "/TaskLogFile.txt";
        StreamWriter sw;
        //Debug.Log(path);
        if (line == 1)
        {
            sw = new StreamWriter(path, false);
            string fileTitle = "日志文件创建的时间  " + System.DateTime.Now.ToString();
            sw.WriteLine(fileTitle);
        }
        else
        {
            sw = new StreamWriter(path, true);
        }

        string lineInfo = line + "\t" + "时刻 " + Time.time + ": ";
        sw.WriteLine(lineInfo);
        sw.WriteLine(info+"   "+info1+"    "+info2);
        //Debug.Log(info);
        sw.Flush();
        sw.Close();
    }

    void ChoosePlan()
    {
        int randomNumber = UnityEngine.Random.Range(1, 5);
        int randomNumber2 = UnityEngine.Random.Range(1, 20);
        int type = (randomNumber + randomNumber2) % 3;
        switch (type)
        {
            case 0:
                plan = plan1;
                break;
            case 1:
                plan = plan2;
                break;
            case 2:
                plan = plan3;
                break;
        }
    }

    void EndingTest()
    {
        SceneManager.LoadScene(0);
        Sending(0f, 0f,0);
        ChoosePlan();
    }

}
