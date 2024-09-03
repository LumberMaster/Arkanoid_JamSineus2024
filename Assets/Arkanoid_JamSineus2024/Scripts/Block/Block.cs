using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
	public UnityEvent<Block> OnBreak= new UnityEvent<Block>();

	private void Start()
	{
		OnChangeDurability.Invoke();
	}

	public void AddDamage(float damage) 
	{
		Duarability -= damage;

		if (Duarability <= 0) 
		{
			OnBreak.Invoke(this);
			Destroy(gameObject);
		}
	}

}
