using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalCutSceneScript : MonoBehaviour
{
	public enum State { None, Middle, Final };

	public GameObject player, sceneStart, sceneEnd;
	private State state;

	public GameObject blackPlanel;

	public GameObject[] finalTexts;

	private float totalDist, dist, alpha, secondsBetweenTexts = 5;

    // Start is called before the first frame update
    void Start()
    {
		state = State.None;

		/*player = GameObject.FindGameObjectWithTag("Player");

		sceneStart = GameObject.FindGameObjectWithTag("StartTrigger");
		sceneEnd = GameObject.FindGameObjectWithTag("EndTrigger");*/
	}

    // Update is called once per frame
    void Update()
    {
		if (sceneStart == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");

			sceneStart = GameObject.FindGameObjectWithTag("StartTrigger");
			sceneEnd = GameObject.FindGameObjectWithTag("EndTrigger");

			totalDist = Vector3.Magnitude(sceneStart.transform.position - sceneEnd.transform.position);
			totalDist *= 1.1f;
		}

		if (state == State.Middle)
		{
			//Debug.Log("totalDist = " + totalDist);
			//Debug.Log("dist = " + dist);
			//Debug.Log("alpha = " + alpha);

			dist = Vector3.Magnitude(player.transform.position - sceneEnd.transform.position);

			alpha = 1 - dist / totalDist;

			blackPlanel.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
		}
    }

	public void SetStart()
	{
		state = State.Middle;
	}

	public void SetEnd()
	{
		state = State.Final;
		StartCoroutine(FinalCredits());
	}

	private IEnumerator FinalCredits()
	{
		//Debug.Log("Hey 1");
		player.GetComponent<PlayerBehavior>().enabled = false;
		//Debug.Log("Hey 2");
		blackPlanel.GetComponent<Image>().color = new Color(0, 0, 0, 1);
		//Debug.Log("Hey 3");
		//Debug.Log("color = " + blackPlanel.GetComponent<Image>().color);
		for (int i = 0; i < finalTexts.Length; i++)
		{
			//Debug.Log("Hey 4");
			StartCoroutine(ChangeAlpha(finalTexts[i], 1, 0.5f));
			//Debug.Log("Hey 5");
			yield return new WaitForSeconds(secondsBetweenTexts+1);
			//Debug.Log("Hey 6");
			StartCoroutine(ChangeAlpha(finalTexts[i], 0, 0.5f));
			//Debug.Log("Hey 7");
		}

		//return null;
	}

	private IEnumerator ChangeAlpha(GameObject target, float finalAlpha, float time)
	{
		float alphaTol = 0.05f;
		float iniAlpha = target.GetComponent<Text>().color.a;
		float totalAlpha = Mathf.Abs(iniAlpha - finalAlpha);

		if(finalAlpha > iniAlpha)
		{
			while (finalAlpha - target.GetComponent<Text>().color.a > alphaTol)
			{
				Debug.Log("Loop called");
				target.GetComponent<Text>().color += new Color(0, 0, 0, (totalAlpha * Time.deltaTime) / time);
				Debug.Log("Target name = " + target.name);
				yield return new WaitForFixedUpdate();
			}
		}
		else
		{
			while (target.GetComponent<Text>().color.a - finalAlpha > alphaTol)
			{
				target.GetComponent<Text>().color -= new Color(0, 0, 0, (totalAlpha * Time.deltaTime) / time);
				yield return new WaitForFixedUpdate();
			}
		}

		target.GetComponent<Text>().color = new Color(target.GetComponent<Text>().color.r, target.GetComponent<Text>().color.g, target.GetComponent<Text>().color.b, finalAlpha);
	}
}
