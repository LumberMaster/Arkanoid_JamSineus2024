using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour
{
	[SerializeField] private float _duarability;
	public float Duarability { get => _duarability; set => _duarability = value; }
}
