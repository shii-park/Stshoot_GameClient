using System.Collections;
using StShoot.OutGame.Inputs;
using UnityEngine;
using R3;
using StShoot.Common;
using StShoot.Common.Scripts;
using StShoot.OutGame.UIs.MenuItems;

namespace StShoot.OutGame.UIs
{
    public class OutGameLogic : MonoBehaviour
    {
        private IOutGameInputEventProvider _outGameInputEventProvider;
        public IOutGameInputEventProvider OutGameInputEventProvider {get => _outGameInputEventProvider;}
        
        [SerializeField]
        private OutGameUI _outGameUI;
        
        [SerializeField]
        private MenuManager _menuManager;
        
        private bool _canSelectMenu = false;
        
        private bool _canDecide = false;
        
        private GameSetting _gameSetting;
        
        [SerializeField]
        private GameRoom _gameRoom;
        
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
                    _canDecide = false;
                    ActivateMenu();
                    StartCoroutine(WaitForDecide());
                });
        }
        
        private void ActivateMenu()
        {
            _outGameUI.ShowMenuItems();
            _canSelectMenu = true;
            
            OutGameInputEventProvider.LeftButtonPushed
                .Skip(1)
                .Where(_ => _canSelectMenu)
                .Subscribe(_ =>
                {
                    _menuManager.MoveLeft();
                    _canSelectMenu = false;
                    StartCoroutine(WaitForNextAction());
                });
            
            OutGameInputEventProvider.RightButtonPushed
                .Skip(1)
                .Where(_ => _canSelectMenu)
                .Subscribe(_ =>
                {
                    _menuManager.MoveRight();
                    _canSelectMenu = false;
                    StartCoroutine(WaitForNextAction());
                });
            
            OutGameInputEventProvider.OnDecideButtonPushed
                .Where(_ => _canDecide)
                .Skip(1)
                .Take(1)
                .Subscribe(_ =>
                {
                    _menuManager.CurrentItem.DecideItem();
                    _canDecide = false;
                    _outGameUI.ShowRoomID(_gameRoom.RoomId);
                    StartCoroutine(WaitForDecide());
                });
        }
        
        public void SetGameSetting()
        {
            _gameSetting = new GameSetting(_menuManager.CurrentItem.Level, _gameRoom.RoomId);
            
            OutGameInputEventProvider.OnDecideButtonPushed
                .Where(_ => _canDecide)
                .Skip(1)
                .Take(1)
                .Subscribe(_ =>
                {
                    SceneTransitionManager.Instance.LoadScene("InGame", _gameSetting, null);
                    _canDecide = false;
                    StartCoroutine(WaitForDecide());
                });
        }
        
        private IEnumerator WaitForNextAction()
        {
            yield return new WaitForSeconds(0.2f);
            _canSelectMenu = true;
        }
        
        private IEnumerator WaitForDecide()
        {
            yield return new WaitForSeconds(1f);
            _canDecide = true;
        }
    }
}
