using UnityEngine;

namespace catinapoke.arkanoid
{
    public class DeathZone : TouchableObject
    {
        public override void Hit(GameObject touched)
        {
            if (touched.CompareTag("Ball") || touched.CompareTag("Bonus"))
            {
                Destroy(touched, 0);
            }
        }
    }
}