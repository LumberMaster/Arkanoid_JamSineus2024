using UnityEngine;
 
namespace Game 
{
	public class ScreenAdapter : MonoBehaviour
	{


		[SerializeField] private float _padding = 0.7f;


		[SerializeField] private Transform _rightWall;
		public Transform RightWall { get => _rightWall; protected set => _rightWall = value; }

		[SerializeField] private Transform _leftWall;
		public Transform LeftWall { get => _leftWall; protected set => _leftWall = value; }

		[SerializeField] private Transform _topWall;
		public Transform TopWall { get => _topWall; protected set => _topWall = value; }


		private void Awake()
		{
			ReloadWalls();
		}

		private void ReloadWalls()
		{
			RightWall.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - _padding, Screen.height, 10.0f));
			LeftWall.position = Camera.main.ScreenToWorldPoint(new Vector3(0.0f + _padding, Screen.height, 10.0f));
			TopWall.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height - _padding, 10.0f));
		}
	}

}
