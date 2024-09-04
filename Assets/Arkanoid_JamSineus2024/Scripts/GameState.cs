using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game 
{

	[Serializable]
	public enum EGameStates 
	{
		Ready, Game,
		FirstLaunch
	}

	public class GameState : MonoBehaviour
	{
		[SerializeField] private EGameStates state = EGameStates.Ready;
		public EGameStates State {
			get => state;
			set {
				state = value;
				OnChangeState.Invoke(State);
			}
		}

		public UnityEvent<EGameStates> OnChangeState = new UnityEvent<EGameStates>();


		[SerializeField] private int _maxHealths = 3;

		private int _healths;
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


		[SerializeField] private int _playerLevel = 0;
		public int PlayerLevel
		{
			get => _playerLevel;
			set
			{
				_playerLevel = value;
				_playerLevelText.text = "������� " + _playerLevel;
			}
		}
		[SerializeField] private TMP_Text _playerLevelText;


		public UnityEvent OnStart = new UnityEvent();

		public UnityEvent OnSpawnBall = new UnityEvent();

		public UnityEvent OnLose = new UnityEvent();

		public UnityEvent OnWin = new UnityEvent();

		private void Awake()
		{
			_playerLevelText.text = "������� " + PlayerLevel;
		}

		public void ReciveMessage(string message) 
		{
			switch (State)
			{
				case EGameStates.Ready:
					if (message == "StartGame") 
					{
						State = EGameStates.FirstLaunch; 
						Healths = _maxHealths; 
						OnStart.Invoke();
						return;
					}
					break;
				case EGameStates.FirstLaunch:
					if (message == "BallLaunched")
					{
						State = EGameStates.Game;
						return;
					}
					break;
					
				case EGameStates.Game:
					if (message == "ResetGame") 
					{
						State = EGameStates.Ready;
						Healths = _maxHealths;
						return;
					} 
					if (message == "SpawnBall")
					{
						State = EGameStates.FirstLaunch;
						OnSpawnBall.Invoke();
						return;

					}
					if (message == "Lose") 
					{
						State = EGameStates.Ready; 
						OnLose.Invoke();
						return;

					}
					if (message == "Win") 
					{
						State = EGameStates.Ready;
						OnWin.Invoke();
						return;
					}
					
					break;
				
			}


		}

	}
}
