using System.Collections.Generic;
using R3;
using StShoot.InGame.GameManagers;
using UnityEngine;

namespace StShoot.InGame.Items
{
    /// <summary>
    /// アイテムのファクトリークラス
    /// </summary>
    public class ItemFactory : MonoBehaviour
    {
        public static ItemFactory Instance { get; private set; }

        [SerializeField]
        private List<GameObject> _itemPrefabs;

        private Dictionary<string, List<GameObject>> _itemPools = new Dictionary<string, List<GameObject>>();
        private Dictionary<string, GameObject> _itemsParents = new Dictionary<string, GameObject>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            foreach (var prefab in _itemPrefabs)
            {
                _itemPools.Add(prefab.name, new List<GameObject>());
                var parent = new GameObject($"{prefab.name}_ItemsParent");
                _itemsParents.Add(prefab.name, parent);
            }
        }
        
        private void Start()
        {
            MainGameManager.Instance.CurrentGameState
                .Where(state => state != GameState.Game)
                .Subscribe(_ =>
                {
                    RemoveAllItems();
                });
        }

        /// <summary>
        /// アイテムを生成するメソッド
        /// </summary>
        /// <param name="itemName">生成するアイテムの名前</param>
        /// <param name="spawnPosition">生成位置</param>
        public GameObject Create(string itemName, Vector3 spawnPosition)
        {
            if(MainGameManager.Instance.CurrentGameState.CurrentValue != GameState.Game){ return null;}
            
            if (!_itemPools.ContainsKey(itemName))
            {
                Debug.LogError($"ItemFactory: 指定されたアイテムの名前が存在しません。{itemName}");
                return null;
            }

            var parentObj = _itemsParents[itemName];
            GameObject item = null;
            foreach (var i in _itemPools[itemName])
            {
                if (!i.activeInHierarchy)
                {
                    item = i;
                    break;
                }
            }

            if (item == null)
            {
                var prefab = _itemPrefabs.Find(p => p.name == itemName);
                if (prefab == null)
                {
                    Debug.LogError($"ItemFactory: 指定されたアイテムの名前が存在しません。{itemName}");
                    return null;
                }
                item = Instantiate(prefab, spawnPosition, Quaternion.identity, parentObj.transform);
                _itemPools[itemName].Add(item);
            }
            else
            {
                item.transform.SetParent(parentObj.transform, false);
                item.transform.position = spawnPosition;
                item.SetActive(true);
            }

            item.GetComponent<BaseItem>()?.Init();

            return item;
        }
        
        /// <summary>
        /// アイテムを生成するメソッド
        /// </summary>
        /// <param name="itemName">生成するアイテムの名前</param>
        /// <param name="spawnPosition">生成位置</param>
        public GameObject CreateRandom(Vector3 spawnPosition)
        {
            if(MainGameManager.Instance.CurrentGameState.CurrentValue != GameState.Game){ return null;}
            
            var itemName = _itemPrefabs[Random.Range(0, _itemPrefabs.Count)].name;
            if (!_itemPools.ContainsKey(itemName))
            {
                Debug.LogError($"ItemFactory: 指定されたアイテムの名前が存在しません。{itemName}");
                return null;
            }

            var parentObj = _itemsParents[itemName];
            GameObject item = null;
            foreach (var i in _itemPools[itemName])
            {
                if (!i.activeInHierarchy)
                {
                    item = i;
                    break;
                }
            }

            if (item == null)
            {
                var prefab = _itemPrefabs.Find(p => p.name == itemName);
                if (prefab == null)
                {
                    Debug.LogError($"ItemFactory: 指定されたアイテムの名前が存在しません。{itemName}");
                    return null;
                }
                item = Instantiate(prefab, spawnPosition, Quaternion.identity, parentObj.transform);
                _itemPools[itemName].Add(item);
            }
            else
            {
                item.transform.SetParent(parentObj.transform, false);
                item.transform.position = spawnPosition;
                item.SetActive(true);
            }

            item.GetComponent<BaseItem>()?.Init();

            return item;
        }


        /// <summary>
        /// 全てのアイテムを非アクティブにするメソッド
        /// </summary>
        public void RemoveAllItems()
        {
            foreach (var itemPools in _itemPools.Values)
            {
                foreach (var item in itemPools)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}
