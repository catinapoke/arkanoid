using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SocialPlatforms.GameCenter;

namespace catinapoke.arkanoid
{
    class BonusToughBrick : ToughBrick
    {
        // TODO: BonusList
        // Bonuses:
        //   ● Increase/decrease ball's speed 
        //   ● Spawn additional ball
        //   ● Increase width of platform
        
        protected override void Start()
        {
            base.Start();
            // TODO: Choose bonus: none or 1-3
        }

        public override void Hit()
        {
            lives--;
            if (lives == 0)
            {
                Destroy(this.gameObject, 0);
                // TODO: Spawn bonus
                throw new NotImplementedException();
            }
                
        }
    }
}
