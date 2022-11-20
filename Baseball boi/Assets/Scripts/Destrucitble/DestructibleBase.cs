using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YOLO
{
    public partial class DestructibleBase : MonoBehaviour
    {
        public bool destroyOnCollision = true;

        private void OnCollisionEnter(Collision other)
        {
            if(!destroyOnCollision)
            {
                return;
            }   

            if(!other.transform.name.ToLower().Contains("ball"))
            {
                return;
            }

            DestroyOnCollision(other);         
        }

        public virtual void DestroyOnCollision(Collision collision)
        {

        }
    }
}
