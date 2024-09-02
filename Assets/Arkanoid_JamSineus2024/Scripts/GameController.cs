using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public class GameController : MonoBehaviour
	{

		[SerializeField] private PlatformController _platformController;
		[SerializeField] private LavaArea _lavaArea;
		[SerializeField] private InputManager _inputManager;


		[SerializeField] private int _healths = 3;
		public int Healths
		{
			get => _healths; 
			protected set
			{
				_healths = value;
				OnChangeHealth.Invoke();
			}
		}

		public UnityEvent OnChangeHealth = new UnityEvent();

		[SerializeField] private Ball _prefabBall;
		private List<Ball> _balls = new List<Ball>();


		public UnityEvent OnStart = new UnityEvent();

		public UnityEvent OnSpawn = new UnityEvent();

		public UnityEvent OnLose = new UnityEvent();

		public UnityEvent OnWin = new UnityEvent();

		private void Start()
		{
			StartGame();
		}

		private void OnEnable()
		{
			Cursor.visible = false;
			_inputManager.OnMouseMove.AddListener(BindOnMouseMove);
			_inputManager.OnMouseLeftClick.AddListener(BindOnMouseLeftClick);
			_lavaArea.OnBallCollision.AddListener(BindOnBallCollision);
			OnChangeHealth.AddListener(BindOnChangeHealth);
		}



		private void OnDisable()
		{
			Cursor.visible = true;
			_inputManager.OnMouseMove.RemoveListener(BindOnMouseMove);
			_inputManager.OnMouseLeftClick.RemoveListener(BindOnMouseLeftClick);
			_lavaArea.OnBallCollision.RemoveListener(BindOnBallCollision);
			OnChangeHealth.RemoveListener(BindOnChangeHealth);
		}


		public void StartGame() 
		{
			if (_balls.Count > 0) return;
			
			Healths = 3;
			Respawn();
		}


		public void SetPause(bool isEnablePause)
		{
			if(isEnablePause) Time.timeScale = 0.0f;
			else Time.timeScale = 1.0f;
		}

		private void Respawn() 
		{
			//Ball ball = Instantiate(_prefabBall, _platformController.SpawnBallPoint.position, Quaternion.identity, _platformController.SpawnBallPoint);
			Ball ball = Instantiate(_prefabBall, _platformController.SpawnBallPoint.position, Quaternion.identity, transform);
			_balls.Add(ball);

			OnSpawn.Invoke(); 
		}


		private void BindOnMouseLeftClick()
		{
			if (_balls.Count != 1) return;

			_balls[0].RigidBody.isKinematic = false;
			_balls[0].RigidBody.AddForce(Vector3.up);
		}

		private void BindOnChangeHealth()
		{
			_platformController.HealthText.text = Healths.ToString();
		}

		private void BindOnBallCollision(Ball ball)
		{
			_balls.Remove(ball);
			ball.Destroy();

			if (_balls.Count <= 0 && Healths > 0) { Healths--; Respawn(); }
			if (Healths <= 0) OnLose.Invoke();
		}

		private void BindOnMouseMove(Vector3 positionMouse)
		{
			_platformController.transform.position = new Vector3(
				positionMouse.x,
				_platformController.transform.position.y,
				_platformController.transform.position.z
			);

		}

	}
}