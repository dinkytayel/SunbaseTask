using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ClientData
{
    public List<Client> clients;
    public Dictionary<string, ClientDetails> data;
    public string label;
}

[System.Serializable]
public class Client
{
    public bool isManager;
    public int id;
    public string label;
}

[System.Serializable]
public class ClientDetails
{
    public string address;
    public string name;
    public int points;
}