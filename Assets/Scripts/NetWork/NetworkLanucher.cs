using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkLanucher : MonoBehaviourPunCallbacks
{
    public GameObject loginUI;
    public GameObject nameUI;
    public TMP_InputField roomName;
    public TMP_InputField playerName;

    private void Start()
    {
        loginUI.SetActive(false);
        nameUI.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }
    /// <summary>
    /// 点击多人游戏按钮
    /// </summary>
    public void MultipleGameStart()
    {
        nameUI.SetActive(true);

    }
    /// <summary>
    /// 输入用户名
    /// </summary>
    public void EnterUserName()
    {
        nameUI.SetActive(false);
        PhotonNetwork.NickName = playerName.text;//用户名上传到网络
        loginUI.SetActive(true);
    }

    public void JoinOrCreateButton()
    {
        if (roomName.text.Length < 2) return;

        loginUI.SetActive(false);
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, default);

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

}
