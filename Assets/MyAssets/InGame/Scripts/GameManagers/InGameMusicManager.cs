using UnityEngine;
using R3;

namespace StShoot.InGame.GameManagers
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField]
        private  AudioSource _audioSource;
        
        [SerializeField]
        private AudioClip _bgmClip;
        
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
        }
    }
}
