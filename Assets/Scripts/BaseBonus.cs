using UnityEngine;

namespace catinapoke.arkanoid
{
    // Base class for bonus activator
    public class BaseBonus : TouchableObject
    {
        public override void Hit(GameObject touched)
        {
            if (touched.CompareTag("Player"))
            {
                Activate(touched);
                Destroy(gameObject);
            }
        }

        protected virtual void Activate(GameObject touched)
        { }
    }
}