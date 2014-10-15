﻿using UnityEngine;
using System.Collections;

public class Player : Entities 
{

	//public enumerator to easily communicate current game state
	public enum GameState { PLAYING, PAUSED };
	public static GameState playerState = GameState.PLAYING;

	//replaced with Entities global speed
	//public float speed;
	public float turnSpeed;
	public static bool paused = false;
	private bool a;
	private bool d;
	
	Animator anim;

	private Vector3 moveDirection;

	void Start () {

		paused = false;
		anim = GetComponent<Animator> ();
	
	}
	
	void Update () 
	{
		////Attempted mouse implementation
		/*Vector3 currentPosition = transform.position;
		Vector3 targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//targetPosition.z = 0;
		if (Input.GetButton("Fire1"))
		{
			Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();
		}
		//while(Input.GetButton ("Fire1"))
		{
			//Vector3 target = moveDirection * moveSpeed + currentPosition;
			//if(!((targetPosition.x).Equals(this.rigidbody2D.position.x) && !(targetPosition.y).Equals(rigidbody2D.position.y)))
			//transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
		}
		//*/

		//consider taking out for less calls

		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
		{
			anim.SetBool("d",true);
			rigidbody2D.transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
		{
			anim.SetBool("a", true);
			anim.SetBool("d", false);
			rigidbody2D.transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) 
		{
			if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.LeftArrow)) 
				anim.SetBool("d",true);
			rigidbody2D.transform.position += Vector3.down * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
		{			
			anim.SetBool("d", true);
			anim.SetBool("a", false);
			rigidbody2D.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//if not moving
		if(!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.UpArrow) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.DownArrow) && !Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.RightArrow))
		{
			anim.SetBool("d",false);
			anim.SetBool("a", false);
		}

		if(Input.GetKeyDown (KeyCode.Escape)) 
		{
			if(paused)
			{
				paused = false;
			}
			else
			{
				paused = true;
			}
		}


		if(health <= 0)
		{
			Die();		
		}


		UpdateGameState();
	}

	//NEEDS TO BE CALLED
	
	void UpdateGameState()
	{
		Time.timeScale = paused ? 0 : 1;
		
		playerState = paused ? GameState.PAUSED : GameState.PLAYING;

	}

	void OnGUI() 
	{
		if (paused) 
		{
			if(GUI.Button (new Rect((Screen.width)/2, ((Screen.height)/2)-50, 100, 50), "CONTINUE")) 
			{
				paused = false;
				//	Debug.Log("BUTTON HIT");
			}
			if(GUI.Button (new Rect((Screen.width)/2, ((Screen.height)/2)+50, 100, 50), "SAVE & QUIT")) 
			{
				paused = true;
				Application.LoadLevel("MainMenu");
			}
		}
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if(other.CompareTag("goal")) 
		{
			Application.LoadLevel ("TileMapTester");
		} else
		{
			Application.LoadLevel ("MainMenu");
		}
	}




	public void Respawn(Vector3 spawnPt)
	{
		transform.position = spawnPt;
	}

	public void Die()
	{
		print ("I've been killed");
		Debug.Break ();
	}
}