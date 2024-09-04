using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Window 
{
	public class MainWindow : AWindow
	{
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _aboutGameButton;
		[SerializeField] private Button _exitButton;

		private void OnEnable()
		{
			_playButton.onClick.AddListener(BindOnClickPlayButton);
			//_aboutGameButton.onClick.AddListener(BindOnClickAboutGameButton);
			_exitButton.onClick.AddListener(BindOnClickExitButton);
		}
		private void OnDisable()
		{
			_playButton.onClick.RemoveListener(BindOnClickPlayButton);
			//_aboutGameButton.onClick.RemoveListener(BindOnClickAboutGameButton);
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

		private void BindOnClickExitButton()
		{
			Application.Quit();
		}

		private void BindOnClickAboutGameButton()
		{

		}

		private void BindOnClickPlayButton()
		{
			this.Hide();
			GameController.Instance.StartGame();
		}


	}

}
