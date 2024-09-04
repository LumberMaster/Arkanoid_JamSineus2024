using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Game
{
	public class PlatformController : MonoBehaviour
	{
		[Header("Data")]

		[SerializeField] private ScreenAdapter _screenAdapter;
		[SerializeField] private float _force = 20f;
		private float _minPosition;
		private float _maxPosition;

		[Header("UI")]
		[SerializeField] private TMP_Text _healthText;
		public TMP_Text HealthText { get => _healthText; protected set => _healthText = value; }

		
		[SerializeField] private Transform _spawnBallPoint;
		public Transform SpawnBallPoint { get => _spawnBallPoint; protected set => _spawnBallPoint = value; }

		private void Awake()
		{
			_minPosition = _screenAdapter.LeftWall.position.x + transform.localScale.x + 1.2f;
			_maxPosition = _screenAdapter.RightWall.position.x - transform.localScale.x - 1.2f;

		}

		private void OnCollisionEnter(Collision collision)
		{
			Ball ball = collision.gameObject.GetComponent<Ball>();
			if (ball == null) return;
			Vector3 direct = gameObject.transform.position - collision.GetContact(0).point;

			ball.RigidBody.AddForce((-direct)* _force);
		}

		public void SetPosition(float posX) 
		{
			transform.position = new Vector3(
				Mathf.Clamp(posX, _minPosition, _maxPosition),
				transform.position.y,
				transform.position.z
			);
		}

	}
}