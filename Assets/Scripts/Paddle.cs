using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paddle : MonoBehaviour
{
    public bool autoPlay = false;

    private Ball ball;

    void Start ()
    {
        ball = GameObject.FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (autoPlay == false)
        {
            MoveWithMouse();
        }
        else
        {
            AutoPlay();
        }
    }

    void AutoPlay()
    {
        Vector3 paddlesPos = new Vector3(0.5f, this.transform.position.y, 0f);
        Vector3 ballPos = ball.transform.position;
        paddlesPos.x = (Mathf.Clamp(ballPos.x, 0.5f, 15.5f));
        this.transform.position = paddlesPos;
    }

    void MoveWithMouse()
    {
        Vector3 paddlesPos = new Vector3(0.5f, this.transform.position.y, 0f);
        float mousePosInblocks = (Input.mousePosition.x / Screen.width * 16);
        paddlesPos.x = (Mathf.Clamp(mousePosInblocks, 0.5f, 15.5f));
        this.transform.position = paddlesPos;
    }
}
