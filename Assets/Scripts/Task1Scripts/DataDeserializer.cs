using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class DataDeserializer : MonoBehaviour
{
   public ClientDataModel clientDataModel;
    public static DataDeserializer instance;
    public event Action DataLoaded;

    private void Awake()
    { 
            instance = this;
            clientDataModel = new ClientDataModel();
    }
    public void Start()
    { 
        IClientService clientService = new JsonClientService("https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data");
        StartCoroutine(clientService.GetAllClientData((clients, details) =>
        {
            clientDataModel.allClients = clients;
            clientDataModel.clientDetails = details;
            DataLoaded?.Invoke();
        }));
    }
}

public class ClientDataModel
{
    public List<Client> allClients;
    public Dictionary<string, ClientDetails> clientDetails;
}