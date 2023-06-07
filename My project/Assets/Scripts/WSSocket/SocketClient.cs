using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;               //C# 에서 웹 소켓을 지원하는 라이브러리
using System.Text;
using Newtonsoft.Json;              //JSON 을 사용하기위한 라이브러리 

public class MyData
{
    public string clientID;                     //서버에서 제작해서 클라이언트에 접속시 줌
    public string message;
    public int requestType;                     // 요청번호 json으로 보냄
}

public class SocketClient : MonoBehaviour
{
    private WebSocket webSocket;
    private bool isConnected = false;
    private int connectionAttempt = 0;              // 연결 시도 횟수 
    private const int maxConnectionAttempts = 3;    // 최대 연결 시도 횟수

    MyData sendData = new MyData { message = "메세지 전송" };

    void Start()
    {
        ConnectWebSocekt();
    }

    void ConnectWebSocekt()
    {
        webSocket = new WebSocket("ws://localhost:8000");           //localhost 127.0.0.1 port : 8000, ws => websocket
        webSocket.OnOpen += OnWebSocketOpen;                        //웹 소켓이 연결 되었을 때 이벤트를 발생시켜서 함수를 실행 시킨다. 
        webSocket.OnMessage += OnWebSocketMessage;                  //웹 소켓 메세지가 왔을 때 이벤트를 발생시켜 Message 함수를 실행 시킨다.
        webSocket.OnClose += OnWebSocketClose;                      //웹 소켓 연결이 끊어졌을 때 이벤트를 발생시켜 Close 함수를 실행 시킨다. 

        webSocket.ConnectAsync();
    }

    void OnWebSocketOpen(object sender, System.EventArgs e)         //웹 소켓이 오픈되고 연결 되었을 때 
    {
        Debug.Log("WebSocket connected");
        isConnected = true;
        connectionAttempt = 0;
    }

    void OnWebSocketMessage(object sender, MessageEventArgs e)      //웹 소켓이 연결된후 Message가 왔을 때 
    {
        string jsonData = Encoding.Default.GetString(e.RawData);    //MessageEventArgs에 들어온 RawData를 Json으로 인코딩 한다. 
        Debug.Log("Received JSON data : " + jsonData);

        MyData receivedData = JsonConvert.DeserializeObject<MyData>(jsonData);          //JSON 데이터를 객체로 역직렬화

        if (receivedData != null && !string.IsNullOrEmpty(receivedData.clientID))        //receivedData 값이 비어 있지 않을 때
        {
            sendData.clientID = receivedData.clientID;                                  //서버에서 받아온 ID 값을 MyData에 넣는다. 
        }

    }

    void OnWebSocketClose(object sender, CloseEventArgs e)              //웹 소켓 연결이 끊겼을 
    {
        Debug.Log("WebSocket connection closed");
        isConnected = false;                                            //연결 끈김 flag 

        if (connectionAttempt < maxConnectionAttempts)                   //총 3번의 시도 
        {
            connectionAttempt++;
            Debug.Log("Attempting to reconnect. Attempt : " + connectionAttempt);
            ConnectWebSocekt();                                                         //Connect 시도를 한다.
        }
        else
        {
            Debug.Log("Failed to connect ");
        }
    }

    void OnApplicationQuit()                        //프로그램 종료시에 호출 되는 함수 
    {
        DisconnectWebSocket();
    }

    void DisconnectWebSocket()                      //연결된 socket을 Relese 해준다. 
    {
        if (webSocket != null && isConnected)
        {
            webSocket.Close();
            isConnected = false;
        }
    }

    void Update()
    {
        if (webSocket == null || !isConnected)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sendData.requestType = 0;
            string jsonData = JsonConvert.SerializeObject(sendData);            //Mydata를 Json으로 만들어줌
            webSocket.Send(jsonData);
        }
    }
}