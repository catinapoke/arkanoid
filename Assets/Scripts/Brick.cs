using catinapoke.arkanoid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catinapoke.arkanoid
{
    class Brick : BouncableObject
    {
        public override void Hit()
        {
            Destroy(this.gameObject, 0);
        }
    }
}
