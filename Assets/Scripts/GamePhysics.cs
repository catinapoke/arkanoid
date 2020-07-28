using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace catinapoke.arkanoid
{
    public enum Direction
    {
        Up = 0,
        Right,
        Down,
        Left
    }

    public struct Collision
    {
        public bool occurred;
        public Direction direction;
        public Vector2 difference;

        public Collision(bool collision, Direction direction, Vector2 difference)
        {
            this.occurred = collision;
            this.direction = direction;
            this.difference = difference;
        }
    }

    static class GamePhysics
    {
        static public Direction VectorDirection(Vector2 target)
        {
            Vector2[] compass = {
                Vector2.up,
                Vector2.right,
                Vector2.down,
                Vector2.left
                };

            float max = 0.0f;
            UInt16 best_match = UInt16.MaxValue;
            for (UInt16 i = 0; i < 4; i++)
            {
                float dot_product = Vector2.Dot(target.normalized, compass[i]);
                if (dot_product > max)
                {
                    max = dot_product;
                    best_match = i;
                }
            }
            return (Direction)best_match;
        }

        static public bool CheckCollision(BoxCollider2D one, BoxCollider2D two) // Box - Box collision
        {
            Vector3 onePosition = one.gameObject.transform.position;
            Vector3 oneExtents  = one.bounds.extents;
            
            Vector3 twoPosition = two.gameObject.transform.position;
            Vector3 twoExtents  = two.bounds.extents;

            bool collisionX = onePosition.x + oneExtents.x >= twoPosition.x - twoExtents.x &&
                              twoPosition.x + twoExtents.x >= onePosition.x - oneExtents.x;

            bool collisionY = onePosition.y + oneExtents.y >= twoPosition.y - twoExtents.y &&
                              twoPosition.y + twoExtents.y >= onePosition.y - oneExtents.y;
            
            return collisionX && collisionY;
        }

        // Circle - Box collision
        static public Collision CheckCollision(CircleCollider2D movableCircle, BoxCollider2D box)
        {
            // Circle info
            Vector2 ballCenter = movableCircle.gameObject.transform.position;
            float ballRadius = movableCircle.bounds.extents.x;
            // Box info
            Vector2 boxCenter = box.bounds.center;
            Vector2 boxExtents = box.bounds.extents;

            // Difference between centers
            Vector2 difference = ballCenter - boxCenter;
            Vector2 clamped = new Vector2(
                Mathf.Clamp(difference.x, -boxExtents.x, boxExtents.x),
                Mathf.Clamp(difference.y, -boxExtents.y, boxExtents.y)
                );

            // Clamped + boxCenter makes the value of box closest to circle
            Vector2 closestPoint = boxCenter + clamped;
            // Retrieve vector between center circle and closest point and check if length < radius
            Vector2 oldDifference = difference;
            difference = closestPoint - ballCenter;

            // Not <= because it's one exactly touch two like when they are at the end of collision resolution
            if (Vector2.SqrMagnitude(difference) < ballRadius * ballRadius)
                return new Collision(true, VectorDirection(difference), difference);
            else
                return new Collision(false, Direction.Up, Vector2.zero);
        }

        // Collision resolution
        static public void ResolveCollision(Collision collision, CircleCollider2D ballCollider)
        {
            Direction dir = collision.direction;
            Vector2 diffVector = collision.difference;
            Transform ballTransform = ballCollider.transform;

            if (dir == Direction.Left || dir == Direction.Right) // Horizontal collision
            {
                // Relocation
                float penetration = ballCollider.bounds.extents.x - Mathf.Abs(diffVector.x); // Radius - difference

                if (dir == Direction.Left)
                    ballTransform.position = new Vector3(
                        ballTransform.position.x + penetration, // Move ball to the right
                        ballTransform.position.y,
                        ballTransform.position.z
                        );
                else
                    ballTransform.position = new Vector3(
                        ballTransform.position.x - penetration, // Move ball to the left;
                        ballTransform.position.y,
                        ballTransform.position.z
                        );
            }
            else // Vertical collision
            {
                float penetration = ballCollider.bounds.extents.y - Mathf.Abs(diffVector.y);

                if (dir == Direction.Down)
                    ballTransform.position = new Vector3(
                        ballTransform.position.x,
                        ballTransform.position.y + penetration, // Back up
                        ballTransform.position.z
                        );
                else
                    ballTransform.position = new Vector3(
                        ballTransform.position.x,
                        ballTransform.position.y - penetration, // Back down;
                        ballTransform.position.z
                        );
            }
        }
    }
}
