using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSM : MonoBehaviour {

	public delegate void State();
	private List<State> availableActions = new List<State>();
	private State action;

	public FSM(State state)
	{
		availableActions.Add(state);
		action = state;
	}

	public void AddState(State state)
	{
		availableActions.Add(state);
	}

	public void RandomState()
	{
		SetState(availableActions[Random.Range(0, availableActions.Count)]);
	}

	public void SetState(State state)
	{
		action = state;	
	}

	public void SetStateByName(string stateName)
	{
		foreach(State state in availableActions)
		{
			if(state.Method.Name == stateName)
			{
				action = state;	
				return;
			}
		}
		Debug.LogError("Incorrect State");
	}

	public State GetCurrentState()
	{
		return action;
	}

	public void Update () 
	{
		action();
	}
}
