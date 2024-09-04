using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Window
{
	public class LoseWindow : AWindow
	{
		[SerializeField] private Button _reloadGameButton;
		[SerializeField] private Button _exitButton;

		private void OnEnable()
		{
			_reloadGameButton.onClick.AddListener(BindOnClickReloadButton);
			_exitButton.onClick.AddListener(BindOnClickExitButton);
		}

		private void OnDisable()
		{
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