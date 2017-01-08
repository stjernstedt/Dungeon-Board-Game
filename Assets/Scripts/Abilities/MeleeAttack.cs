using UnityEngine;
using System.Collections;
using System;

public class MeleeAttack : Ability
{
	void Awake()
	{
		abilityName = "Melee Attack";
		range = 2;
	}

	public override void Execute()
	{
		charHandler.abilityRunning = true;
		targetingHandler.StartTargeting(this);
	}

}