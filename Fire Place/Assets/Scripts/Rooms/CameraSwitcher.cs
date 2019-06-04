using Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Anathema.Rooms
{
    [RequireComponent(typeof(Collider))]
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                virtualCamera.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                virtualCamera.enabled = false;
            }
        }
    }
}