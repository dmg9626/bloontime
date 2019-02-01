using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  Hot Tub Air Balloon Prototype
 *  Josh Karmel (Copyright 2019)
 *  ~BalloonController.cs~
 */

public class BalloonController : MonoBehaviour
{
/**************************************************************************Public Fields */
    public GameObject BalloonChar, cursor;
    public ClickBurst clickBurst;
    public float temperature, tempMultiplier, cursorSpeed;//, balloonSpeed;
    public bool bottomCollision;

/****************************************************************************Private Fields */
    [SerializeField]
    private float balloonVerticalSpeed, balloonHorizontalSpeed;
    [SerializeField]
    private Vector2 charPos;

/***************************************************************************Mono Methods */
    // Start is called before the first frame update
    void Start()
    {
        charPos = BalloonChar.transform.position;
        balloonHorizontalSpeed = .005f;
        bottomCollision = false;
        if(tempMultiplier == 0)
        {
            tempMultiplier = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        BalloonMovement();
        CursorMovement();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
    }

/*************************************************************************Methods*/
    //controls the vertical movement of the ballon using temperature and preset horizontal temp
    void BalloonMovement()
    {
        balloonVerticalSpeed = temperature / tempMultiplier;

        if(!bottomCollision || balloonVerticalSpeed > 0)
        {
            charPos.y += balloonVerticalSpeed;

            charPos.x += balloonHorizontalSpeed;
        }

        BalloonChar.transform.position = charPos;
    }

    //moves the cursor with WASD or arrows
    void CursorMovement()
    {
        Vector2 pos = cursor.transform.position;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            pos.x -= cursorSpeed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            pos.x += cursorSpeed;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            pos.y += cursorSpeed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            pos.y -= cursorSpeed;

        cursor.transform.position = pos;
    }

/***************************************************************************Public Methods */
    //takes in temp to change temperature
    public void changeTemp(float tempChange)
    {
        temperature += tempChange;
    }
}
