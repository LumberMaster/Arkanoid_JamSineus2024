using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{

	public class Level : MonoBehaviour
	{
		private List<Block> _blocks = new List<Block>();
		public List<Block> Blocks { get => _blocks; protected set => _blocks = value; }



		public UnityEvent<Block>  OnBreakBlock = new UnityEvent<Block> ();


		private void Awake()
		{
			Blocks = new List<Block>(FindObjectsOfType<Block>());
		}

		private void OnEnable()
		{
			Load();
		}

		private void OnDisable()
		{
			Unload();
		}


		public void Load() 
		{

			foreach (Block block in Blocks)
			{
				block.OnBreak.AddListener(BindOnBreakBlock);
			}

		}

		public void Unload()
		{

			foreach (Block block in Blocks)
			{
				block.OnBreak.RemoveListener(BindOnBreakBlock);
				block.Destroy();
			}
		}

		private void BindOnBreakBlock(Block block)
		{
			block.OnBreak.RemoveListener(BindOnBreakBlock);
			Blocks.Remove(block);
			OnBreakBlock.Invoke(block);
		}

	}

}