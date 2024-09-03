using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game 
{

	[Serializable]
	public enum EGameStates 
	{
		Ready, Game, Pause
	}

	public class GameState : MonoBehaviour
	{
		[field: SerializeField] public EGameStates State { get; set; } = EGameStates.Ready;

		[SerializeField] private int _healths = 3;
		public int Healths
		{
			get => _healths;
			set
			{
				_healths = value;
				OnChangeHealth.Invoke();
			}
		}


		public UnityEvent OnChangeHealth = new UnityEvent();

		public UnityEvent OnStart = new UnityEvent();

		public UnityEvent OnSpawn = new UnityEvent();

		public UnityEvent OnLose = new UnityEvent();

		public UnityEvent OnWin = new UnityEvent();


	
	}
}
