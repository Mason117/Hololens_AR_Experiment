using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Networktoholo : MonoBehaviour
{

    public string connetionIP = "127.0.0.1";
    public int PorNumber = 8808;
    public bool conneted = false;
    private int playerCount = 0;

    private int number;
    private int numberM;
    private bool countdown = false;

    [SerializeField]
    public Text theTextProgress;

    // Use this for initialization
    void Start () {
        theTextProgress.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (countdown == true)
        {
            theTextProgress.enabled = true;
            int number1M = System.DateTime.Now.Minute;
            int number1 = System.DateTime.Now.Second;
            if (number1M != numberM)
            {
               number1 += 60;
            }
            theTextProgress.text = "Game start in " + (number - number1)+ " seconds.";
            //Debug.Log("Game start in " + (number - number1)+" seconds.");
            if (number1-number==0)
            {
                ReLunchGame();
            }
        }
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player " + playerCount++ + " connected from " + player.ipAddress + ":" + player.port);
        countdown = true;
        numberM= System.DateTime.Now.Minute;
        number = System.DateTime.Now.Second+4;
    }


    private void OnConnectedToServer()
    {
        Debug.Log("Connected to server");
        Debug.Log(connetionIP);
        conneted = true;
    }

    private void OnServerInitialized()
    {
        Debug.Log("Server initialized and ready");
        Debug.Log(connetionIP);
        conneted = true;
    }

    private void OnDisconnectedFromServer()
    {
        conneted = false;
    }

    private void OnGUI()
    {
        if (!conneted)
        {
            connetionIP = GUILayout.TextField(connetionIP);
            int.TryParse(GUILayout.TextField(PorNumber.ToString()), out PorNumber);//这个后面要好好查查
            if (GUILayout.Button("Connet"))
            {
                Network.Connect(connetionIP, PorNumber);
            }
            if (GUILayout.Button("Host"))
            {
                //Network.InitializeServer(4, PorNumber, true);
                NetworkServer.Listen(8808);

            }
        }
        else
        {
            GUILayout.Label("Connetions:"+Network.connections.Length.ToString());
        }
    }

    void ReLunchGame()
    {
        //System.Threading.Thread.Sleep(5000);
        Time.timeScale = 1;
        GameControl.gameStopped = false;
        countdown = false;
        theTextProgress.enabled = false;
    }
}
