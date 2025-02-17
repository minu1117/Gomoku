using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI ConnectionStatus;
    public TextMeshProUGUI ID_text;
    public Button connetBtn;
    public string lobbySceneName;

    public void Awake()
    {
        connetBtn.onClick.AddListener(Connect);
    }

    public void Update()
    {
        ConnectionStatus.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 접속 완료");
        PhotonNetwork.LocalPlayer.NickName = ID_text.text;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene(lobbySceneName);
        Debug.Log("로비 접속 완료");
    }
}