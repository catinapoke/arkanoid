using System.Collections.Generic;
using UnityEngine;

namespace catinapoke.arkanoid
{
    public class BallMover : MonoBehaviour
    {
        [Header("Move parameters")]

        [SerializeField]
        private float speed;

        [SerializeField]
        private float minSpeed;

        [SerializeField]
        private float maxSpeed;

        [SerializeField]
        private Vector2 moveDirection;

        private CircleCollider2D ballCollider;

        [Space]

        [Header("Runtime objects")]

        [SerializeField]
        private GameObjectSet brickSet;

        [SerializeField]
        private GameObjectSet wallSet;

        [SerializeField]
        private GameObjectSet ballSet;

        [SerializeField]
        private RuntimeGameObject runtimePlayerPad;

        [Space]

        [Header("Events")]

        [SerializeField]
        private GameEvent BallDestroy;

        private void Start()
        {
            ballCollider = this.gameObject.GetComponent<CircleCollider2D>();
            moveDirection = new Vector2(UnityEngine.Random.Range(-1, 1), 1).normalized;
            ballSet.Add(gameObject);
            if (minSpeed > maxSpeed) // Swap
            {
                minSpeed = maxSpeed + minSpeed;
                maxSpeed = minSpeed - maxSpeed;
                minSpeed = minSpeed - maxSpeed;
            }
        }

        private void FixedUpdate()
        {
            gameObject.transform.Translate(speed * moveDirection * Time.fixedDeltaTime);

            Collision collision;

            BounceFromBoxes(brickSet.Items, false);
            BounceFromBoxes(wallSet.Items, true);

            // Platform collision
            BoxCollider2D playerPadCollider = runtimePlayerPad.Item.GetComponent<BoxCollider2D>();

            collision = GamePhysics.CheckCollision(ballCollider, playerPadCollider);
            if (collision.occurred)
            {
                if (collision.direction == Direction.Up || collision.direction == Direction.Down)
                {
                    float centerBoard = playerPadCollider.gameObject.transform.position.x;
                    float distance = gameObject.transform.position.x - centerBoard;

                    moveDirection.x = distance / playerPadCollider.bounds.extents.x;
                    moveDirection.y = -moveDirection.y;
                    moveDirection = moveDirection.normalized;

                    GamePhysics.ResolveCollision(collision, ballCollider);
                }
                else // Side-collision → ball is missed
                    moveDirection.x = -moveDirection.x;
            }
        }

        // Handle ball bounce from static boxes
        private void BounceFromBoxes(List<GameObject> gameObjects, bool checkDeathZone)
        {
            Collision collision;
            foreach (GameObject boxObject in gameObjects)
            {
                BoxCollider2D boxCollider = boxObject.GetComponent<BoxCollider2D>();

                collision = GamePhysics.CheckCollision(ballCollider, boxCollider);
                if (collision.occurred)
                {
                    if (checkDeathZone && boxObject.CompareTag("DeathZone"))
                    {
                        Destroy(this.gameObject);
                        return;
                    }

                    GamePhysics.ResolveCollision(collision, ballCollider);

                    // Reflect direction
                    if (collision.direction == Direction.Left || collision.direction == Direction.Right) // Horizontal collision
                        moveDirection.x = -moveDirection.x;
                    else // Vertical collision
                        moveDirection.y = -moveDirection.y;

                    boxObject.GetComponent<ITouchable>().Hit(gameObject);
                }
            }
        }

        public void MutiplySpeed(float coefficient)
        {
            speed *= coefficient;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        }

        public void CopySpeed(BallMover otherBall)
        {
            this.speed = otherBall.speed;
        }

        protected virtual void OnDisable()
        {
            ballSet.Remove(gameObject);
            BallDestroy.Raise();
        }
    }
}