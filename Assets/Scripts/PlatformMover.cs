using UnityEngine;

namespace catinapoke.arkanoid
{
    public class PlatformMover : MonoBehaviour
    {
        [Header("Move parameters")]
        [SerializeField]
        private Vector2 horizontalBounds;  // X - left corner, Y - right corner
        private Vector2 movingBounds;      // Borders according to platform size

        [SerializeField]
        private float maxWidthPercent;     // Max width relatively to bounds

        private BoxCollider2D boxCollider;

        [SerializeField]
        private float speed;

        [Header("Runtime objects")]
        [SerializeField]
        private GameObjectSet bonusSet;

        [SerializeField]
        private RuntimeGameObject runtimeObject;

        private void Start()
        {
            runtimeObject.Set(gameObject);
            boxCollider = gameObject.GetComponent<BoxCollider2D>();
            CalculateMovingBounds(boxCollider.bounds.extents.x);
        }

        public void SetHorizontalBounds(Vector2 bounds)
        {
            horizontalBounds = bounds;
        }

        public void MultiplyScale(float coefficient)
        {
            float predictedValue = gameObject.transform.localScale.x * coefficient;
            predictedValue = Mathf.Min(predictedValue, maxWidthPercent * (horizontalBounds.y - horizontalBounds.x));
            gameObject.transform.localScale = new Vector3(
                predictedValue,
                gameObject.transform.localScale.y,
                gameObject.transform.localScale.z
                );
            // As BoxCollider updates too late, so I use scale for size because they're equal
            CalculateMovingBounds(gameObject.transform.localScale.x / 2);
        }

        private void CalculateMovingBounds(float halfSize)
        {
            movingBounds = new Vector2(horizontalBounds.x + halfSize, horizontalBounds.y - halfSize);
        }

        private void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal");
            gameObject.transform.position = new Vector3(
                Mathf.Clamp(gameObject.transform.position.x + x * speed * Time.fixedDeltaTime, movingBounds.x, movingBounds.y),
                gameObject.transform.position.y,
                gameObject.transform.position.z
                );

            foreach (GameObject bonus in bonusSet.Items)
            {
                BoxCollider2D bonusCollider = bonus.GetComponent<BoxCollider2D>();
                if (GamePhysics.CheckCollision(boxCollider, bonusCollider))
                {
                    bonus.GetComponent<TouchableObject>().Hit(this.gameObject);
                }
            }
        }

        private void OnDestroy()
        {
            runtimeObject.Remove(gameObject);
        }
    }
}