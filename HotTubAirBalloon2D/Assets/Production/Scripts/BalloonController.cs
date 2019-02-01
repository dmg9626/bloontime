using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonController : MonoBehaviour
{
    public GameObject BalloonChar, cursor;
    public ClickBurst clickBurst;
    public float temperature, tempMultiplier, cursorSpeed;//, balloonSpeed;


    [SerializeField]
    private float balloonVerticalSpeed, balloonHorizontalSpeed;
    [SerializeField]
    private Vector2 charPos;

    // Start is called before the first frame update
    void Start()
    {
        charPos = BalloonChar.transform.position;
        balloonHorizontalSpeed = .005f;
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
        // checkShoot();
    }

    void checkShoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            clickBurst.Burst(cursor.transform.position);
        }
    }

    void BalloonMovement()
    {
        balloonVerticalSpeed = temperature / tempMultiplier;
        charPos.y += balloonVerticalSpeed;

        charPos.x += balloonHorizontalSpeed;

        BalloonChar.transform.position = charPos;
    }

    void CursorMovement()
    {
        Vector2 pos = cursor.transform.position;
        if (Input.GetKey(KeyCode.A))
            pos.x -= cursorSpeed;
        if (Input.GetKey(KeyCode.D))
            pos.x += cursorSpeed;
        if (Input.GetKey(KeyCode.W))
            pos.y += cursorSpeed;
        if (Input.GetKey(KeyCode.S))
            pos.y -= cursorSpeed;

        cursor.transform.position = pos;
    }
}
