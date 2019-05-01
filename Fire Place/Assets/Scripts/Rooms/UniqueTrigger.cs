using Anathema.Saving;
using UnityEngine;
using Anathema.SceneLoading;

namespace Anathema.Rooms
{
    /// <summary>
    /// Base for Door and SaveStations
    /// Handles ignoring collision when spawning on top of trigger.
    /// Requires a Trigger Collider on the same GameObject.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class UniqueTrigger : UniqueComponent
    {
        private const float FilterTime = 0.05f;
        /// <summary>
        /// For ignoring the first trigger enter when player spawns on trigger
        /// </summary>
        public bool IgnoreNextCollision { get; set; }
        
        private bool FilterNextCollision
        {
            get
            {
                return Time.realtimeSinceStartup - lastTriggerExitTime < FilterTime;
                
            }
        }

        private bool sceneFinishedLoading = false;
        private float lastTriggerExitTime = 0;
        
        protected override void Awake()
        {
            base.Awake();

            Collider myCollider = GetComponent<Collider>();
            if (!myCollider) Debug.LogWarning($"{gameObject.name}: UniqueTrigger requires a Collider component.");
            else if (!myCollider.isTrigger) Debug.LogWarning($"{gameObject.name}: UniqueTrigger  requires that the Collider is a trigger.");
        }

        protected virtual void OnEnable()
        {
            SceneLoader.OnSceneLoaded += SceneLoadHandler;
        }

        private void SceneLoadHandler(UniqueID destination, GameData gameData)
        {
            if (destination.Equals(this.UniqueID))
            {
                IgnoreNextCollision = true;
            }
            Debug.Log($"{gameObject.name}: {UniqueID}: {nameof(UniqueTrigger)}: SceneFinishedLoading = true");
            sceneFinishedLoading = true;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if ((SceneLoader.runningWithoutSceneLoader || sceneFinishedLoading) && other.CompareTag("Player"))
            {
                if (!FilterNextCollision)
                {
                    Debug.Log($"{gameObject.name}: {UniqueID}: {nameof(UniqueTrigger)}: Collision");
                    if (IgnoreNextCollision)
                    {
                        Debug.Log($"{gameObject.name}: {UniqueID}: {nameof(UniqueTrigger)}: IgnoringCollision");
                        IgnoreNextCollision = false;
                    }
                    else
                    {
                        Debug.Log($"{gameObject.name}: {UniqueID}: {nameof(UniqueTrigger)}: Actitvating");
                        OnTriggerActivate(other);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (sceneFinishedLoading && other.CompareTag("Player"))
            {
                Debug.Log($"{gameObject.name}: {UniqueID}: {nameof(UniqueTrigger)}: TriggerExit");

                lastTriggerExitTime = Time.realtimeSinceStartup;
            }
        }

        protected abstract void OnTriggerActivate(Collider collider);

        protected virtual void OnDisable()
        {
            SceneLoader.OnSceneLoaded -= SceneLoadHandler;
        }
    }
}