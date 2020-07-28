using System;
using UnityEngine;

namespace catinapoke.arkanoid
{
    public class BallMover : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private Vector2 moveDirection;

        private CircleCollider2D ballCollider;

        [SerializeField]
        public GameObject playerPad { set; private get; }

        [SerializeField]
        private BouncableList bouncableList;

        private void Start()
        {
            ballCollider = this.gameObject.GetComponent<CircleCollider2D>();
            moveDirection = new Vector2(UnityEngine.Random.Range(-1, 1), 1).normalized;
        }

        private void FixedUpdate()
        {
            gameObject.transform.Translate(speed * moveDirection * Time.fixedDeltaTime);

            foreach (GameObject boxObject in bouncableList.Items)
            {
                BoxCollider2D boxCollider = boxObject.GetComponent<BoxCollider2D>();

                Collision collision = GamePhysics.CheckCollision(ballCollider, boxCollider);
                if (collision.occurred)
                {
                    Debug.Log($"Collision {collision.direction} {collision.difference}", boxObject);

                    GamePhysics.ResolveCollision(collision, ballCollider);

                    // Reflect direction
                    if (collision.direction == Direction.Left || collision.direction == Direction.Right) // Horizontal collision
                        moveDirection.x = -moveDirection.x;
                    else // Vertical collision
                        moveDirection.y = -moveDirection.y;

                    boxObject.GetComponent<IBouncable>().Hit();
                }

                // TODO: Check Power-ups

                // Platform collision
                BoxCollider2D playerPadCollider = playerPad.GetComponent<BoxCollider2D>();
                Debug.Log($"Center - {playerPad.transform.position} Extents - {playerPadCollider.bounds.extents}");
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
        }
    }
}