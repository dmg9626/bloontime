using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public enum ShootMode {
        MOVING_PROJECTILE,
        CLICK_BURST
    }
    public ShootMode shootMode;

    public enum Player {
        ONE, TWO
    }

    public Player playerNum;

    public ShootProjectile shootProjectile;

    public ClickBurst clickBurst;
    public Transform cursor;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && playerNum.Equals(Player.ONE)) {
            Vector2 cursorPos = cursor.position;
            Shoot(cursorPos);
        }

        if(Input.GetButtonDown("Fire2") && playerNum.Equals(Player.TWO)) {
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
