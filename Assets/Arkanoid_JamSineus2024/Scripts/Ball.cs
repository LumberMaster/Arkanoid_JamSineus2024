using UnityEngine;
using UnityEngine.Events;

namespace Game 
{
	public class Ball : MonoBehaviour
	{
		[SerializeField] private float _damage = 1.0f;
		[SerializeField] private float _speedMoving;
		[SerializeField] private Vector3 _directionMoving;

		public UnityEvent<Block> OnCollisionBlock = new UnityEvent<Block>();
		public UnityEvent OnDestroy = new UnityEvent();

		private void OnCollisionEnter(Collision collision)
		{
			Block blockBase = collision.gameObject.GetComponent<Block>();
			if (blockBase == null) return;


			blockBase.AddDamage(_damage);
			OnCollisionBlock.Invoke(blockBase);
		}

		public void Destroy()
		{
			Destroy(gameObject);
			OnDestroy.Invoke();
		}
	}
}
