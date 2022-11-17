using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine.UI;

public class ButtonNetwork : NetworkBehaviour
{
    [SerializeField]Text status,message;
    [SerializeField]Button buttonHost, buttonClient, buttonServer, buttonLeave;
    [SerializeField] InputField name, port, adress;

    private void Start()
    {
        status.text = "Not Connected";
        UpdateUI(false);

        adress.text = PlayerPrefs.GetString("IPadress");
        if (adress.text == "")
           adress.text= ((UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport)
                .ConnectionData.Address;
        port.text = PlayerPrefs.GetString("port");

        if (port.text == "")
            port.text = ((UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport)
                 .ConnectionData.Port.ToString();

        name.text = PlayerPrefs.GetString("PlayName");

        buttonHost.onClick.AddListener(StartHost);
        buttonClient.onClick.AddListener(StartClient);
        buttonServer.onClick.AddListener(StartServer);
        buttonLeave.onClick.AddListener(Leave);
    }
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        UpdateUI(true);
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        UpdateUI(true);
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        UpdateUI(true);
    }

    public void Leave()
    {
        NetworkManager.Singleton.Shutdown();
        UpdateUI(false);
    }

    void UpdateUI(bool isconected)
    {
        if (isconected)
            status.text = "Connected!";
        else
            status.text = "Not Connected";
        buttonHost.gameObject.SetActive(!isconected);
        buttonClient.gameObject.SetActive(!isconected);
        buttonServer.gameObject.SetActive(!isconected);
        name.gameObject.SetActive(!isconected);
        adress.gameObject.SetActive(!isconected);
        port.gameObject.SetActive(!isconected);
    }
}
