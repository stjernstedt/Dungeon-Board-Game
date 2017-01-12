using UnityEngine;
using System.Collections;
using System;

public class MeleeAttack : Ability
{
	
	void Awake()
	{
		abilityName = "Melee Attack";
		range = 2;
		damage = 10;
	}

	public override void Execute()
	{
		charHandler.abilityRunning = true;
		targetingHandler.StartTargeting(this);
	}

	public override void FireAt(GameObject target)
	{
		target.GetComponent<Health>().Damage(damage);
	}
}