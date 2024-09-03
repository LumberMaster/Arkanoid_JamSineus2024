using Game.Window;
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
		[SerializeField] private GameState _gameState;
		[SerializeField] private WindowManager _windowManager;



		[SerializeField] private Ball _prefabBall;
		public List<Ball> Balls = new List<Ball>();
		public List<Block> Blocks = new List<Block>();



		private void Start()
		{
			StartGame();
		}

		private void OnEnable()
		{
			_inputManager.OnMouseMove.AddListener(BindOnMouseMove);
			_inputManager.OnMouseLeftClick.AddListener(BindOnMouseLeftClick);
			_inputManager.OnPressESC.AddListener(BindOnPressESC);
			_lavaArea.OnBallCollision.AddListener(BindOnBallCollision);
			_gameState.OnChangeHealth.AddListener(BindOnChangeHealth);
		}



		private void OnDisable()
		{
			_inputManager.OnMouseMove.RemoveListener(BindOnMouseMove);
			_inputManager.OnMouseLeftClick.RemoveListener(BindOnMouseLeftClick);
			_inputManager.OnPressESC.RemoveListener(BindOnPressESC);
			_lavaArea.OnBallCollision.RemoveListener(BindOnBallCollision);
			_gameState.OnChangeHealth.RemoveListener(BindOnChangeHealth);
		}

	
		public void StartGame() 
		{
			if (Balls.Count > 0) return;


			Blocks = new List<Block>(FindObjectsOfType<Block>());
			foreach (Block block in Blocks) 
			{
				block.OnBreak.AddListener(BindOnBreakBlock);
			}
			_gameState.Healths = 3;
			Respawn();
		}

	
		private void Respawn() 
		{

			Ball ball = SpawnBall(_platformController.SpawnBallPoint.position, _platformController.SpawnBallPoint);
			ball.gameObject.GetComponent<TrailRenderer>().enabled = false;
			ball.RigidBody.isKinematic = true;

			_gameState.OnSpawn.Invoke(); 
		}

		private Ball SpawnBall(Vector3 position, Transform parent) 
		{
			Ball ball = Instantiate(_prefabBall, position, Quaternion.identity, parent);
			Balls.Add(ball);

			return ball;
		}

		private void BindOnPressESC()
		{
			if (_windowManager.PauseWindow.IsEnable) _windowManager.Hide();
			else _windowManager.Show(_windowManager.PauseWindow);
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
			_platformController.HealthText.text = _gameState.Healths.ToString();
		}

		private void BindOnBallCollision(Ball ball)
		{
			Balls.Remove(ball);
			ball.Destroy();

			if (Balls.Count <= 0 && _gameState.Healths > 0) { _gameState.Healths--; Respawn(); }
			if (_gameState.Healths <= 0) _gameState.OnLose.Invoke();
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