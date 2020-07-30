using UnityEngine;

namespace catinapoke.arkanoid
{
    public class PlatformWidthBonus : BaseBonus
    {
        [SerializeField]
        private Vector2 widthCoefLimits;

        protected override void Activate(GameObject touched)
        {
            float scaleCoefficient = UnityEngine.Random.Range(widthCoefLimits.x, widthCoefLimits.y);
            Debug.Log($"Platform scale multiplied by {scaleCoefficient}");
            touched.GetComponent<PlatformMover>().MultiplyScale(scaleCoefficient);
        }
    }
}