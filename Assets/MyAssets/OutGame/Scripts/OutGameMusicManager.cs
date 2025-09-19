using R3;
using StShoot.OutGame.Inputs;
using UnityEngine;

namespace StShoot.OutGame
{
    public class OutGameMusicManager : MonoBehaviour
    {
        [SerializeField]
        private  AudioSource _audioSource;
        
        [SerializeField]
        private AudioSource _seAudioSource;
        
        [SerializeField]
        private AudioClip _bgmClip;
        
        [SerializeField]
        private AudioClip _moveClip;
        [SerializeField]
        private AudioClip _decideClip;
        
        private IOutGameInputEventProvider _inputEventProvider;
        
        void Start()
        {
            _inputEventProvider = GetComponent<IOutGameInputEventProvider>();
            _audioSource.clip = _bgmClip;
            _audioSource.Play();
            
            _inputEventProvider.LeftButtonPushed
                .Where(pushed => pushed)
                .Subscribe(_ =>
                {
                    _seAudioSource.PlayOneShot(_moveClip);
                });
            _inputEventProvider.RightButtonPushed
                .Where(pushed => pushed)
                .Subscribe(_ =>
                {
                    _seAudioSource.PlayOneShot(_moveClip);
                });
            _inputEventProvider.OnDecideButtonPushed
                .Where(pushed => pushed)
                .Subscribe(_ =>
                {
                    _seAudioSource.PlayOneShot(_decideClip);
                });
        }
    }
}
