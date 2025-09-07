using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private bool isFollowing = false;

    private void OnEnable()
    {
        EventDefine.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        EventDefine.OnPlayerSpawned -= OnPlayerSpawned;
    }

    private void OnPlayerSpawned(GameObject player)
    {
        // 只处理本地玩家
        PhotonView photonView = player.GetComponent<PhotonView>();
        if (photonView != null && photonView.IsMine && !isFollowing)
        {
            Setup2DCamera(player.transform);
            isFollowing = true;
        }
    }

    private void Setup2DCamera(Transform target)
    {
        if (virtualCamera == null)
        {
            Debug.LogError("请分配CinemachineVirtualCamera组件");
            return;
        }
        virtualCamera.Follow = target;
    }
}
