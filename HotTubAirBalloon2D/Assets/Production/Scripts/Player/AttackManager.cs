using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class AttackManager : MonoBehaviour
{
    public enum ShootMode {
        MOVING_PROJECTILE,
        CLICK_BURST
    }
    public ShootMode shootMode;

    public enum PlayerNumber {
        ONE, TWO
    }

    public PlayerNumber playerNum;

    public ShootProjectile shootProjectile;

    public ClickBurst clickBurst;
    public Transform cursor;

    public Rewired.Player player;

    void Start(){
        player = ReInput.players.GetPlayer(0);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if((Input.GetButtonDown("Fire1") || player.GetButtonDown("FireBurst")) && playerNum.Equals(PlayerNumber.ONE)) {
            Vector2 cursorPos = cursor.position;
            Shoot(cursorPos);
        }

        if((Input.GetButtonDown("Fire2") || player.GetButtonDown("IceBurst")) && playerNum.Equals(PlayerNumber.TWO)) {
            Vector2 cursorPos = cursor.position;
            Shoot(cursorPos);
        }
    }

    void Shoot(Vector2 position)
    {
        switch (shootMode)
        {
            case ShootMode.MOVING_PROJECTILE:
                shootProjectile.Shoot(position);
                break;

            case ShootMode.CLICK_BURST:
                clickBurst.Burst(position);
                break;
        }
    }
}
