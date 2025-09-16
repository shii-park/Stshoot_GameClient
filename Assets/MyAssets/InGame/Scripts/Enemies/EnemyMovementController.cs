using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StShoot.InGame.Enemies
{
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
                    switch (wp.MoveType)
                    {
                        case MoveType.Straight:
                            transform.position = Vector3.Lerp(from, to, t);
                            break;
                        case MoveType.Curve:
                            // ベジェ曲線（1点制御点）でカーブ
                            Vector3 control = (from + to) / 2 + Vector3.up * 2f;
                            transform.position = Mathf.Pow(1 - t, 2) * from +
                                2 * (1 - t) * t * control +
                                Mathf.Pow(t, 2) * to;
                            break;
                        case MoveType.Wave:
                            // 直線移動＋sin波
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
            
            _enemyPresenter.Model.Die();
        }
    }
}
