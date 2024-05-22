using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Attack,
    Healing,
    Shied
}

[CreateAssetMenu(fileName = "New Skill Config", menuName = "Config/Skill")]
public class FungusSkillConfig : ScriptableObject
{
    [SerializeReference, SubclassSelector] public List<SkillEffect> skillList;

    public SkillConfig nA_SkillConfig;
    public SkillConfig eS_SkillConfig;
    public SkillConfig eB_SkillConfig;

}


[System.Serializable]
public class SkillConfig
{
    [TextArea] public string description;
    public float cooldown;
    public float moveSpeed = 10f;
    public float range;
    public float activeTime;
    
    public int valuePercent = 100; 
    // % giá tr? c?a k? n?ng vd: (500%) => skill attack scale theo atk (1000) => 1000 * 500% == 5000 damage
    
    public SkillBase skillPrefab;
}

public interface ISkillEffect
{
    void Apply();
    void Remove();
}

[Serializable]
public class SkillEffect : ISkillEffect
{
    [TextArea] public string description;
    public float cooldown;

    public void Apply()
    {
        throw new NotImplementedException();
    }

    public void Remove()
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class Healing : SkillEffect
{
    public int valueHealing = 100;
}

[Serializable]
public class Shield : SkillEffect
{
    public int valueShield = 100;
}

