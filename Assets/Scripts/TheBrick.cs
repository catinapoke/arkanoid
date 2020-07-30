using UnityEngine;

namespace catinapoke.arkanoid
{
    /*
        There was a way to do inheritance: TouchableObject → OneLifeBrick → Brick with several lives → Brick with lives and bonuses
        But if I use one Brick class, I get rid of code parts duplication in Hit method
     */

    public class TheBrick : TouchableObject
    {
        [SerializeField]
        private GameEvent BrickDestroy;

        [SerializeField]
        protected int maxLives;

        protected int lives;

        [SerializeField]
        private GameObject[] bonusesPrefabs;

        [SerializeField]
        private float bonusDropRate;

        protected override void Start()
        {
            base.Start();
            lives = UnityEngine.Random.Range(1, maxLives + 1);
            bonusDropRate = Mathf.Clamp(bonusDropRate, 0.0f, 1.0f);
        }

        public override void Hit(GameObject touched)
        {
            if (touched.CompareTag("Ball"))
            {
                lives--;
                if (lives < 1)
                {
                    // Not just .value because I need exclusive 1.0
                    if (bonusesPrefabs.Length != 0 && UnityEngine.Random.Range(0, 0.999f) < bonusDropRate)
                    {
                        Instantiate(bonusesPrefabs[UnityEngine.Random.Range(0, bonusesPrefabs.Length)],
                                    gameObject.transform.position,
                                    Quaternion.identity
                                    );
                    }
                    Destroy(this.gameObject);
                }
            }
        }

        protected override void OnDestroy()
        {
            Set.Remove(gameObject);
            BrickDestroy.Raise();
        }
    }
}