using StShoot.InGame.Scripts.Walls.Interfaces;
using UnityEngine;

namespace StShoot.InGame.Scripts.Players.Bullets
{
    public class BulletCollision : MonoBehaviour
    {
        [SerializeField]
        private BulletPresenter _presenter;
        
        private BulletModel _model => _presenter.Model;
        
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<IWall>() != null)
            {
                _model.SetAvailabl(true);
            }
        }
    }
}