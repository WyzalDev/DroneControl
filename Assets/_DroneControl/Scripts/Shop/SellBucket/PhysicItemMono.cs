using UnityEngine;

namespace _DroneControl.Scripts.Shop.SellBucket
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicItemMono : MonoBehaviour
    {
        [SerializeField] public PhysicItem itemInfo;

        private Rigidbody rb;
        private Transform holdEndPoint;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Grab(Transform holdEndPoint)
        {
            this.holdEndPoint = holdEndPoint;
            rb.useGravity = false;
        }

        public void Drop()
        {
            this.holdEndPoint = null;
            rb.useGravity = true;
        }

        private void FixedUpdate()
        {
            if (holdEndPoint != null)
            {
                var newPos = Vector3.Lerp(transform.position, holdEndPoint.position,
                    Time.fixedDeltaTime * 10f);
                rb.MovePosition(newPos);
            }
        }
    }
}