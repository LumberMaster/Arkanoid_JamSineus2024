using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
	[SerializeField] private float _currentDurability;
	public float CurrentDurability
	{
		get => _currentDurability; 
		protected set
		{
			_currentDurability = value;
			_blockMaterial.color = Color.Lerp(_lowDurabilityColor, _maxDurabilityColor, CurrentDurability / MaxDurability);
			OnChangeDurability.Invoke();
		}
	}

	[SerializeField] private float _maxDurability;
	public float MaxDurability
	{
		get => _maxDurability;
		protected set
		{
			_maxDurability = value;
		}
	}

	[SerializeField] private Color _lowDurabilityColor;
	[SerializeField] private Color _maxDurabilityColor;

	private Material _blockMaterial;

	public UnityEvent OnChangeDurability = new UnityEvent();
	public UnityEvent<Block> OnBreak= new UnityEvent<Block>();

	private void Start()
	{
		_blockMaterial = GetComponent<MeshRenderer>().materials[0];
		CurrentDurability = MaxDurability;
	}

	public void AddDamage(float damage) 
	{
		CurrentDurability -= damage;

		if (CurrentDurability <= 0) Destroy();
	}

	public void Destroy()
	{

		OnBreak.Invoke(this);
		// TODO: В дальнейшем поменять дестрой на скрытие в пуле объектов
		gameObject.SetActive(false);
		//Destroy(gameObject);
	}
}
