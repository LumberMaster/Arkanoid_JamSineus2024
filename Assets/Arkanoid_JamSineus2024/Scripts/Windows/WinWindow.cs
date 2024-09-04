using System;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Window
{
	public class WinWindow : AWindow
	{
		[SerializeField] private Button _nextLevelButton;
		[SerializeField] private Button _reloadGameButton;
		[SerializeField] private Button _exitButton;

		private void OnEnable()
		{
			_nextLevelButton.onClick.AddListener(BindOnClickNextLevelButton);
			_reloadGameButton.onClick.AddListener(BindOnClickReloadButton);
			_exitButton.onClick.AddListener(BindOnClickExitButton);
		}
		private void OnDisable()
		{
			_nextLevelButton.onClick.RemoveListener(BindOnClickNextLevelButton);
			_reloadGameButton.onClick.RemoveListener(BindOnClickReloadButton);
			_exitButton.onClick.RemoveListener(BindOnClickExitButton);
		}

		public override void Show()
		{
			base.Show();
			Time.timeScale = 0.0f;
		}

		public override void Hide()
		{
			base.Hide();
			Time.timeScale = 1.0f;
		}


		private void BindOnClickNextLevelButton()
		{

			if (GameController.Instance.GameState.PlayerLevel + 1 >= GameController.Instance.LevelManager.Levels.Count) GameController.Instance.GameState.PlayerLevel = 0;
			else GameController.Instance.GameState.PlayerLevel++;

			GameController.Instance.StartGame();
			this.Hide();
		}

		private void BindOnClickReloadButton()
		{
			GameController.Instance.StartGame();
			this.Hide();

		}

		private void BindOnClickExitButton()
		{

			AWindow window = GameController.Instance.WindowManager.MainWindow;
			GameController.Instance.WindowManager.Show(window);
		}


	}
}
