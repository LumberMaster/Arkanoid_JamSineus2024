using UnityEngine;

namespace Game 
{
	public class LavaArea : MonoBehaviour
	{

		private void OnCollisionEnter(Collision collision)
		{
			Ball ball = collision.gameObject.GetComponent<Ball>();
			if (ball == null) return;

			ball.Destroy();
		}
	}

}
