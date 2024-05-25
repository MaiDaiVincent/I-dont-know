using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillAnimEvent : MonoBehaviour
{
    public CircleCollider2D coll2D;
    public void StartAnim()
    {
        coll2D.enabled = false;
    }
    public void EndAnim()
    {
        coll2D.enabled = true;
    }
}
