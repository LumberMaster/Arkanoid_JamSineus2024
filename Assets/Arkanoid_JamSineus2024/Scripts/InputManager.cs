using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public class InputManager : MonoBehaviour
	{
		public UnityEvent OnMouseLeftClick = new UnityEvent();
		public UnityEvent<Vector3> OnMouseMove = new UnityEvent<Vector3>();
		public UnityEvent OnPressESC = new UnityEvent();

		private void Update()
		{
			if (Input.GetMouseButtonDown(0)) OnMouseLeftClick.Invoke();
			if (Input.GetKeyDown(KeyCode.Escape)) OnPressESC.Invoke();

		}


		private void FixedUpdate()
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			OnMouseMove.Invoke(mousePosition);

		}

	}
}