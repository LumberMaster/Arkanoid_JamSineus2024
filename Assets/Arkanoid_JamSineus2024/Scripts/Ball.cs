using UnityEngine;
using UnityEngine.Events;

namespace Game 
{
	public class Ball : MonoBehaviour
	{
		[SerializeField] private float _speedMoving;
		[SerializeField] private Vector3 _directionMoving;

		public UnityEvent<BlockBase> OnCollisionBlock = new UnityEvent<BlockBase>();

		private void OnCollisionEnter(Collision collision)
		{
			BlockBase blockBase = collision.gameObject.GetComponent<BlockBase>();
			if (blockBase == null) return;

			OnCollisionBlock.Invoke(blockBase);
		}
	}
}
