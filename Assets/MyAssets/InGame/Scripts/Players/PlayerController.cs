using R3;
using UnityEngine;

namespace StShoot.InGame.Players
{
    public class PlayerController : BasePlayerComponent
    {
        private float _moveSpeed;
        private float _moveLowerSpeed = 3f;
        
        private const float SideMargin = 0.015f;
        
        protected override void OnInitialize()
        {
            _moveSpeed =6.5f;
            _moveLowerSpeed = 3f;
        }

        protected override void OnStart()
        {

            InGameInputEventProvider.MoveDirection
                .Where(_ => !(InGameInputEventProvider.MoveDirection.CurrentValue.normalized == new Vector2(0f, 0f)))
                .Subscribe(_ =>
                {
                    var direction = InGameInputEventProvider.MoveDirection.CurrentValue.normalized;
                    var newPosition = transform.position + new Vector3(direction.x, direction.y, 0f) * _moveSpeed * Time.deltaTime;

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
