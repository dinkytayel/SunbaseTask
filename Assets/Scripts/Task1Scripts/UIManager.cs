using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{ 
    public TMP_Text nameLabel;
    public TMP_Text pointsLabel;
    public TMP_Text addressLabel;
    public TMP_Dropdown filterDropdown;
    public Transform clientsList;
    public GameObject clientPrefab;
    public GameObject popup;

   void Start()
    {
        DataDeserializer.instance.DataLoaded += PopulateData;
    }

    void PopulateData()
    {
            PopulateDropdown();
            PopulateClientsList(DataDeserializer.instance.clientDataModel.allClients);
            filterDropdown.onValueChanged.AddListener(delegate
            {
                FilterClients();
            });
    }

        void PopulateDropdown()
        {
            filterDropdown.ClearOptions();

            List<string> filterOptions = new List<string> { "All Clients", "Managers only", "Non-managers" };
            filterDropdown.AddOptions(filterOptions);
        }

        void PopulateClientsList(List<Client> allClients)
        {
            foreach (Transform child in clientsList)
            {
                Destroy(child.gameObject);
            }

            foreach (Client client in allClients)
            {
                GameObject clientObject = Instantiate(clientPrefab,clientsList);
                clientObject.transform.GetChild(0).GetComponent<TMP_Text>().text = client.label;
                Button button = clientObject.GetComponent<Button>();
                button.onClick.AddListener(() => ShowClientDetails(client.id.ToString()));
            }
        }

        void ShowClientDetails(string clientId)
        {
            if (DataDeserializer.instance.clientDataModel.clientDetails.TryGetValue(clientId, out ClientDetails details))
            {
                nameLabel.text = details.name;
                pointsLabel.text = "Points: " + details.points.ToString();
                addressLabel.text = "Address: " + details.address;
            OpenPopupWithAnimation();
            }
        }

        public void FilterClients()
        {
        string selectedOption = filterDropdown.options[filterDropdown.value].text;

        List<Client> filteredClients;

        switch (selectedOption)
        {
            case "Managers only":
                filteredClients = DataDeserializer.instance.clientDataModel.allClients.FindAll(client => client.isManager);
                break;

            case "Non-managers":
                filteredClients = DataDeserializer.instance.clientDataModel.allClients.FindAll(client => !client.isManager);
                break;

            default:
                filteredClients = DataDeserializer.instance.clientDataModel.allClients;
                break;
        }

        PopulateClientsList(filteredClients);
    }
    void OpenPopupWithAnimation()
    {
        popup.transform.localScale = Vector3.zero;
        popup.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBack) 
            .OnStart(() =>
            { 
                popup.SetActive(true);
            })
            .OnComplete(() =>
            {
            });
    }
}
