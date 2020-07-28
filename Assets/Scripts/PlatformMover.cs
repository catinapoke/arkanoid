using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace catinapoke.arkanoid
{
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField]
        private Vector2 horizontalBounds;  // X - left corner, Y - right corner
        private Vector2 movingBounds;      // Borders according to platform size
        private BoxCollider2D boxCollider;
        [SerializeField]
        private float speed;

        private void Start()
        {
            Camera camera = Camera.main;
            boxCollider = gameObject.GetComponent<BoxCollider2D>();
            CalculateMovingBounds();
        }

        public void SetHorizontalBounds(Vector2 bounds)
        {
            horizontalBounds = bounds;
        }

        private void CalculateMovingBounds()
        {
            movingBounds = new Vector2(horizontalBounds.x + boxCollider.bounds.extents.x, horizontalBounds.y - boxCollider.bounds.extents.x);
        }

        private void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal");
            gameObject.transform.position = new Vector3(
                Mathf.Clamp(gameObject.transform.position.x + x * speed * Time.fixedDeltaTime, movingBounds.x, movingBounds.y),
                gameObject.transform.position.y,
                gameObject.transform.position.z
                );
        }
    }
}