using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject roomButtonPrefab;
    public Transform roomListParent;
    public Button roomCreateButton;
    public string gameSceneName;
    private List<GameObject> roomButtons = new List<GameObject>();

    public void Start()
    {
        roomCreateButton.onClick.AddListener(CreateRoom);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    void UpdateRoomList(List<RoomInfo> roomList)
    {
        // ���� ��ư ����
        foreach (var button in roomButtons)
        {
            Destroy(button);
        }
        roomButtons.Clear();

        // ���ο� �� ��� UI ����
        foreach (var room in roomList)
        {
            GameObject newButton = Instantiate(roomButtonPrefab, roomListParent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{room.Name} ({room.PlayerCount}/{room.MaxPlayers})";
            newButton.GetComponent<Button>().onClick.AddListener(() => JoinRoom(room.Name));

            roomButtons.Add(newButton);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(gameSceneName);
        Debug.Log($"�� ����: {PhotonNetwork.CurrentRoom.Name}");
    }

    public void CreateRoom()
    {
        string roomName = "Room_" + Random.Range(1, 9999);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
}
