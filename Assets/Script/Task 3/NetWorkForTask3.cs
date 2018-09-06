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
    public string Serverip = "192.168.12.114";  //hololens ip address
    public int Serverport = 8808;
    public const short RegisterHotsMsgId = 1003;

    private bool isAtstartup = true;
    private bool countdown = false;
    private bool testStart = false;

    private int number;
    private int lastNumber = 0;
    private int numberM;
    private int testRecord = 0;
    private int buttonRecord = 0;

    private float nextTime = 0f;
   // private float testStartTime = 0f;
    private int aim;
    private float rate;
    private static int trialNumber = 0;
    private float everStartTime = 0f;
    private static int line = 0;
    private static string path;
    private string[] plan;
    private string[] plan1 = { "w", "a", "s", "d" };
 

    [SerializeField]
    public Text theTextInformation;

    [SerializeField]
    public Text theTextInformation1;

    public class Actionmsg : MessageBase
    {
        public float aimRate;
        public bool letterChange;
        public int buttonRecords;
        public int state;
    }

    void Start()
    {
        ChoosePlan();
        path = Application.dataPath + "/Task3_Log"+ System.DateTime.Now.Hour.ToString()+"_"+ System.DateTime.Now.Minute.ToString()+ ".txt";
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
                theTextInformation.text = " ";
            }
        }

        if (testStart)
        {
            if (Time.time > nextTime)
            {
                GenerateTest();//改变按键
            }

            if (Input.GetKeyDown(theTextInformation1.text))
            {
                bool changeSign;
                buttonRecord++;
                if (buttonRecord == 1)
                {
                    changeSign = true;
                    Reaction((Time.time - everStartTime).ToString("0.00"));
                }
                else
                {
                    changeSign = false;
                }

                rate = buttonRecord / (Time.time - everStartTime);
                Sending(aim, changeSign, buttonRecord, 1);
                Myprint(theTextInformation1.text, aim.ToString(), rate.ToString("0.00"));//log,反应时间可以由按键第一行的时间减去everStartTime得出。
            }
        }
    }

    void GenerateTest()
    {
        //generate the test button and target rate
        number = UnityEngine.Random.Range(1, 500);
        Random ro = new Random(number);
        number = ro.Next(1, 5);
        if (number == lastNumber || number == 5)
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
        aim = UnityEngine.Random.Range(2, 7);
        theTextInformation1.text = ReturnStr(number);
        Sending(aim, true, 0, 1);
        nextTime = Time.time + 10f;
        buttonRecord = 0;
        everStartTime = Time.time;
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

    void Sending(float theInfor, bool theChange, int theRecord, int theState)
    {
        Actionmsg msg = new Actionmsg();
        msg.aimRate = theInfor;
        msg.letterChange = theChange;
        msg.buttonRecords = theRecord;
        msg.state = theState;
        client.Send(RegisterHotsMsgId, msg);
    }

    private void OnGUI()
    {
        if (isAtstartup)
        {
            Serverip = GUILayout.TextField(Serverip);
            int.TryParse(GUILayout.TextField(Serverport.ToString()), out Serverport);

            if (GUILayout.Button("Connet"))
            {
                trialNumber++;
                client = new NetworkClient();
                client.RegisterHandler(MsgType.Connect, OnConnected);
                client.Connect(Serverip, Serverport);
                PrintNewTrial(trialNumber.ToString());
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

    void ChoosePlan()
    {
        plan = plan1;
    }

    void EndingTest()
    {
        for (int i = 0; i < 5; i++)
        {
            Sending(0f, false, 0, 0);
        }
        SceneManager.LoadScene(0);
    }

    public static void Myprint(string info, string info1, string info2)
    {
        line++;
        StreamWriter sw;
        sw = new StreamWriter(path, true);
        sw.WriteLine(line + "\t"+ info+ "\t" + info1 + "\t" + info2 + "\t" +Time.time);
        sw.Flush();
        sw.Close();
    }

    public static void PrintNewTrial(string info)
    {
        StreamWriter sw;
        sw = new StreamWriter(path, true);
        string fileTitle = "Date Time for Trial " + info + " : " + System.DateTime.Now.ToString();
        sw.WriteLine(fileTitle);
        string lineInfo = "line"+ "\t" + "Button" + "\t" + "Aim" + "\t" + "Rate" + "\t" + "Time ";
        sw.WriteLine(lineInfo);
        sw.Flush();
        sw.Close();
    }

    public static void Reaction(string info)
    {
        StreamWriter sw;
        sw = new StreamWriter(path, true);
        string lineInfo = "Reaction time :" + info;
        sw.WriteLine(lineInfo);
        sw.Flush();
        sw.Close();
    }
}
