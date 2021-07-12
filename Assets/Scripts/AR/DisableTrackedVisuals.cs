using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;

namespace AR
{
    public class DisableTrackedVisuals : MonoBehaviour
    {
        
        [SerializeField]
        [Tooltip("Disables spawned feature points and the ARPointCloudManager")]
        bool _disableFeaturePoints;

        public bool disableFeaturePoints
        {
            get => _disableFeaturePoints;
            set => _disableFeaturePoints = value;
        }

        [SerializeField]
        [Tooltip("Disables spawned planes and ARPlaneManager")]
        bool _disablePlaneRendering;

        public bool disablePlaneRendering
        {
            get => _disablePlaneRendering;
            set => _disablePlaneRendering = value;
        }

        [SerializeField]
        private ARPointCloudManager _pointCloudManager;

        public ARPointCloudManager PointCloudManager
        {
            get => _pointCloudManager;
            set => _pointCloudManager = value;
        }
    
        [SerializeField]
        ARPlaneManager _planeManager;

        public ARPlaneManager PlaneManager
        {
            get => _planeManager;
            set => _planeManager = value;
        }

        private void OnEnable()
        {
            ARContentPlacement.OnPlacedObject += OnPlacedObject;
        }

        private void OnDisable()
        {
            ARContentPlacement.OnPlacedObject -= OnPlacedObject;
        }

        private void OnPlacedObject()
        {
            if (_disableFeaturePoints)
            {
                _pointCloudManager.SetTrackablesActive(false);
                _pointCloudManager.enabled = false;
            }

            if (_disablePlaneRendering)
            {
                _planeManager.SetTrackablesActive(false);
                _planeManager.enabled = false;
            }
        }
    }
}