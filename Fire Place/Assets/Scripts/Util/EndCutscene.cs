using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using FirePlace.Util;

namespace FirePlace.Util
{

    public class EndCutscene : MonoBehaviour
    {
        
        [SerializeField] private Image blackScreen = null;
        [SerializeField] private float fadeOutTime = 3.0f;
        
        [SerializeField] private string[] endingTexts = null;
        [SerializeField] private Text endingTextComponent = null;
        [SerializeField] private float textFadeTime = 2.0f;
        [SerializeField] private float textReadTime = 4.0f;

        [SerializeField] private Text[] creditsText = null;
        [SerializeField] private float creditsTime = 6.0f;

        [SerializeField] private Image loadingScreen = null;
        [SerializeField] private Text loadingText = null;

        [SerializeField] private string targetScene = "StartGame";

        private bool startedEnding;

        private void Start()
        {

            startedEnding = false;

        }

        private void FixedUpdate()
        {

            if(PlayerBehaviour.instance != null && PlayerBehaviour.instance.getState() == PlayerBehaviour.States.End && !startedEnding)
            {

                startedEnding = true;
                StartCoroutine(EndingCutscene());

            }

        }

        private IEnumerator EndingCutscene()
        {

            float time = 0;

            Color blackScreenColor = blackScreen.color;
            while (blackScreenColor.a < 1)
            {

                blackScreenColor = blackScreen.color;
                blackScreenColor.a += Time.deltaTime / fadeOutTime;
                blackScreen.color = blackScreenColor;

                yield return new WaitForEndOfFrame();

            }

            Color endingTextColor = endingTextComponent.color;
            for(int i = 0; i < endingTexts.Length; i++) {

                endingTextComponent.text = endingTexts[i];

                while(endingTextColor.a < 1)
                {
                    endingTextColor = endingTextComponent.color;
                    endingTextColor.a += Time.deltaTime / textFadeTime;
                    endingTextComponent.color = endingTextColor;

                    yield return new WaitForEndOfFrame();
                }

                time = 0;
                while(time < textReadTime)
                {
                    time += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                while(endingTextColor.a > 0)
                {
                    endingTextColor = endingTextComponent.color;
                    endingTextColor.a -= Time.deltaTime * 2 / textFadeTime;
                    endingTextComponent.color = endingTextColor;

                    yield return new WaitForEndOfFrame();
                }

                time = 0;
                while(time < 0.5f)
                {
                    time += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

            }

            Color loadScreenColor = loadingScreen.color;
            Color creditsTextColor = creditsText[0].color;
            while (loadScreenColor.a < 1)
            {

                loadScreenColor = loadingScreen.color;
                loadScreenColor.a += Time.deltaTime / fadeOutTime;
                loadingScreen.color = loadScreenColor;

                creditsTextColor = creditsText[0].color;
                creditsTextColor.a += Time.deltaTime / fadeOutTime;
                for(int i = 0; i < creditsText.Length; i++)
                    creditsText[i].color = creditsTextColor;

                yield return new WaitForEndOfFrame();

            }

            time = 0;
            while(time < creditsTime)
            {
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            while(creditsTextColor.a > 0)
            {
                creditsTextColor = creditsText[0].color;
                creditsTextColor.a -= Time.deltaTime * 2 / fadeOutTime;
                for(int i = 0; i < creditsText.Length; i++)
                    creditsText[i].color = creditsTextColor;

                yield return new WaitForEndOfFrame();
            }

            for(int i = 0; i < creditsText.Length; i++)
                creditsText[i].enabled = false;

            loadingText.enabled = true;
            SceneManager.LoadScene(targetScene);

        }

    }

}
