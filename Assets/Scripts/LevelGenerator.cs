using UnityEngine;

namespace catinapoke.arkanoid
{
    public class LevelGenerator : MonoBehaviour
    {
        // Playground
        [Header("Level variables")]
        [SerializeField]
        private Vector2 levelSize;

        [SerializeField]
        private int levelNumber;

        [Space]

        // Scene objects prefabs
        [Header("Scene prefabs")]
        [SerializeField]
        private GameObject platform;

        [SerializeField]
        private GameObject ball;

        [SerializeField]
        private GameObject wall;

        [SerializeField]
        private GameObject deathZone;

        [SerializeField]
        private GameObject[] levelBricks;

        [Space]

        // Rows settings
        [Header("Rows settings")]
        [SerializeField]
        private float rowHeight;

        [SerializeField]
        private int rowCount;

        [SerializeField]
        private int bricksInRow;

        [Space]

        // Other scene objects settings
        [Header("Other scene objects settings")]
        [SerializeField]
        private float wallThickness;

        [SerializeField]
        private float platformLevel;

        [Space]

        [Header("Runtime objects")]
        [SerializeField]
        private GameObjectSet wallSet;

        [SerializeField]
        private GameObjectSet brickSet;

        [SerializeField]
        private GameObjectSet bonusSet;

        [SerializeField]
        private GameObjectSet ballSet;

        [SerializeField]
        private RuntimeGameObject runtimePlatform;

        private void Start()
        {
            levelNumber = Mathf.Clamp(levelNumber, 0, levelBricks.Length);
            SpawnWalls();
            SpawnBricks(levelNumber);
            SpawnActive();
        }

        private void ClearLevel()
        {
            foreach (GameObject item in brickSet.Items)
            {
                Destroy(item);
            }
            foreach (GameObject item in bonusSet.Items)
            {
                Destroy(item);
            }
            foreach (GameObject item in ballSet.Items)
            {
                Destroy(item);
            }
            Destroy(runtimePlatform.Item);
        }

        public void ResetLevel()
        {
            ClearLevel();
            SpawnBricks(levelNumber);
            SpawnActive();
        }

        public void SpawnNextLevel()
        {
            levelNumber = Mathf.Clamp(++levelNumber, 0, levelBricks.Length);
            ResetLevel();
        }

        public void ResetMatch()
        {
            levelNumber = 0;
            ResetLevel();
        }

        private void SpawnWalls()
        {
            GameObject wallObject;

            // Left
            wallObject = Instantiate(wall, new Vector3(-(levelSize.x + wallThickness) / 2, 0, 0), Quaternion.identity);
            wallObject.transform.localScale = new Vector3(wallThickness, levelSize.y + 2 * wallThickness, 0);

            // Right
            wallObject = Instantiate(wall, new Vector3((levelSize.x + wallThickness) / 2, 0, 0), Quaternion.identity);
            wallObject.transform.localScale = new Vector3(wallThickness, levelSize.y + 2 * wallThickness, 0);

            // Top
            wallObject = Instantiate(wall, new Vector3(0, (levelSize.y + wallThickness) / 2, 0), Quaternion.identity);
            wallObject.transform.localScale = new Vector3(levelSize.x + 2 * wallThickness, wallThickness, 0);

            // Bottom
            wallObject = Instantiate(deathZone, new Vector3(0, -(levelSize.y + wallThickness) / 2, 0), Quaternion.identity);
            wallObject.transform.localScale = new Vector3(levelSize.x + 2 * wallThickness, wallThickness, 0);
        }

        private void SpawnBricks(int levelNum)
        {
            float brickWidth = levelSize.x / bricksInRow;
            float startPosX = (-levelSize.x + brickWidth) / 2;
            Vector2 brickPosition = new Vector2(startPosX, (levelSize.y - rowHeight) / 2);

            for (int k = 0; k < rowCount; k++)
            {
                for (int i = 0; i < bricksInRow; i++)
                {
                    GameObject brickObject = Instantiate(levelBricks[levelNum], new Vector3(brickPosition.x, brickPosition.y, 0), Quaternion.identity);
                    brickObject.transform.localScale = new Vector3(brickWidth, rowHeight, 1);
                    brickPosition.x += brickWidth;
                }
                brickPosition.x = startPosX;
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
        }
    }
}