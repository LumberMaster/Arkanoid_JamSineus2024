using Game.Window;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// Очень плохой патерн синглтона и антипатерн божественого объекта :(
	/// ☭✞✞Боже помоги избавить код от большой связности ✞✞☭
	/// </summary>
	public class GameController : MonoBehaviour
	{

		private static GameController _instance;
		public static GameController Instance { 
			get => _instance; 
			protected set => _instance = value; 
		}

		[SerializeField] private PlatformController _platformController;
		public PlatformController PlatformController { 
			get => _platformController; 
			protected set => _platformController = value; 
		}

		[SerializeField] private WindowManager _windowManager;
		public WindowManager WindowManager { 
			get => _windowManager; 
			protected set => _windowManager = value; 
		}

		[SerializeField] private GameState _gameState;
		public GameState GameState { 
			get => _gameState; 
			protected set => _gameState = value; 
		}

		[SerializeField] private LavaArea _lavaArea;
		public LavaArea LavaArea { 
			get => _lavaArea; 
			protected set => _lavaArea = value; 
		}


		[SerializeField] private InputManager _inputManager;


		[SerializeField] private Ball _prefabBall;
		public List<Ball> _balls = new List<Ball>();
		public List<Block> _blocks = new List<Block>();

		private void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(Instance);
		}

		private void OnEnable()
		{
			_inputManager.OnMouseMove.AddListener(BindOnMouseMove);
			_inputManager.OnMouseLeftClick.AddListener(BindOnMouseLeftClick);
			_inputManager.OnPressESC.AddListener(BindOnPressESC);
			LavaArea.OnBallCollision.AddListener(BindOnBallCollision);
			GameState.OnChangeHealth.AddListener(BindOnChangeHealth);
			GameState.OnLose.AddListener(BindOnLose);
			GameState.OnWin.AddListener(BindOnWin);
		}

		private void OnDisable()
		{
			_inputManager.OnMouseMove.RemoveListener(BindOnMouseMove);
			_inputManager.OnMouseLeftClick.RemoveListener(BindOnMouseLeftClick);
			_inputManager.OnPressESC.RemoveListener(BindOnPressESC);
			LavaArea.OnBallCollision.RemoveListener(BindOnBallCollision);
			GameState.OnChangeHealth.RemoveListener(BindOnChangeHealth);
			GameState.OnLose.RemoveListener(BindOnLose);
			GameState.OnWin.RemoveListener(BindOnWin);
		}

	
		public void StartGame() 
		{
		
			if (_balls.Count > 0) return;


			_blocks = new List<Block>(FindObjectsOfType<Block>());
			foreach (Block block in _blocks) 
			{
				block.OnBreak.AddListener(BindOnBreakBlock);
			}

			GameState.ReciveMessage("StartGame");
			RespawnBall();
		}

		public void ReloadGame()
		{
			ResetGame();
			StartGame();
		}

		private void ResetGame()
		{
			foreach (Ball ball in _balls) ball.Destroy();
			foreach (Block block in _blocks)
			{
				block.OnBreak.RemoveListener(BindOnBreakBlock);
				block.Destroy();
			}

			_balls.Clear();
			_blocks.Clear();

			GameState.ReciveMessage("ResetGame");
		}

		private void RespawnBall() 
		{

			Ball ball = SpawnBall(PlatformController.SpawnBallPoint.position, PlatformController.SpawnBallPoint);
			ball.gameObject.GetComponent<TrailRenderer>().enabled = false;
			ball.RigidBody.isKinematic = true;

			GameState.ReciveMessage("SpawnBall"); 
		}

		private Ball SpawnBall(Vector3 position, Transform parent) 
		{
			Ball ball = Instantiate(_prefabBall, position, Quaternion.identity, parent);
			_balls.Add(ball);
			return ball;
		}



		private void BindOnWin()
		{
			WindowManager.Show(WindowManager.WinWindow);
		}

		private void BindOnLose()
		{
			WindowManager.Show(WindowManager.LoseWindow);
		}
		private void BindOnPressESC()
		{
			if (WindowManager.PauseWindow.IsEnable) WindowManager.Hide();
			else WindowManager.Show(WindowManager.PauseWindow);
		}

		private void BindOnBreakBlock(Block block)
		{
			block.OnBreak.RemoveListener(BindOnBreakBlock);
			Ball ball = SpawnBall(block.transform.position, null);
			_blocks.Remove(block);

			if(_blocks.Count == 0) GameState.ReciveMessage("Win");
		}

		private void BindOnMouseLeftClick()
		{
			if (GameState.State != EGameStates.FirstLaunch) return;
			if (_balls.Count != 1) return;

			_balls[0].RigidBody.isKinematic = false;
			_balls[0].gameObject.GetComponent<TrailRenderer>().enabled = true;
			_balls[0].transform.parent = null;
			_balls[0].RigidBody.AddForce(Vector3.up*400);
			GameState.ReciveMessage("BallLaunched");
		}

		private void BindOnChangeHealth()
		{
			PlatformController.HealthText.text = GameState.Healths.ToString();
		}

		private void BindOnBallCollision(Ball ball)
		{
			_balls.Remove(ball);
			ball.Destroy();

			if (_balls.Count <= 0 && GameState.Healths > 0) { GameState.Healths--; RespawnBall(); }
			if (GameState.Healths <= 0) GameState.ReciveMessage("Lose");
		}

		private void BindOnMouseMove(Vector3 positionMouse) => PlatformController.SetPosition(positionMouse.x);

		

	}
}