using UnityEngine;
using R3;
using StShoot.InGame.Players;

namespace StShoot.InGame.GameManagers
{
    /// <summary>
    /// 音楽のマネージャークラス
    /// </summary>
    public class MusicManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;
        
        [SerializeField]
        private AudioSource _seAudioSource;
        
        [SerializeField]
        private AudioClip _bgmClip;
        
        [SerializeField]
        private PlayerCore _playerCore;
        
        [SerializeField]
        private AudioClip _deadClip;
        
        public void Init()
        {
            _audioSource.clip = _bgmClip;
            
            MainGameManager.Instance.CurrentGameState.Subscribe(state =>
            {
                if (state == GameState.Game)
                {
                    _audioSource.Play();
                }
                else
                {
                    _audioSource.Stop();
                }
            });
            
            _playerCore.IsDead
                .Where(isDead => isDead)
                .Subscribe(_ =>
                {
                    _seAudioSource.PlayOneShot(_deadClip);
                });
        }
    }
}
