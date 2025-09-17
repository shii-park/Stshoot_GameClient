using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StShoot.InGame.Enemies
{
    /// <summary>
    /// 敵の移動を制御するクラス
    /// </summary>
    public class EnemyMovementController : MonoBehaviour
    {
        [SerializeField]
        private List<Waypoint> _waypoints = new List<Waypoint>();
        
        [SerializeField]
        EnemyPresenter _enemyPresenter;

        private int _currentIndex = 0;
        private float _elapsed = 0f;
        private Vector3 _startPos;

        private void Start()
        {
            if (_waypoints.Count > 0)
            {
                _startPos = transform.position;
                StartCoroutine(MoveRoutine());
            }
        }
        
        /// <summary>
        /// ウェイポイントを設定するメソッド
        /// </summary>
        public void SetWaypoints(List<Waypoint> waypoints)
        {
            _waypoints = waypoints;
            _currentIndex = 0;
            StopAllCoroutines();
            if (_waypoints.Count > 0)
            {
                StartCoroutine(MoveRoutine());
            }
        }

        /// <summary>
        /// 敵の移動を制御するコルーチン
        /// </summary>
        private IEnumerator MoveRoutine()
        {
            while (_currentIndex < _waypoints.Count)
            {
                Waypoint wp = _waypoints[_currentIndex];
                Vector3 from = transform.position;
                Vector3 to = wp.Position;
                float t = 0f;

                while (t < 1f)
                {
                    t += Time.deltaTime / wp.Duration;
// EnemyMovementController.cs の MoveRoutine 内
                    switch (wp.MoveType)
                    {
                        case MoveType.Straight:
                            transform.position = Vector3.Lerp(from, to, t);
                            break;
                        case MoveType.Curve:
                            // デフォルトのカーブ
                            Vector3 control = (from + to) / 2 + Vector3.up * 2f;
                            transform.position = Mathf.Pow(1 - t, 2) * from +
                                                 2 * (1 - t) * t * control +
                                                 Mathf.Pow(t, 2) * to;
                            break;
                        case MoveType.CurveInner:
                        case MoveType.CurveOuter:
                            Vector3 mid = (from + to) / 2;
                            Vector3 dir = (to - from).normalized;
                            Vector3 normal = Vector3.Cross(dir, Vector3.forward);
                            float sign = (wp.MoveType == MoveType.CurveInner) ? -1f : 1f;
                            float offset = 5f;
                            Vector3 control2 = mid + normal * offset * sign;
                            transform.position = Mathf.Pow(1 - t, 2) * from +
                                                 2 * (1 - t) * t * control2 +
                                                 Mathf.Pow(t, 2) * to;
                            break;
                        case MoveType.Wave:
                            Vector3 straight = Vector3.Lerp(from, to, t);
                            float wave = Mathf.Sin(t * Mathf.PI * 4) * 0.5f;
                            transform.position = straight + Vector3.up * wave;
                            break;
                    }

                    yield return null;
                }
                transform.position = to;
                _currentIndex++;
            }
        }
    }
}
