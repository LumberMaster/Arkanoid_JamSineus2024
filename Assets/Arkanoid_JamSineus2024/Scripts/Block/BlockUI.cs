using System;
using TMPro;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Block))]
    public class BlockUI : MonoBehaviour
    {

        [SerializeField] private Block _block;
        [SerializeField] private TMP_Text _blockDurabilityText;

		private void OnEnable()
		{
			_block.OnChangeDurability.AddListener(BindOnChangeDurability);
		}

		private void OnDisable()
		{
			_block.OnChangeDurability.RemoveListener(BindOnChangeDurability);
		}


		private void BindOnChangeDurability()
		{
			UpdateUI();
		}

		private void UpdateUI() 
        {
			_blockDurabilityText.text = _block.CurrentDurability.ToString();
		}
    }
}