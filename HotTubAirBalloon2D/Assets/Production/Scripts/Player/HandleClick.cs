using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleClick : MonoBehaviour
{
    public enum ShootMode {
        MOVING_PROJECTILE,
        CLICK_BURST
    }
    public ShootMode shootMode;

    public ShootProjectile shootProjectile;

    public ClickBurst clickBurst;
    public Transform cursor;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Shoot(mousePos);
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
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
