using Cinemachine;
using Fireplace.Util;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class SetupToFollowPlayer : MonoBehaviour
{
    [SerializeField] private string PlayerSceneName = "Player";
    [SerializeField] private string PlayerTag = "Player";
    private CinemachineVirtualCamera virtualCamera;
    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (!virtualCamera)
        {
            Debug.LogWarning($"{nameof(SetupToFollowPlayer)}: {name}: Requires component of type: {nameof(CinemachineVirtualCamera)}");
        }
        
        GameObject player = Finder.FindRootObject(PlayerSceneName, PlayerTag);
        if (player)
        {
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;
        }
    }
}