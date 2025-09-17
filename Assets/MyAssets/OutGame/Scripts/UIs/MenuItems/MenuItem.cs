using UnityEngine;
using UnityEngine.UI;

namespace StShoot.OutGame.UIs.MenuItems
{
    public class MenuItem : MonoBehaviour
    {
        [SerializeField] private Text _label;
        [SerializeField] private Image _textBackGorund;
        [SerializeField] private MenuItem _left;
        [SerializeField] private MenuItem _right;

        private bool _selected;

        public MenuItem Left => _left;
        public MenuItem Right => _right;
        public bool Selected => _selected;

        public void SetSelected(bool selected)
        {
            _selected = selected;
            _label.fontStyle = selected ? FontStyle.Bold : FontStyle.Normal;
            _textBackGorund.enabled = selected;
        }

        public void DecideItem()
        {
            
        }
    }
}
