using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;

namespace Game 
{
	public class Ball : MonoBehaviour
	{
		[SerializeField] private float _damage = 1.0f;
		[SerializeField] private Rigidbody _rigidBody;
		[SerializeField] private AudioSource _source;
		[SerializeField] private AudioClip _clipCollision;
		public Rigidbody RigidBody { get => _rigidBody; protected set => _rigidBody = value; }

		public UnityEvent<Block> OnCollisionBlock = new UnityEvent<Block>();
		public UnityEvent OnDestroy = new UnityEvent();

		private void OnCollisionEnter(Collision collision)
		{
			_source.PlayOneShot(_clipCollision);

			Block blockBase = collision.gameObject.GetComponent<Block>();
			if (blockBase == null) return;

			blockBase.AddDamage(_damage);
			OnCollisionBlock.Invoke(blockBase);
		}

		public void Destroy()
		{
			// TODO: В дальнейшем поменять дестрой на скрытие в пуле объектов
			Destroy(gameObject);
			OnDestroy.Invoke();
		}
	}
}
