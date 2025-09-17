using StShoot.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace StShoot.OutGame.UIs.MenuItems
{
    public class MenuItem : MonoBehaviour
    {
        [SerializeField] private Text _label;
        [SerializeField] private Image _textBackGorund;
        [SerializeField] private MenuItem _left;
        [SerializeField] private MenuItem _right;
        
        [SerializeField] private UnityEvent onDecide;

        [SerializeField] private GameLevel _level;

        private bool _selected;

        public MenuItem Left => _left;
        public MenuItem Right => _right;
        public bool Selected => _selected;
        public GameLevel Level => _level;

        private void Awake()
        {
            SetSelected(false);
        }

        public void SetSelected(bool selected)
        {
            _selected = selected;
            _label.fontStyle = selected ? FontStyle.Bold : FontStyle.Normal;
            _textBackGorund.enabled = selected;
        }

        public void DecideItem()
        {
            onDecide?.Invoke();
        }
    }
}
