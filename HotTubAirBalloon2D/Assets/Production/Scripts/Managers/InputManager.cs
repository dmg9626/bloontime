using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class InputManager : MonoBehaviour
{
/**************************************************************************Public Fields */
    public GameController gm;

    public GameObject cursor1, cursor2;

/****************************************************************************Private Fields */
    [SerializeField]
    private Rewired.Player player { get { return GetRewiredPlayer(0); } }
    [SerializeField]
    private Vector2 cursor1Movement, cursor2Movement;

    ///<summary>
    ///CursorMovement, AttackManager, and ShootProjectile references for both cursors
    ///</summary>
    [SerializeField]
    private CursorMovement c1M, c2M;
    [SerializeField]
    private AttackManager c1AM, c2AM;

/***************************************************************************Mono Methods */
    // Start is called before the first frame update
    void Start()
    {
        c1M = cursor1.GetComponent<CursorMovement>();
        c2M = cursor2.GetComponent<CursorMovement>();

        c1AM = cursor1.GetComponent<AttackManager>();
        c2AM = cursor2.GetComponent<AttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        checkPause();
        checkAnalogs();
    }

/*************************************************************************Methods*/
    ///<summary>
    ///Gets the player id for rewired
    ///</summary>
    public static Rewired.Player GetRewiredPlayer(int gamePlayerId) {
        return ReInput.players.GetPlayer(0);
    }

    ///<summary>
    ///
    ///</summary>
    void checkPause()
    {
        if((player.GetButtonDown("Pause")) || Input.GetKeyDown("escape"))
            gm.PauseGame();
    }

    ///<summary>
    ///Checks Analogs for input
    ///</summary>
    void checkAnalogs()
    {
        if(!gm.isFrozen)
        {
            cursor1Movement.x = player.GetAxis("Fire_MoveCursorX");
            cursor1Movement.y = player.GetAxis("Fire_MoveCursorY");
            cursor2Movement.x = player.GetAxis("Ice_MoveCursorX");
            cursor2Movement.y = player.GetAxis("Ice_MoveCursorY");
            c1M.UpdateCursor(cursor1Movement.x, cursor1Movement.y);
            c2M.UpdateCursor(cursor2Movement.x, cursor2Movement.y);
        }
    }
}
