using System;
using System.Diagnostics;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer.Samples
{
    /// <summary>
    /// Sample of BzSliceableObjectBase implementation 
    /// </summary>
    public class ObjectSlicerSample : BzSliceableObjectBase, IBzSliceableNoRepeat
    {
        [HideInInspector]
        [SerializeField]
        int _sliceId;
        [HideInInspector]
        [SerializeField]
        float _lastSliceTime = float.MinValue;
        /// <summary>
        /// If your code do not use SliceId, it can relay on delay between last slice and new.
        /// If real delay is less than this value, slice will be ignored
        /// </summary>
        public float delayBetweenSlices = 1f;

        public void Slice(Plane plane, int sliceId, Action<BzSliceTryResult> callBack)
        {
            float currentSliceTime = Time.time;

            // we should prevent slicing same object:
            // - if _delayBetweenSlices was not exceeded
            // - with the same sliceId
            if ((sliceId == 0 & _lastSliceTime + delayBetweenSlices > currentSliceTime) |
                (sliceId != 0 & _sliceId == sliceId))
            {
                return;
            }

            // exit if it have LazyActionRunner
            if (GetComponent<LazyActionRunner>() != null)
                return;

            _lastSliceTime = currentSliceTime;
            _sliceId = sliceId;

            Slice(plane, callBack);
        }
    }
}
