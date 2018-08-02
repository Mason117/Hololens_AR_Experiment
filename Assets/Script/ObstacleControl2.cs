using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControl2 : MonoBehaviour
{


    //障碍物移动代码

    [SerializeField]
    float moveSpeed = -5f;
    private Vector2 theForce_W = new Vector2(-14.0f, 0.0f);
    private Vector2 theForce_W1 = new Vector2(-12.0f, 0.0f);
    private Vector2 theForce_S = new Vector2(-10.0f, 0.0f);
    private Vector2 theForce;

    private void Start()
    {
        string ob_name = this.name;
        switch (ob_name)
        {
            case "wind(Clone)":
                theForce = theForce_W;
                break;
            case "wind1(Clone)":
                theForce = theForce_W1;
                break;
            case "sand(Clone)":
                theForce = theForce_S;
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime,
            transform.position.y);

        if (transform.position.x < -13f)
            Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log(this.name);
        if (other.gameObject.name.Equals("Car"))
        {
            other.attachedRigidbody.AddForce(theForce, ForceMode2D.Force);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(theForce);
        other.attachedRigidbody.AddForce(theForce, ForceMode2D.Force);
    }
}
