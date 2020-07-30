using UnityEngine;

namespace catinapoke.arkanoid
{
    public class AdditionalBallBonus : BaseBonus
    {
        [SerializeField]
        private GameObjectSet ballSet;

        [SerializeField]
        private GameObject ballPrefab;

        protected override void Activate(GameObject touched)
        {
            GameObject ball = ballSet.Items[UnityEngine.Random.Range(0, ballSet.Items.Count)];
            Debug.Log($"Additional ball spawned at {ball.transform.position}");
            GameObject spawnedBall = Instantiate(ballPrefab, ball.transform.position, Quaternion.identity);
            spawnedBall.GetComponent<BallMover>().CopySpeed(ball.GetComponent<BallMover>());
        }
    }
}