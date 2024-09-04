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
			throw new NotImplementedException();
		}

		private void BindOnClickReloadButton()
		{
			GameController.Instance.ReloadGame();
		}

		private void BindOnClickExitButton()
		{
			AWindow window = GameController.Instance.WindowManager.MainWindow;
			GameController.Instance.WindowManager.Show(window);
		}


	}
}
