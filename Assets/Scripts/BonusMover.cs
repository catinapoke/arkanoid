using UnityEngine;

namespace catinapoke.arkanoid
{
    public class BonusMover : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private BoxCollider2D boxCollider;

        [SerializeField]
        private GameObjectSet wallSet;

        private void Start()
        {
            boxCollider = gameObject.GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            gameObject.transform.Translate(Vector2.down * speed * Time.fixedDeltaTime);

            foreach (GameObject wall in wallSet.Items)
            {
                if (GamePhysics.CheckCollision(boxCollider, wall.GetComponent<BoxCollider2D>()))
                    wall.GetComponent<TouchableObject>().Hit(gameObject);
            }
        }
    }
}