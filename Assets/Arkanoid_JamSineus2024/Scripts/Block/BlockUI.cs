using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    public class BlockUI : MonoBehaviour
    {

        [SerializeField] private BlockBase _block;
        [SerializeField] private TMP_Text _blockDurabilityText;
    }
}