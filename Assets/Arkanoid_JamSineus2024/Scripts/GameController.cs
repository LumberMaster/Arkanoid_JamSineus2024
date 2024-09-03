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
		public List<Ball> Balls = new List<Ball>();
		public List<Block> Blocks = new List<Block>();


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
			if (Balls.Count > 0) return;


			Blocks = new List<Block>(FindObjectsOfType<Block>());
			foreach (Block block in Blocks) 
			{
				block.OnBreak.AddListener(BindOnBreakBlock);
			}
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

			//Ball ball = Instantiate(_prefabBall, _platformController.SpawnBallPoint.position, Quaternion.identity, transform);
			Ball ball = SpawnBall(_platformController.SpawnBallPoint.position, _platformController.SpawnBallPoint);
			ball.gameObject.GetComponent<TrailRenderer>().enabled = false;
			ball.RigidBody.isKinematic = true;

			OnSpawn.Invoke(); 
		}

		private Ball SpawnBall(Vector3 position, Transform parent) 
		{
			Ball ball = Instantiate(_prefabBall, position, Quaternion.identity, parent);
			Balls.Add(ball);

			return ball;
		}


		private void BindOnBreakBlock(Block block)
		{
			block.OnBreak.RemoveListener(BindOnBreakBlock);
			Ball ball = SpawnBall(block.transform.position, null);
			Blocks.Remove(block);
		}

		private void BindOnMouseLeftClick()
		{
			if (Balls.Count != 1) return;

			Balls[0].RigidBody.isKinematic = false;
			Balls[0].gameObject.GetComponent<TrailRenderer>().enabled = true;
			Balls[0].transform.parent = null;
			Balls[0].RigidBody.AddForce(Vector3.up*400);
		}

		private void BindOnChangeHealth()
		{
			_platformController.HealthText.text = Healths.ToString();
		}

		private void BindOnBallCollision(Ball ball)
		{
			Balls.Remove(ball);
			ball.Destroy();

			if (Balls.Count <= 0 && Healths > 0) { Healths--; Respawn(); }
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