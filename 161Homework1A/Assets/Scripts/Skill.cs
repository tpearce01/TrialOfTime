using UnityEngine;
using System.Collections;

public enum Target{
    Player,
    Enemy,
    Friendly,
    Neutral,
    Other
};

public enum SkillType
{
    Buff,
    Debuff,
    Heal,
    Damage,
    Modifier,
    Other
};

public enum Element
{
    Light,
    Dark,
    Fire,
    Earth,
    Water,
    Air,
    Neutral,
    Other
}

public class Skill
{
    public int id;
    public string name;
    public float healthModifier;
    public float resourceModifier;
    public float duration;
    public float autoStrikeChance;
    public Target target;
    public SkillType type;
    public Element element;
}
