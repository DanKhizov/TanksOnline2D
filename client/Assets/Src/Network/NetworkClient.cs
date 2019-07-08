using SocketIO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkClient : SocketIOComponent
{
    [Header("Network Client")]
    [SerializeField]
    private Transform networkContainer;
    [SerializeField]
    private GameObject playerPrefab;

    public static string ClientId { get; private set; }

    private Dictionary<string, NetworkIdentity> serverObjects;

    public override void Start()
    {
        base.Start();
        Initialize();
        SetupEvents();
    }

    public override void Update()
    {
        base.Update();
    }

    private void Initialize()
    {
        serverObjects = new Dictionary<string, NetworkIdentity>();
    }

    private void SetupEvents()
    {
        On("open", (E) => {
            Debug.Log("Connection made");
        });

        On("register", (E) =>
        {
            ClientId = E.data["id"].ToString();

            Debug.LogFormat("Client id ({0})", ClientId);
        });

        On("spawn", (E) =>
        {
            string id = E.data["id"].ToString();

            GameObject player = Instantiate(playerPrefab, networkContainer);
            player.name = string.Format("Player ({0})", id);

            NetworkIdentity ni = player.GetComponent<NetworkIdentity>();
            ni.SetControllerId(id);
            ni.SetSocketReference(this);
            serverObjects.Add(id, ni);
        });

        On("disconnected", (E) =>
        {
            string id = E.data["id"].ToString();

            GameObject player = serverObjects[id].gameObject;
            Destroy(player);
            serverObjects.Remove(id);
        });

        On("updatePosition", (E) =>
        {
            Debug.Log("position");
            string id = E.data["id"].ToString();
            float x = E.data["position"]["x"].f;
            float y = E.data["position"]["y"].f;

            NetworkIdentity ni = serverObjects[id];
            ni.transform.position = new Vector3(x, y, 0);
        });
    }

    [Serializable]
    public class Player
    {
        public string id;
        public Position position;
    }

    [Serializable]
    public class Position
    {
        public float x;
        public float y;
    }
}
