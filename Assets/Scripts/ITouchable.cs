using UnityEngine;

namespace catinapoke.arkanoid
{
    internal interface ITouchable
    {
        void Hit(GameObject touched);
    }
}