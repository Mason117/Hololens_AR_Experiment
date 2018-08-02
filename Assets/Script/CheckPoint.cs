using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckPoint : MonoBehaviour
{

    public int Checknumber = 0;

    public bool Check = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Checknumber%2==0)
	    {
	        Check = true;
	    }
	    else
	    {
	        Check = false;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Checknumber++;
    }
}
