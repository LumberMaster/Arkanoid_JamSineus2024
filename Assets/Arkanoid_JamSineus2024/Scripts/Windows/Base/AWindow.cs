using UnityEngine;

namespace Game.Window
{
	public abstract class AWindow : MonoBehaviour
	{
		[SerializeField] private bool isEnable;
		public bool IsEnable { get => isEnable; protected set => isEnable = value; }

		public virtual void Show()
		{
			IsEnable = true;
			Cursor.visible = true;
			gameObject.SetActive(true);
		}
		public virtual void Hide()
		{
			IsEnable = false;
			Cursor.visible = false;
			gameObject.SetActive(false);

		}
	}
}