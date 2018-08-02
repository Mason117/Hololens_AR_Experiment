using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NewNetWorkC : MonoBehaviour {


    public GameObject Dno;
    public GameObject CheckBar;
    //电脑客户端！！！！！
    public NetworkClient client;
    public string Serverip = "192.168.12.203";  //hololens ip address
    public int Serverport = 8808;
    public const short RegisterHotsMsgId = 1005;

    private bool isAtstartup = true;
    private bool countdown = false;
    private int number;
    private int numberM;

    [SerializeField]
    public Text theTextProgress;

    public class Actionmsg : MessageBase
    {
        public int infor1;
        public int infor2;

    }

    void Start()
    {
        theTextProgress.enabled = false;
       // Setupclient();   
    }

    // Update is called once per frame
    void Update()
    {
        //到底是一次性发还是连续发信息，现在还没有想好

        if (countdown == true)
        {
            theTextProgress.enabled = true;
            int number1M = System.DateTime.Now.Minute;
            int number1 = System.DateTime.Now.Second;
            if (number1M != numberM)
            {
                number1 += 60;
            }
            theTextProgress.text = "Game start in " + (number - number1) + " seconds.";
            if (number1 - number == 0)
            {
                ReLunchGame();             
            }
        }
    }

    void ReLunchGame()
    {
        Time.timeScale = 1;
        GameControl.gameStopped = false;//另外一个脚本的参数
        countdown = false;
        theTextProgress.enabled = false;
        //Sending("test test!!!");
    }

    void Sending(int theinfor1, int theinfor2)
    {
        Actionmsg msg = new Actionmsg();
        msg.infor1 = theinfor1;
        msg.infor2 = theinfor2;
        client.Send(RegisterHotsMsgId, msg);
        Debug.Log("Sending...");
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

    void OnTriggerEnter2D(Collider2D col)
    {
        int keyNumber,patternNumber;
        CheckBar.GetComponent<CheckPoint>().Checknumber++;

        if (col.gameObject.name.Equals("rock(Clone)"))
        {
            patternNumber = Dno.GetComponent<DinoControl>().number;
            keyNumber = Dno.GetComponent<DinoControl>().number1;
            Sending(patternNumber, keyNumber);
        }

        if (col.gameObject.name.Equals("roadBlock(Clone)"))
        {
            patternNumber = Dno.GetComponent<DinoControl>().number;
            keyNumber = Dno.GetComponent<DinoControl>().number1;
            Sending(patternNumber, keyNumber);
        }

        if (col.gameObject.name.Equals("brid(Clone)"))
        {
            patternNumber = Dno.GetComponent<DinoControl>().number;
            keyNumber = Dno.GetComponent<DinoControl>().number2;
            Sending(patternNumber, keyNumber);
        }
    }
}
