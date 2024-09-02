using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public class InputManager : MonoBehaviour
	{
		public UnityEvent<Vector3> OnMouseMove = new UnityEvent<Vector3>();

		private void Update()
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			OnMouseMove.Invoke(mousePosition);
		}

	}
}