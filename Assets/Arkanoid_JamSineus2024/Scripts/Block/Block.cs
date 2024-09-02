using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
	[SerializeField] private float _duarability;
	public float Duarability
	{
		get => _duarability; 
		protected set
		{
			_duarability = value;
			OnChangeDurability.Invoke();
		}
	}

	public UnityEvent OnChangeDurability = new UnityEvent();

	private void Start()
	{
		OnChangeDurability.Invoke();
	}

	public void AddDamage(float damage) 
	{
		Duarability -= damage;

		if(Duarability <= 0) Destroy(gameObject);
	}

}
