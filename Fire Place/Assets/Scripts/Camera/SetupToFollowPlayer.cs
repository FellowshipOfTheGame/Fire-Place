using Cinemachine;

using UnityEngine;

using FirePlace.Util;

namespace FirePlace.Camera
{

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

                Transform followPoint = player.GetComponent<PlayerBehaviour>().cameraFollowPoint;

                virtualCamera.Follow = followPoint;
                virtualCamera.LookAt = followPoint;
            }
        }
    }
}