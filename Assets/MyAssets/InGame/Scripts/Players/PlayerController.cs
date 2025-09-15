using R3;
using UnityEngine;

namespace StShoot.InGame.Players
{
    /// <summary>
    /// プレイヤーの移動を制御するコンポーネント
    /// </summary>
    public class PlayerController : BasePlayerComponent
    {
        private float _moveSpeed;
        private float _moveLowerSpeed = 3f;
        
        private const float SideMargin = 0.015f;
        
        protected override void OnInitialize()
        {

        }

        protected override void OnStart()
        {
            _moveSpeed = 6.5f;
            _moveLowerSpeed = 3f;
            
            InGameInputEventProvider.MoveDirection
                .Where(_ => !(InGameInputEventProvider.MoveDirection.CurrentValue.normalized == new Vector2(0f, 0f)) && PlayerCore.IsDead.CurrentValue == false)
                .Subscribe(_ =>
                {
                    var speed = (InGameInputEventProvider.OnSlowPushed.CurrentValue) ? _moveLowerSpeed : _moveSpeed;
                    var direction = InGameInputEventProvider.MoveDirection.CurrentValue.normalized;
                    var newPosition = transform.position + new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime;

                    var mainCamera = Camera.main;
                    if (mainCamera == null) return;
                    
                    // 画面外に出ないように制限
                    var viewportPosition = mainCamera.WorldToViewportPoint(newPosition);
                    viewportPosition.x = Mathf.Clamp(viewportPosition.x, SideMargin, 1f - SideMargin);
                    viewportPosition.y = Mathf.Clamp(viewportPosition.y, SideMargin, 1f - SideMargin);
                    newPosition = mainCamera.ViewportToWorldPoint(viewportPosition);

                    transform.position = newPosition;
                });
        }
    }
}
