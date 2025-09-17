using UnityEngine;

namespace StShoot.OutGame.UIs.MenuItems
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private MenuItem currentItem;
        
        public MenuItem CurrentItem => currentItem;

        void Start()
        {
            UpdateSelection();
        }

        public void MoveLeft()
        {
            if (currentItem.Left != null)
            {
                currentItem.SetSelected(false);
                currentItem = currentItem.Left;
                UpdateSelection();
            }
        }

        public void MoveRight()
        {
            if (currentItem.Right != null)
            {
                currentItem.SetSelected(false);
                currentItem = currentItem.Right;
                UpdateSelection();
            }
        }

        private void UpdateSelection()
        {
            currentItem.SetSelected(true);
        }
    }
}