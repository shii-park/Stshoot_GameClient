using StShoot.InGame.GameManagers;
using StShoot.InGame.Items;
using UnityEngine;

namespace StShoot.InGame.Players
{
    public class PlayerCollision : BasePlayerComponent
    {
        protected override void OnInitialize()
        {
        }

        protected override void OnStart()
        {

        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.GetComponent<BaseItem>()?.ApplyEffect(MainGameManager.Instance.CreateItemEffectContext());
        }
    }
}
