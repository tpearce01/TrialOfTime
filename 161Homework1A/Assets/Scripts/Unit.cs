using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Resources;

public abstract class Unit : MonoBehaviour
{
    private int level;
    private Element element;

    //Resources
    private float health;
    private PAttribute maxHealth = new PAttribute();
    private float healthRegen;
    private float resource;
    private PAttribute maxResource = new PAttribute();
    private float resourceRegen;

    //Primary stats
    private PAttribute strength = new PAttribute();
    private PAttribute dexterity = new PAttribute();
    private PAttribute constitution = new PAttribute();
    private PAttribute intelligence = new PAttribute();

    //Secondary stats
    private PAttribute armor = new PAttribute();
    private PAttribute criticalChance = new PAttribute();
    private PAttribute criticalDamage = new PAttribute();
    private PAttribute multistrikeChance = new PAttribute();
    private float damageOutModifier;
    private float damageInModifier;
    private int intDamageOutModifier;
    private int intDamageInModifier;
    private float healModifier;
    private float resourceModifier;
    private float timeBetweenAttacks;
    private float attacksPerSecond;
    private float attackSpeedModifier;

    //Other
    private float variance = 0.2f;  //+/-20% variance
    private List<Skill> skills = new List<Skill>();
    private bool isDead = false;

    public Unit()
    {
        health = maxHealth.baseValue = 100;
        resource = maxResource.baseValue = 100;
        healthRegen = resourceRegen = 0;
        strength.baseValue = strength.modifier = 0;
        dexterity.baseValue = dexterity.modifier = 0;
        constitution.baseValue = constitution.modifier = 0;
        intelligence.baseValue = intelligence.modifier = 0;
        element = Element.Neutral;
        armor.baseValue = armor.modifier = 0;
        criticalChance.baseValue = criticalChance.modifier = 0;
        criticalDamage.baseValue = 1f;
        criticalDamage.modifier = 0;
        multistrikeChance.baseValue = multistrikeChance.modifier = 0;
        damageInModifier = 0;
        damageInModifier = 0;
        healModifier = 0;
        resourceModifier = 0;
        attacksPerSecond = 1;
        timeBetweenAttacks = 1;
    }

    public void InitializeStats()
    {
        strength.baseValue = 10;
        dexterity.baseValue = 10;
        constitution.baseValue = 10;
        intelligence.baseValue = 10;

        CalculateAllStats();
    }

    

    public void CalculateAllStats()
    {
        CalculateBaseValues();
    }

    public void CalculateBaseValues()
    {
        maxHealth.baseValue = 100 + constitution.Value()*level;
        maxResource.baseValue = 100 + intelligence.Value()*level;
        armor.baseValue = 10 + constitution.Value()*10;
        criticalChance.baseValue = strength.Value()*0.3f;
        multistrikeChance.baseValue = intelligence.Value()*0.3f;
        attacksPerSecond = (4000f + dexterity.Value()* dexterity.Value() * 3f)/(4000f + dexterity.Value() * 2f);
        timeBetweenAttacks = Mathf.Clamp(1/attacksPerSecond, 1, 0.15f); //Minimum time between attacks is 0.15
    }

    public void ModifyHealth(float value)
    {
        //Modify with variance
        value *= 1 + Random.Range(-variance, variance);

        if (value < 0)
        {
            //Perform modifiers;
            value *= 1 + damageInModifier;  //Percent damage intake modifier
            value += intDamageInModifier;   //Flat damage intake modifier
            value *= (float) (4000 + armor.Value())/(4000 + armor.Value()*10); //Armor becomes less effective as armor increases. Good armor values range from 0-300 for weaker characters or up to 1000 for tank builds
        }
        else
        {   //Heal
            value *= 1 + healModifier + (constitution.Value()/100f);  //Percent heal modifier
        }

        //Calculate new health
        health += value;
        if (health <= 0)
        {
            Kill(); //If health drops below 0, unit is dead
        }
        else if (health >= maxHealth.Value())
        {
            health = maxHealth.Value(); //If health exceeds maximum, normalize
        }
    }

    public void ModifyResource(float value)
    {

        value *= 1 + resourceModifier;
        resource += value;
        if (resource >= maxResource.Value())
        {
            resource = maxResource.Value();
        }
    }

    public void Kill()
    {
        isDead = true;
        health = 0;
    }

    public void Ressurect(int healthh, int resourcee)
    {
        isDead = false;
        health = healthh;
        resource = resourcee;
    }

    public void Ressurect(int healthh)
    {
        isDead = false;
        health = healthh;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        PlayerInput();
    }

	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag ("light_hazard")) {
			HUDManager.i.modifyHealth (-1);
		}
	}



    public abstract void PlayerInput();
    public abstract void Initialize();


}
