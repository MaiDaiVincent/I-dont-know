using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Config", menuName = "Config/Boss")]
public class BossConfig : ScriptableObject
{
    [Header("Info")]
    public string bossName;
    public string bossDescription;

    public Sprite bossModelSprite;
    public Sprite bossAvatar;

    public Color bossColor;

    public Elemental fungusElemental;

}