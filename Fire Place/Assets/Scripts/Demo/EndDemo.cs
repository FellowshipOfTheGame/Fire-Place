using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndDemo : MonoBehaviour
{

    public Image panel;
    public Text text;
    public Image logo;

    public float panelFade;
    public float textAppear;

    public float logoFade;

    public float logoAppear;

    public static EndDemo instance;

    void Start()
    {
        instance = this;
    }

# if UNITY_EDITOR
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.F6)) End();

    }

# endif

    public void End() {

        StartCoroutine(EndDemoRoutine());

    }

    IEnumerator EndDemoRoutine() {

        while(panel.color.a < 1) {

            Color p_color = panel.color;
            Color t_color = text.color;

            p_color.a += Time.fixedDeltaTime / panelFade;
            t_color.a += Time.fixedDeltaTime / panelFade;

            panel.color = p_color;
            text.color = t_color;

            yield return new WaitForFixedUpdate();

        }

        float time = 0;
        while(time < textAppear) {

            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();

        }

        while(text.color.a > 0) {

            Color t_color = text.color;
            t_color.a -= 2 * Time.fixedDeltaTime / panelFade;
            text.color = t_color;

            yield return new WaitForFixedUpdate();

        }

        Debug.Log("hey");

        while(logo.color.a < 1) {

            Color l_color = logo.color;

            l_color.a += Time.fixedDeltaTime / panelFade;

            logo.color = l_color;

            yield return new WaitForFixedUpdate();

        }

        time = 0;
        while(time < logoAppear) {

            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();

        }

        while(logo.color.a > 0) {

            Color l_color = logo.color;

            l_color.a -= 2 * Time.fixedDeltaTime / panelFade;

            logo.color = l_color;

            yield return new WaitForFixedUpdate();

        }

        time = 0;
        while(time < 1) {

            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();

        }

        SceneManager.LoadScene("StartGame");

    }

}
