using System;
using UnityEngine;

namespace Game
{
	public class GameController : MonoBehaviour
	{
		[SerializeField] private PlatformController _platformController;
		[SerializeField] private InputManager _inputManager;

		private void OnEnable()
		{
			Cursor.visible = false;
			_inputManager.OnMouseMove.AddListener(BindOnMouseMove);
		}

		private void OnDisable()
		{
			Cursor.visible = true;
			_inputManager.OnMouseMove.RemoveListener(BindOnMouseMove);
		}


		private void BindOnMouseMove(Vector3 positionMouse)
		{
			_platformController.transform.position = new Vector3(
				positionMouse.x,
				_platformController.transform.position.y,
				_platformController.transform.position.z
			);

		}

	}
}