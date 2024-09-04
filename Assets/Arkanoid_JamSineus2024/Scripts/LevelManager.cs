using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game 
{

	public class LevelManager : MonoBehaviour
	{
		public Level CurrentLevel { get; protected set; }

		[SerializeField] private List<Level> _levels = new List<Level>();
		public List<Level> Levels { get => _levels; set => _levels = value; }

		public Transform _containerLevelObject;


		// Да простит меня бог за большое кол-во UnityEvent с ломанием блоков
		public UnityEvent<Block> OnBreakBlock = new UnityEvent<Block>();


		public void LoadLevel(int levelNumber) 
		{
			if (CurrentLevel) UnloadLevel();

			CurrentLevel = Instantiate(Levels[levelNumber], _containerLevelObject.localPosition, _containerLevelObject.localRotation, _containerLevelObject);
			CurrentLevel.Load();
			CurrentLevel.OnBreakBlock.AddListener(BindOnBreakBlock);
		}

		public void UnloadLevel()
		{
			CurrentLevel.OnBreakBlock.RemoveListener(BindOnBreakBlock);
			CurrentLevel.Unload();
			CurrentLevel = null;
		}

		private void BindOnBreakBlock(Block block) => OnBreakBlock.Invoke(block);



	}

}
