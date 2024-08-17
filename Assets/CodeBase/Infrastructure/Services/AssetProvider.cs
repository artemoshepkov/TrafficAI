using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class AssetProvider : IService
    {
        public GameObject Instantiate(string path)
        {
            Object prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab) as GameObject;
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            Object prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity) as GameObject;
        }
        
        public GameObject Instantiate(string path, Vector3 at, Quaternion quaternion)
        {
            Object prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, quaternion) as GameObject;
        }
    }
}
