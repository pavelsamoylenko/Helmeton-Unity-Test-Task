using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AR
{
    public class ARContentPlacement : MonoBehaviour
    {
        [SerializeField] private ARSessionOrigin _arSessionOrigin;
        [SerializeField] private ARRaycastManager _raycastManager;
        [SerializeField] private ARContent _arContent;
        [SerializeField] private GameObject _placementIndicator;
        [SerializeField] private TrackableType _trackableType = TrackableType.PlaneWithinPolygon;
        
        /// <summary>
        /// Invoked whenever an object is placed in on a plane.
        /// </summary>
        public static event Action OnPlacedObject;

        private static readonly List<ARRaycastHit> _hits = new List<ARRaycastHit>();
        private GameObject _spawnedGameObject;
        private bool _contentPlaced = false;
        private Pose _placementPose;

        private Animator _contentAnimator;
        private static readonly int Animate = Animator.StringToHash("Animate");
        private bool _animated;

        private void Update()
        {
            TryPlaceContent();
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            if (!_contentPlaced || !_animated) return;
            if (IsTouched())
            {
                _contentAnimator.SetTrigger(Animate);
            }
        }


        private void TryPlaceContent()
        {
            if (_contentPlaced) return;
            var isPoseValid = TryGetPlacementPose(out _placementPose);
            UpdatePlacementIndicator(isPoseValid);

            if (isPoseValid && IsTouched())
            {
                PlaceObject();
            }
        }

        private bool IsTouched()
        {
            return Input.touchCount > 0 &&
                   Input.touches[0].phase == TouchPhase.Began;
        }

        public void SetARContent(ARContent arContent)
        {
            _arContent = arContent;
            _contentAnimator = arContent.GetComponentInChildren<Animator>();
            _animated = _contentAnimator != null;
        }


        private void PlaceObject()
        {
            _arContent.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
            _contentPlaced = true;
            _placementIndicator.SetActive(false);
            _arContent.SetActive(true);
            OnPlacedObject?.Invoke();
        }

        private void UpdatePlacementIndicator(bool placementPoseValid)
        {
            if (placementPoseValid)
            {
                _placementIndicator.SetActive(true);
                _placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
            }
            else
            {
                _placementIndicator.SetActive(false);
            }
        }


        private bool TryGetPlacementPose(out Pose pose)
        {
            if (Raycast(_hits))
            {
                pose = _hits[0].pose;

                var cameraForward = _arSessionOrigin.camera.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z);

                pose.rotation = Quaternion.LookRotation(cameraBearing);
                return true;
            }

            pose = Pose.identity;
            return false;
        }

        private bool Raycast(List<ARRaycastHit> hits)
        {
            var screenCenter = _arSessionOrigin.camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            return _raycastManager.Raycast(screenCenter, hits, _trackableType);
        }
    }
}