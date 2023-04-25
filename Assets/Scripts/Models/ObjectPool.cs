using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Models
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        private readonly Dictionary<string, Queue<GameObject>> _dicPool = new();

        private GameObject _pool;

        public GameObject  GetPool(GameObject prefab)
        {
             GameObject go;
            if (!_dicPool.ContainsKey(prefab.name) || _dicPool[prefab.name].Count ==0)
            {

               if (_pool == null)
               {
                    _pool = new GameObject("ObjectPool");
               }
                var childPool = GameObject.Find("ObjectPool/" + prefab.name + "Pool");
               if (childPool == null)
               {
                    childPool = new GameObject(prefab.name + "Pool");
                    childPool.transform.SetParent(_pool.transform);
               }
               go = Object.Instantiate(prefab, childPool.transform, true);
               PushPool(go);
            }
            go =  _dicPool[prefab.name].Dequeue();
            go.SetActive(true);
            return go;
        }

        public  void PushPool(GameObject pre)
        {
            var name = pre.name.Replace("(Clone)",string.Empty);
            if (!_dicPool.ContainsKey(name))
            {
                _dicPool.Add(name, new Queue<GameObject>());
            }
            pre.SetActive(false);
            _dicPool[name].Enqueue(pre);
        }
    }
}
