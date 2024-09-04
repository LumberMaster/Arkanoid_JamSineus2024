using System;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Window
{
	public class PauseWindow : AWindow
	{
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _reloadGameButton;
		[SerializeField] private Button _exitButton;

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

		private void OnEnable()
		{
			_playButton.onClick.AddListener(BindOnClickPlayButton);
			_reloadGameButton.onClick.AddListener(BindOnClickReloadButton);
			_exitButton.onClick.AddListener(BindOnClickExitButton);
		}
		private void OnDisable()
		{
			_playButton.onClick.RemoveListener(BindOnClickPlayButton);
			_reloadGameButton.onClick.RemoveListener(BindOnClickReloadButton);
			_exitButton.onClick.RemoveListener(BindOnClickExitButton);
		}
		private void BindOnClickExitButton()
		{
			this.Hide();
			AWindow window = GameController.Instance.WindowManager.MainWindow;
			GameController.Instance.WindowManager.Show(window);
		}

		private void BindOnClickReloadButton()
		{
			this.Hide();
			GameController.Instance.ReloadGame();
		}

		private void BindOnClickPlayButton()
		{
			this.Hide();
		}

		

	}
}