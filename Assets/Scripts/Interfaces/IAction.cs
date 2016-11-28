using UnityEngine;
using System.Collections;

public interface IAction
{
	void Execute();
	IEnumerator Target();
}