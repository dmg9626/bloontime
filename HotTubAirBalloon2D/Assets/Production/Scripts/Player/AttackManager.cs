using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;

public class AttackManager : MonoBehaviour
{
    public enum ShootMode {
        BEAM,
        CLICK_BURST
    }
    public ShootMode shootMode;

    public enum PlayerNumber {
        ONE, TWO
    }

    public PlayerNumber playerNum;

    public BeamAttack beam;

    public Transform cursor;

    public ClickBurst clickBurst;

    public Rewired.Player player;

    void Start(){
        player = ReInput.players.GetPlayer(0);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(!GM.Instance.isFrozen)
        {
            HandleBurstAttack();

            HandleBeamAttack();
        }
    }

    void HandleBeamAttack()
    {
        if(playerNum.Equals(PlayerNumber.ONE)){
            // Beam attack P1
            if (Input.GetKey(KeyCode.LeftControl) || player.GetButton("FireBeam"))
            {
                beam.SetActive(true);

                Vector2 cursorPos = cursor.position;
                beam.FireBeam();
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl) || player.GetButtonUp("FireBeam"))
            {
                beam.SetActive(false);
            }
        }

        if(playerNum.Equals(PlayerNumber.TWO)){
            // Beam attack P2
            if (Input.GetKey(KeyCode.RightControl) || player.GetButton("IceBeam"))
            {
                beam.SetActive(true);

                Vector2 cursorPos = cursor.position;
                beam.FireBeam();
            }
            else if (Input.GetKeyUp(KeyCode.RightControl) || player.GetButtonUp("IceBeam"))
            {
                beam.SetActive(false);
            }
        }
    }

    void HandleBurstAttack()
    {
        // Burst attack P1
        if ((Input.GetButtonDown("Fire1") || player.GetButtonDown("FireBurst")) && playerNum.Equals(PlayerNumber.ONE))
        {
            clickBurst.Burst(cursor.position);
        }

        // Burst attack P2
        if ((Input.GetButtonDown("Fire2") || player.GetButtonDown("IceBurst")) && playerNum.Equals(PlayerNumber.TWO))
        {
            clickBurst.Burst(cursor.position);
        }
    }
}
