using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StShoot.InGame.Players;

namespace StShoot.InGame.UIs
{
    public class PlayerParameterView : MonoBehaviour
    {
        [SerializeField]
        private List<Image> _hearts = new List<Image>();
        [SerializeField]
        private Text _powerText;
        
        public void SetPlayerLifePoint(int lifePoint){
            for (int i = 0; i < _hearts.Count; i++)
            {
                if (i < lifePoint)
                {
                    _hearts[i].enabled = true;
                }
                else
                {
                    _hearts[i].enabled = false;
                }
            }

        }
        
        public void SetPlayerPower(int power, int maxPower)
        {
            var powerText = (power >= maxPower) ? "MAX" : power.ToString("D3");
            _powerText.text = powerText;
        }
    }
}
