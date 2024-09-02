using UnityEngine;
using UnityEngine.Events;

namespace Game 
{
	public class LavaArea : MonoBehaviour
	{
		public UnityEvent<Ball> OnBallCollision = new UnityEvent<Ball>();

		private void OnCollisionEnter(Collision collision)
		{
			Ball ball = collision.gameObject.GetComponent<Ball>();
			if (ball == null) return;

			OnBallCollision.Invoke(ball);
		}
	}

}
