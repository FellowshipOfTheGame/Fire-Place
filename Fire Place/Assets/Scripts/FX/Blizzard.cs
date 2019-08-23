using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Experimental.VFX;

using FirePlace.Util;

namespace FirePlace.FX 
{
    public class Blizzard : MonoBehaviour
    {

        public static Blizzard instance;
        
        public float defaultIntensity = 1000;

        private VisualEffect blizzard;
        private GameObject cameraObject;

        public float intensityChangeTime = 10f;        
        private Coroutine intensityChange = null;

        public float Intensity
        {
            get => blizzard.GetFloat(nameof(Intensity));
            set
            {
                if (value < 0f || value > 15000f) throw new ArgumentOutOfRangeException();
                
                blizzard.SetFloat(nameof(Intensity), value);
            }
        }
        [SerializeField] private Transform follow = null;
        private Vector3 relativeDistance = Vector3.zero;

        private void Awake ()
        {

            if(instance != null)
				Debug.Log("Blizzard.Awake: More than one Blizzard instance found!");
			else
				instance = this;

            blizzard = GetComponent<VisualEffect>();
            Intensity = defaultIntensity;

            if(follow != null)
                relativeDistance = transform.position - follow.position;

        }

        private void Update() 
        {

            if(follow != null)
                transform.position = follow.position + relativeDistance;

        }

        public void ChangeIntensity(float intensity)
        {

            if(intensity < 0f || intensity > 15000f)
            {
                Debug.LogWarning("Blizzard.TargetIntensity: Intensity out of bounds! Clamping value...");
                intensity = Mathf.Clamp(intensity, 0f, 15000f);
            }

            if(intensityChange != null)
                StopCoroutine(intensityChange);

            intensityChange = StartCoroutine(ChangingIntensity(intensity));

        }

        private IEnumerator ChangingIntensity(float intensity)
        {

            float cur = Intensity;
            float delta = intensity - cur;

            while(Mathf.Abs(intensity - cur) > 1)
            {

                Intensity += Time.fixedDeltaTime * delta / intensityChangeTime;                    
                cur = Intensity;

                yield return new WaitForFixedUpdate();

            }

        }
    }
}