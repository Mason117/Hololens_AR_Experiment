using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{

    private Vector2 startForce = new Vector2(2.5f, 0.0f);

    private Vector2 tempForce;
    // Use this for initialization
    void Start ()
    {
        tempForce = startForce;

    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.RightArrow))
	    {
            tempForce.x += Time.deltaTime * 2;
	        Debug.Log(tempForce);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 0), ForceMode2D.Impulse);//瞬时力。爆炸那种冲击力
            GetComponent<Rigidbody2D>().AddForce(tempForce, ForceMode2D.Force);//持续力
	        
        }
	    if (Input.GetKeyUp(KeyCode.RightArrow))
	        tempForce = startForce;

        if (Input.GetKey(KeyCode.LeftArrow))
	    {
	        tempForce.x += Time.deltaTime * 2;
            GetComponent<Rigidbody2D>().AddForce(-tempForce, ForceMode2D.Force);//持续力
	        Debug.Log(tempForce);
        }
	    if (Input.GetKeyUp(KeyCode.LeftArrow))
	        tempForce = startForce;
    }
    
}
