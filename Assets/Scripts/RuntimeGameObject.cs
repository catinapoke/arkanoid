using UnityEngine;

namespace catinapoke.arkanoid
{
    [CreateAssetMenu(menuName = "ScriptableObjects/RuntimeGameObject")]
    public class RuntimeGameObject : ScriptableObject
    {
        [SerializeReference]
        public GameObject Item;

        public void Set(GameObject t)
        {
            if (!System.Object.ReferenceEquals(Item, t))
                Item = t;
        }

        public void Remove(GameObject t)
        {
            if (System.Object.ReferenceEquals(Item, t))
                Item = null;
        }
    }
}