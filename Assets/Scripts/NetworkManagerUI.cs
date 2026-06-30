using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake()
    {
        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            Debug.Log("Server Start");
        });
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            Debug.Log("Host Start");
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            Debug.Log("Client Join");
        });
    }
}