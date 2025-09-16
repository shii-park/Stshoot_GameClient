using StShoot.InGame.Scripts.Walls.Interfaces;
using UnityEngine;

namespace StShoot.InGame.Items
{
    public class ItemCollision : MonoBehaviour
    {
        [SerializeField] 
        private ItemPresenter _presenter;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<IWall>() != null)
            {
                _presenter.Model.SetAvailable(true);
            }
        }
    }
}
