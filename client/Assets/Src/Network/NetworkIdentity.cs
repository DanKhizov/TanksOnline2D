using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Utility.Attributes;
using SocketIO;

public class NetworkIdentity : MonoBehaviour
{
    [Header("Helpful values")]
    [SerializeField]
    [GreyOut]
    private string id;
    [SerializeField]
    private bool isControlling;

    private SocketIOComponent socket;

    public void Awake()
    {
        isControlling = false;
    }

    public void SetControllerId(string ID)
    {
        id = ID;
        isControlling = (NetworkClient.ClientId == id) ? true : false;
    }

    public void SetSocketReference(SocketIOComponent Socket)
    {
        socket = Socket;
    }

    public string GetID()
    {
        return id;
    }

    public bool IsControlling()
    {
        return isControlling;
    }

    public SocketIOComponent GetSocket()
    {
        return socket;
    }
}
