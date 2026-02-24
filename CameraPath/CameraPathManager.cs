using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPathManager : MonoBehaviour
{
    private GameObject FPSPlayer;

    private CameraPath cameraPath;
    private CameraPathAnimator cameraPathAnimator;
    [Header("所有路径点")] private Vector3[] pathpoints;

    private void Start()
    {
        FPSPlayer = GameManager.Instance.PlayerControllerModule.FPSPlayer.gameObject;
        cameraPathAnimator = GetComponent<CameraPathAnimator>();
        cameraPathAnimator.animationObject = FPSPlayer.transform;
        cameraPath = GetComponent<CameraPath>();
        pathpoints = cameraPath.storedPoints; //所有的点
    }

    public void AnimationPlay()
    {
        if (cameraPathAnimator)
        {
            cameraPathAnimator.PercentageY = CalculateCurrentPosToAnimator(FPSPlayer.transform.position) - 0.01f;
            cameraPathAnimator.Play();
        }
    }

    public void AnimationStop()
    {
        if (cameraPathAnimator)
            cameraPathAnimator.Pause();
    }


    /// <summary>计算最近点 </summary>
    /// <returns>返回一个CameraPathAnimator的对应值（0·1）</returns>
    public float CalculateCurrentPosToAnimator(Vector3 currentPos)
    {
        int _start = 0;
        float mindistance;
        float percent;
        mindistance = Vector3.Distance(currentPos, pathpoints[0]);
        for (int i = 1; i < pathpoints.Length; i++)
        {
            if (Vector3.Distance(currentPos, pathpoints[i]) < mindistance)
            {
                mindistance = Vector3.Distance(currentPos, pathpoints[i]);
                _start = i;
            }
        }

        percent = (_start + 1) / (float)pathpoints.Length;
        return percent;
    }
}