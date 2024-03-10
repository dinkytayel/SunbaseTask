using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
public interface IClientService
{
    IEnumerator GetAllClientData(Action<List<Client>, Dictionary<string, ClientDetails>> callback);
}
public class JsonClientService : IClientService
{
    private readonly string apiUrl;

    public JsonClientService(string apiUrl)
    {
        this.apiUrl = apiUrl;
    }

    public IEnumerator GetAllClientData(Action<List<Client>, Dictionary<string, ClientDetails>> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string json = webRequest.downloadHandler.text;

                try
                {
                    ClientData clientData = JsonConvert.DeserializeObject<ClientData>(json);
                    callback?.Invoke(clientData.clients, clientData.data);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error parsing JSON: {ex.Message}");
                    callback?.Invoke(new List<Client>(), new Dictionary<string, ClientDetails>());
                }
            }
            else
            {
                Debug.LogError($"Error fetching data from the API: {webRequest.error}");
                callback?.Invoke(new List<Client>(), new Dictionary<string, ClientDetails>());
            }
        }
    }
}



