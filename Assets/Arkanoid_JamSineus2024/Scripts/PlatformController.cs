using TMPro;
using UnityEngine;

namespace Game
{
	public class PlatformController : MonoBehaviour
	{
		[Header("Data")]

		[SerializeField] private float _force = 20f;
		[SerializeField] private float _minPosition;
		[SerializeField] private float _maxPosition;

		[Header("UI")]
		[SerializeField] private TMP_Text _healthText;
		public TMP_Text HealthText { get => _healthText; protected set => _healthText = value; }

		
		[SerializeField] private Transform _spawnBallPoint;
		public Transform SpawnBallPoint { get => _spawnBallPoint; protected set => _spawnBallPoint = value; }

		private void OnCollisionEnter(Collision collision)
		{
			Ball ball = collision.gameObject.GetComponent<Ball>();
			if (ball == null) return;
			Vector3 direct = gameObject.transform.position - collision.GetContact(0).point;

			ball.RigidBody.AddForce((-direct)* _force);
		}
	}
}