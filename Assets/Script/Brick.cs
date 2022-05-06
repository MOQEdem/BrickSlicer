using BzKovSoft.ObjectSlicer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Brick : MonoBehaviour
{
    [SerializeField] private KnifeTip _knifeTip;
    [SerializeField] private Vector3 _cutForce;
    [SerializeField] private float _rollSpeedIncreaser;
    [SerializeField] private float _rollStartValueCorrector;
    [SerializeField] private float _sliceDestroyDelay;
    [SerializeField] private float _moveSpeed;

    private GameObject _slice;
    private Material[] _materials;
    private bool _isCutting;
    private bool _isMoving;
    private float _currentRollValue;
    private float _currentTipPositionY;

    private void Awake()
    {
        _isCutting = false;
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving)
        {
            Move();
        }

        if (_isCutting)
        {
            RollSlice();

            if (_knifeTip.transform.position.y <= 0)
            {
                ReleaseSlice();
            }
        }
    }

    public void SetMovingStatus(bool isMoving)
    {
        _isMoving = isMoving;
    }

    public void Cut()
    {
        var sliceable = GetComponent<IBzSliceable>();

        Plane plane = new Plane(Vector3.right, 0f);

        sliceable.Slice(plane, result =>
        {
            if (result.sliced)
            {
                _isCutting = true;
                _slice = result.outObjectPos;
                _slice.GetComponent<Brick>().SetMovingStatus(false);

                var meshFilter = _slice.GetComponent<MeshFilter>();
                float centerX = meshFilter.sharedMesh.bounds.center.x;

                _materials = _slice.GetComponent<MeshRenderer>().materials;

                foreach (var material in _materials)
                {
                    material.SetFloat("_PointX", centerX);
                }

                _currentRollValue = _knifeTip.transform.position.y - _rollStartValueCorrector;
                _currentTipPositionY = _knifeTip.transform.position.y;
            }
        });
    }

    private void Move()
    {
        transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
    }

    private void RollSlice()
    {
        float deltaPointY = (_currentTipPositionY - _knifeTip.transform.position.y) * _rollSpeedIncreaser;

        foreach (var material in _materials)
        {
            material.SetFloat("_PointY", _currentRollValue - deltaPointY);
        }
    }

    private void ReleaseSlice()
    {
        _slice.GetComponent<Rigidbody>().isKinematic = false;
        _slice.GetComponent<Rigidbody>().AddForce(_cutForce, ForceMode.VelocityChange);
        _slice.GetComponent<Brick>().SetMovingStatus(true);
        _isCutting = false;
        Destroy(_slice, _sliceDestroyDelay);
    }
}
