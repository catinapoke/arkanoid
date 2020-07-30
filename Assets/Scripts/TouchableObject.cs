using UnityEngine;

namespace catinapoke.arkanoid
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TouchableObject : MonoBehaviour, ITouchable
    {
        [SerializeField]
        protected GameObjectSet Set;

        protected virtual void Start()
        {
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            Set.Add(gameObject);
        }

        public virtual void Hit(GameObject touched)
        { }

        protected virtual void OnDestroy()
        {
            Set.Remove(gameObject);
        }
    }
}