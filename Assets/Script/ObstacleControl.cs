using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControl : MonoBehaviour {

    //障碍物移动代码

	[SerializeField]
	float moveSpeed = -5f;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (transform.position.x + moveSpeed * Time.deltaTime,
			transform.position.y);
		
		if (transform.position.x < -13f)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Equals ("runner"))
			GameControl.instance.DinoHit ();

	}

}
