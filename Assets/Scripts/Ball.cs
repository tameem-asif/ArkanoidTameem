﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {
	public float speed = 100.0f;
	Vector2 rackPos = Vector2.zero;
	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.up*speed;
		rackPos = GameObject.Find("racket").transform.position;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("On Collision");
		if(col.gameObject.name == "racket")
		{
			float x = hitFactor(transform.position,
			col.transform.position,col.collider.bounds.size.x);

			Vector2 dir = new Vector2(x, 1).normalized;

			GetComponent<Rigidbody2D>().velocity = dir*speed;
		}
		if(col.gameObject.name == "border_top"||col.gameObject.name == "border_right"||col.gameObject.name == "border_left")
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.down*speed;
		}
	}
	
	float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
	{
		return(ballPos.x - racketPos.x)/racketWidth;
	}

	void FixedUpdate()
	{
		if(gameObject.transform.position.y<=rackPos.y)
		{
			Debug.Log("Ball out of bounds");
			SceneManager.LoadScene("RestartScreen");
		}
	}
}
