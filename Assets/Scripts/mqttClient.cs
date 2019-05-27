using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System;
using SimpleJSON;

public class mqttClient : MonoBehaviour {
	private MqttClient client;
	// Use this for initialization
	private JSONNode jsonNode;
	private float x = 0;
	private float y = 0;
	private float z = 0;
    private float x2 = 0;
    private float y2 = 0;
    private float z2 = 0;

    private Vector3 playerPosition;
    private Vector3 player2Position;
    GameObject Player2;

    public int division = 100;
    public string mainPlayer = "dwm/node/4da4/uplink/location";
    public string secondPlayer = "dwm/node/9129/uplink/location";

    void Start () {
        Player2 = GameObject.Find("Player2");
        // create client instance

        // phone instance
        client = new MqttClient(IPAddress.Parse("192.168.43.8"),1883 , false , null );

        // client = new MqttClient(IPAddress.Parse("192.168.8.100"),1883 , false , null );


		// register to message received
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

		string clientId = Guid.NewGuid().ToString();
		string username = "dwmuser";
		string password = "dwmpass";
		client.Connect(clientId, username, password);

		// subscribe to the topic "/home/temperature" with QoS 2
		client.Subscribe(new string[] { mainPlayer, secondPlayer }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

	}
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
	{
        string toping = e.Topic;
        string message = System.Text.Encoding.UTF8.GetString(e.Message);
        string json = JsonUtility.ToJson(message);
        jsonNode = SimpleJSON.JSON.Parse(message);

        if (toping == mainPlayer)
        {
            x = float.Parse(jsonNode["position"]["x"]);
            y = float.Parse(jsonNode["position"]["y"]);
            z = float.Parse(jsonNode["position"]["z"]);
        }
        if (toping == secondPlayer)
        {
            x2 = float.Parse(jsonNode["position"]["x"]);
            y2 = float.Parse(jsonNode["position"]["y"]);
            z2 = float.Parse(jsonNode["position"]["z"]);
        }
    }
	void transformPlayer() {
		if (!float.IsNaN(x) && !float.IsNaN(y) && !float.IsNaN(z))
		{
			playerPosition = new Vector3(x / division, z/division, y/division);
			transform.position = playerPosition;
		}
        if (!float.IsNaN(x2) && !float.IsNaN(y2) && !float.IsNaN(z2))
        {
            player2Position = new Vector3(x2 / division, z2 / division, y2 / division);
            Player2.transform.position = player2Position;
        }

    }
	// Update is called once per frame
	void Update () {
        transformPlayer();
	}
}
