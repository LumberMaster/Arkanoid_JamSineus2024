using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace Game.Window
{
	public class WindowManager : MonoBehaviour
	{
		[field: SerializeField] public AWindow ActiveWindow { get; protected set; }
		[field: SerializeField] public WinWindow WinWindow { get; protected set; }
		[field: SerializeField] public LoseWindow LoseWindow { get; protected set; }
		[field: SerializeField] public MainWindow MainWindow { get; protected set; }
		[field: SerializeField] public PauseWindow PauseWindow { get; protected set; }



		private void Awake()
		{
			DontDestroyOnLoad(this);
			Show(MainWindow);
		}

		public void Show(AWindow window) 
		{
			if (ActiveWindow) 
			{
				ActiveWindow.Hide();
				ActiveWindow = window;
				ActiveWindow.Show();
			} 
			else 
			{
				ActiveWindow = window;
				ActiveWindow.Show();
			}
		}
		public void Hide()
		{
			if (ActiveWindow)
			{
				ActiveWindow.Hide();
				ActiveWindow = null;
			}
		}
	}
}