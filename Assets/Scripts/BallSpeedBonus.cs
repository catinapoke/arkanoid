using UnityEngine;

namespace catinapoke.arkanoid
{
    public class BallSpeedBonus : BaseBonus
    {
        [SerializeField]
        private GameObjectSet ballSet;

        [SerializeField]
        private Vector2 speedCoefLimits;

        protected override void Activate(GameObject touched)
        {
            float speedCoefficient = UnityEngine.Random.Range(speedCoefLimits.x, speedCoefLimits.y);
            Debug.Log($"Ball speed multiplied by {speedCoefficient}");
            foreach (GameObject ball in ballSet.Items)
            {
                BallMover ballMover = ball.GetComponent<BallMover>();
                ballMover.MutiplySpeed(speedCoefficient);
            }
        }
    }
}