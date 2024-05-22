using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingPyroController : FungusController
{
    protected override void UpdateInput()
    {
        if (IsDied) return;

        if (IsEB_ing)
        {
            rb2d.velocity = Vector2.zero;
            return;
        }
        if (IsUsingSkill)
        {
            rb2d.velocity = new Vector2(MoveDirection.x, MoveDirection.y);
            return;
        }

        Skill();
        Move();
        Dash();
    }
}
