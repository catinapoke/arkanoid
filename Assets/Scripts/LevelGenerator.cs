using UnityEngine;

namespace catinapoke.arkanoid
{
    public class LevelGenerator : MonoBehaviour
    {
        // Playground
        [SerializeField]
        private Vector2 levelSize;

        private int levelNumber;

        // Scene objects
        [SerializeField]
        private GameObject platform;

        [SerializeField]
        private GameObject ball;

        [SerializeField]
        private GameObject wall;

        [SerializeField]
        private GameObject[] levelBricks;

        // Rows settings
        [SerializeField]
        private float rowHeight;

        [SerializeField]
        private int rowCount;

        [SerializeField]
        private int bricksInRow;

        // Other scene objects settings
        [SerializeField]
        private float wallThickness;

        [SerializeField]
        private float platformLevel;

        // TODO: LevelReset, NextLevel

        private void Start()
        {
            levelNumber = 0;
            SpawnWalls();
            SpawnBricks();
            SpawnActive();
        }

        private void SpawnWalls()
        {
            GameObject wallObject;
            wallObject = Instantiate(wall, new Vector3(0, (levelSize.y + wallThickness) / 2, 0), Quaternion.identity); // Top
            wallObject.transform.localScale = new Vector3(levelSize.x + 2 * wallThickness, wallThickness, 0);

            wallObject = Instantiate(wall, new Vector3(0, -(levelSize.y + wallThickness) / 2, 0), Quaternion.identity); // Bottom
            wallObject.transform.localScale = new Vector3(levelSize.x + 2 * wallThickness, wallThickness, 0);

            wallObject = Instantiate(wall, new Vector3(-(levelSize.x + wallThickness) / 2, 0, 0), Quaternion.identity);  // Left
            wallObject.transform.localScale = new Vector3(wallThickness, levelSize.y + 2 * wallThickness, 0);

            wallObject = Instantiate(wall, new Vector3((levelSize.x + wallThickness) / 2, 0, 0), Quaternion.identity); // Right
            wallObject.transform.localScale = new Vector3(wallThickness, levelSize.y + 2 * wallThickness, 0);
        }

        private void SpawnBricks()
        {
            float brickWidth = levelSize.x / bricksInRow;
            float startPosX = (-levelSize.x + brickWidth) / 2;
            Vector2 brickPosition = new Vector2(startPosX, (levelSize.y - rowHeight) / 2);

            for (int k = 0; k < rowCount; k++)
            {
                for (int i = 0; i < bricksInRow; i++)
                {
                    GameObject brickObject = Instantiate(levelBricks[levelNumber], new Vector3(brickPosition.x, brickPosition.y, 0), Quaternion.identity);
                    brickObject.transform.localScale = new Vector3(brickWidth, rowHeight, 1);
                    brickPosition.x += brickWidth;
                }
                brickPosition.x  = startPosX;
                brickPosition.y -= rowHeight;
            }
        }

        private void SpawnActive()
        {
            // Platform
            GameObject platformObject = Instantiate(platform, new Vector3(0, levelSize.y * (platformLevel - 0.5f), 0), Quaternion.identity);
            platform.GetComponent<PlatformMover>().SetHorizontalBounds(new Vector2(-levelSize.x / 2, levelSize.x / 2));

            // Ball
            GameObject ballObject = Instantiate(ball, new Vector3(0, 0, 0), Quaternion.identity);
            ballObject.GetComponent<BallMover>().playerPad = platformObject;
        }
    }
}