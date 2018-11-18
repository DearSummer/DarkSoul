using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace DS
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        private int objCount = 0;
        public int ObjCount
        {
            get { return objCount; }
        }


        private readonly Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();
        private readonly Dictionary<GameObject,string> tagDic = new Dictionary<GameObject, string>();

        private GameObject pool;


        public void Clear()
        {
            foreach (var objQueue in poolDic.Values)
            {
                foreach (var obj in objQueue)
                {
                    Object.Destroy(obj);
                }
            }

            poolDic.Clear();
            tagDic.Clear();

            Object.Destroy(pool);
        }

        public void Return(GameObject obj)
        {
            if (pool == null)
            {
                pool = new GameObject("InstancePool");
            }

            if (obj == null)
                return;

            if (!tagDic.ContainsKey(obj))
                return;


            obj.transform.SetParent(pool.transform);
            obj.SetActive(false);

            string tag = tagDic[obj];
            RemoveMarkObjectTag(obj);

            if (!poolDic.ContainsKey(tag))
                poolDic[tag] = new Queue<GameObject>();

            poolDic[tag].Enqueue(obj);

            objCount--;
        }

        public GameObject Get(string tag)
        {

            GameObject obj = GetFromCache(tag);
            if (obj == null)
                obj = new GameObject(tag);

            MarkOutputObjectTag(tag, obj);
            objCount++;
          
            return obj;
        }

        public GameObject Get(GameObject perfab)
        {
           if(perfab == null)
               return null;

            string tag = perfab.GetInstanceID().ToString();
            GameObject cacheObj = GetFromCache(tag);
            if (cacheObj == null)
            {
                cacheObj = Object.Instantiate(perfab);
                cacheObj.name = perfab.name + "_" + Time.time;
            }

            MarkOutputObjectTag(tag,cacheObj);
            objCount++;
            return cacheObj;
        }

        private GameObject GetFromCache(string tag)
        {
            if (poolDic.ContainsKey(tag) && poolDic[tag].Count > 0)
            {
                GameObject obj = poolDic[tag].Dequeue();
                obj.SetActive(true);
                obj.transform.SetParent(null);
                return obj;
            }

            return null;
        }

        private void MarkOutputObjectTag(string tag, GameObject obj)
        {
            tagDic.Add(obj, tag);
        }

        private void RemoveMarkObjectTag(GameObject obj)
        {
            if (tagDic.ContainsKey(obj))
                tagDic.Remove(obj);
        }
    }
}
