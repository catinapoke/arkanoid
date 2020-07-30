using UnityEngine;

namespace catinapoke.arkanoid
{
    public class EmptySetChecker : MonoBehaviour
    {
        [SerializeField]
        private GameObjectSet set;

        [SerializeField]
        private GameEvent emptySetEvent;

        public void Check()
        {
            if (set.Items.Count == 0)
                emptySetEvent.Raise();
        }
    }
}