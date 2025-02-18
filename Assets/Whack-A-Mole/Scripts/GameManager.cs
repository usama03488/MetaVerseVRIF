using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BNG;

public class GameManager : MonoBehaviour {

	enum State{
		START,
		PLAY,
		GAMEOVER,
	}

	public static float time;
	public float timeLimit = 30;
	const float waitTime = 5;

public	Animator anim;
	public GameObject GameOverText;
	public GameObject PressAText;
	public MoleManager moleManager;
	public Text remainingTIme;
	AudioSource audio;

	State state;
	float timer;

	void Start () 
	{
		Application.targetFrameRate = 60;

		this.state = State.START;
		this.timer = 0;
/*		this.anim = GameObject.Find ("Canvas").GetComponent<Animator> ();
		this.moleManager = GameObject.Find ("GameManager").GetComponent<MoleManager> ();
		this.remainingTIme = GameObject.Find ("RemainingTime").GetComponent<Text>();*/
		this.audio = GetComponent<AudioSource> ();
	}
	
	void Update () 
	{
		if (this.state == State.START) 
		{
			ScoreManager.score = 0;
			GameOverText.SetActive(false);
			PressAText.SetActive(true);
			if (InputBridge.Instance.AButton) 
			{
				this.state = State.PLAY;

				// hide start label
				//this.anim.SetTrigger ("StartTrigger");
				PressAText.SetActive(false);
				anim.gameObject.SetActive(false);
				// start to generate moles
				this.moleManager.StartGenerate ();

				this.audio.Play ();
			}
		}
		else if (this.state == State.PLAY) 
		{	
			this.timer += Time.deltaTime;
			time = this.timer / timeLimit;
				
			if (this.timer > timeLimit) 
			{
				anim.gameObject.SetActive(true);
				this.state = State.GAMEOVER;
				
				// show gameover label
			//	this.anim.SetTrigger ("GameOverTrigger");
				PressAText.SetActive(false);
				GameOverText.SetActive(true);
				// stop to generate moles
				this.moleManager.StopGenerate ();

				this.timer = 0;

				// stop audio
				this.audio.loop = false;
			}

			this.remainingTIme.text = "Time: " + ((int)(timeLimit-timer)).ToString ("D2");
		}
		else if (this.state == State.GAMEOVER) 
		{
			this.timer += Time.deltaTime;

			if (this.timer > waitTime)
			{
				this.state = State.START;

				//SceneManager.LoadScene ( SceneManager.GetActiveScene().name );
			}

			this.remainingTIme.text = "";
		}
	}

	public void ResetGameAfterDisableObject()
    {
		anim.gameObject.SetActive(true);
		this.state = State.GAMEOVER;

		// show gameover label
		//	this.anim.SetTrigger ("GameOverTrigger");
		PressAText.SetActive(false);
		GameOverText.SetActive(true);
		// stop to generate moles
		this.moleManager.StopGenerate();

		this.timer = 0;

		// stop audio
		this.audio.loop = false;
		this.remainingTIme.text = "Time: " + ((int)(timeLimit - timer)).ToString("D2");
		this.state = State.START;
		this.remainingTIme.text = "";

		this.state = State.START;
		this.timer = 0;
		/*		this.anim = GameObject.Find ("Canvas").GetComponent<Animator> ();
				this.moleManager = GameObject.Find ("GameManager").GetComponent<MoleManager> ();
				this.remainingTIme = GameObject.Find ("RemainingTime").GetComponent<Text>();*/
		this.audio = GetComponent<AudioSource>();

	}
    private void OnEnable()
    {
		ResetGameAfterDisableObject();

	}
}
