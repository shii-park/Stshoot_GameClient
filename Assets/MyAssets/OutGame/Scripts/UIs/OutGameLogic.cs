using System.Collections;
using StShoot.OutGame.Inputs;
using UnityEngine;
using R3;

namespace StShoot.OutGame.UIs
{
    public class OutGameLogic : MonoBehaviour
    {
        private IOutGameInputEventProvider _outGameInputEventProvider;
        public IOutGameInputEventProvider OutGameInputEventProvider {get => _outGameInputEventProvider;}
        
        [SerializeField]
        private OutGameUI _outGameUI;
        
        void Start()
        {
            _outGameInputEventProvider = GetComponent<IOutGameInputEventProvider>();
            StartCoroutine(WaitStart());
        }

        IEnumerator WaitStart()
        {
            yield return new WaitForSeconds(1f);

            OutGameInputEventProvider.OnDecideButtonPushed
                .Skip(1)
                .Take(1)
                .Subscribe(_ =>
                {
                    _outGameUI.StartTitleAnimation();
                });
            
        }
    }
}
