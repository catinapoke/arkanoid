using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace catinapoke.arkanoid
{
    [RequireComponent(typeof(Collider2D))]
    class BouncableObject : MonoBehaviour, IBouncable
    {
        [SerializeField]
        protected BouncableList list;

        protected virtual void Start()
        {
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            list.Add(gameObject);
        }

        public virtual void Hit()
        {
            // Walls just do nothing
        }

        protected virtual void OnDestroy()
        {
            list.Remove(gameObject);
        }

    }
}
