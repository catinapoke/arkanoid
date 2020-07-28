using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace catinapoke.arkanoid
{
    class ToughBrick : Brick
    {
        [SerializeField]
        protected int maxLives;
        protected int lives;

        protected override void Start()
        {
            base.Start();
            lives = UnityEngine.Random.Range(1, maxLives + 1);
        }

        public override void Hit()
        {
            lives--;
            if(lives == 0)
                Destroy(this.gameObject, 0);
        }
    }
}
