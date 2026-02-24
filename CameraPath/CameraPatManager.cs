using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPatManager : MonoBehaviour
{
    private float _startTime = 0;
    public GameObject player;
    public GameObject cam;
    public GameObject Guider_UI;
    public bool FirstInsit = true;
    public GameObject[] UIs;
    public GameObject UIRoot;
    public Transform FirstPos;
    public GameObject OpenBtn;
    public GameObject CloseBtn;
    private CameraPathAnimator curanim;
    private CameraPath camerapath;
    public GameObject ManYouQuit;
    public GameObject ManYouEnd;
    private Vector3[] pathpoints;
    private bool ManYouState = true;

    void Start()
    {
        _startTime = Time.time;
        curanim = transform.GetComponent<CameraPathAnimator>();
        camerapath = transform.GetComponent<CameraPath>();
        pathpoints = camerapath.storedPoints;
        
        // if (SceneChange.beOpen == 0)
        // {
        //     ManYouState = true;
        //     CloseBtn.SetActive(true);
        //     OpenBtn.SetActive(false);
        // }
        // else
        // {
        //     ManYouState = false;
        //     CloseBtn.SetActive(false);
        //     OpenBtn.SetActive(true);
        // }
        //ManYouQuit = GameObject.Find("ManYouMid");

        cam.transform.position= new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
      
    }

    void Update()
    {
        if (ManYouState)
        {
            //Debug.Log("lujing");
            if (FirstInsit)
            {
                _startTime = Time.time;
                FirstInsit = false;
                Debug.Log("State is true");
            }

            if (Time.time - _startTime > 2)
            {
                if (!curanim.isPlaying && !CheckUIState())
                {

                    player.SetActive(false);
                    //Debug.Log (StartPostion (player.transform.position));
                    curanim.PercentageY = StartPostion(player.transform.position);
                    //Debug.Log("Per"+curanim.PercentageY);

                    //Debug.Log ("zgdfdf"+curanim.startPercent);
                    AnimationPlay();


                    cam.SetActive(true);
                    //Debug.Log("player");
                    if (Guider_UI != null)
                        Guider_UI.SetActive(true);
                    //Thread.Sleep(10000);
                    //for (int i = 0; i < 6; i++)
                    //{
                    //    if (GameObject.FindGameObjectWithTag("voice").GetComponent<AudioClipC>()._musicArray[i].isPlaying)
                    //    {
                    //        GameObject.FindGameObjectWithTag("voice").GetComponent<AudioClipC>()._musicArray[i].Stop();
                    //        Debug.Log(i);
                    //    }
                    //    Debug.Log(i);
                    //}
                }
            }
            if (curanim.PercentageY == 1)
            {

                ManYouEndWin();
                //CloseManYou();
            }

        }
    }

    /// <summary>寻找最近的位置</summary>
    /// <param name="_currentpos"></param>
    /// <returns></returns>
    float StartPostion(Vector3 _currentpos)
    {
        int _start = 0;
        float mindistance;
        float percent;
        mindistance = Vector3.Distance(_currentpos, pathpoints[0]);
        for (int i = 1; i < pathpoints.Length; i++)
        {
            if (Vector3.Distance(_currentpos, pathpoints[i]) < mindistance)
            {
                mindistance = Vector3.Distance(_currentpos, pathpoints[i]);
                _start = i;
            }
        }
        percent = (_start + 1) / (float)pathpoints.Length;
        //Debug.Log (percent);
        return percent;
    }

    void AnimationPause()
    {
        curanim.PlayingY = false;
    }

    void AnimationPlay()
    {
        curanim.PlayingY = true;
    }

    bool CheckUIState()
    {
        bool result = false;
        if (UIs.Length > 0)
        {
            result = UIs[0].activeSelf || UIRoot.transform.localScale.x > 0;
            for (int i = 1; i < UIs.Length; i++)
            {
                result = result || UIs[i].activeSelf;
            }
        }
        return result;
    }

    public void OpenManYou()
    {
        ManYouState = true;
        Debug.Log("开启漫游");
    }

    public void CloseManYou()
    {

        ManYouState = false;
        player.transform.position = new Vector3(cam.transform.position.x, player.transform.position.y, cam.transform.position.z);
        player.transform.rotation = cam.transform.rotation;
        player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
        cam.SetActive(false);
        player.SetActive(true);
        AnimationPause();
        if (Guider_UI != null)
            Guider_UI.SetActive(false);
        Debug.Log("关闭漫游");
    }

    public void StartManYou()
    {
        if (!curanim.isPlaying && !CheckUIState())
        {
            player.SetActive(false);
            //Debug.Log (StartPostion (player.transform.position));
            curanim.PercentageY = StartPostion(player.transform.position);
            //Debug.Log ("zgdfdf"+curanim.startPercent);
            AnimationPlay();


            cam.SetActive(true);
            if (Guider_UI != null)
                Guider_UI.SetActive(true);
        }
    }

    public void PauseManYou()
    {
        Debug.Log("PauseManYou");
        player.transform.position = new Vector3(cam.transform.position.x, player.transform.position.y, cam.transform.position.z);
        player.transform.rotation = cam.transform.rotation;
        player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
        cam.SetActive(false);
        player.SetActive(true);
        AnimationPause();
        if (Guider_UI != null)
            Guider_UI.SetActive(false);
    }

    public void ConManYou()
    {

        Debug.Log("ConManYou");
        //ManYouQuit.SetActive(false);
        //GameObject.Find("daoyou").SendMessage("ConAudio");
        if (!curanim.isPlaying && !CheckUIState())
        {
            player.SetActive(false);
            //Debug.Log (StartPostion (player.transform.position));
            curanim.PercentageY = StartPostion(player.transform.position);
            //Debug.Log ("zgdfdf"+curanim.startPercent);
            AnimationPlay();
            cam.SetActive(true);
            if (Guider_UI != null)
                Guider_UI.SetActive(true);
        }
    }

    public void ManYouEndWin()
    {
        CloseManYou();
        player.transform.position = new Vector3(cam.transform.position.x, player.transform.position.y, cam.transform.position.z);
        player.transform.rotation = cam.transform.rotation;
        player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
        cam.SetActive(false);
        player.SetActive(true);
        Debug.Log("cam" + cam.name);
        //AnimationPause();
        if (Guider_UI != null)
            Guider_UI.SetActive(false);
        ManYouEnd.SetActive(true);

    }

    public void BeginPos()
    {

        CloseManYou();
        cam.SetActive(false);
        player.SetActive(true);
        player.transform.position = FirstPos.position;
        player.transform.localRotation = FirstPos.localRotation;
        Debug.Log("Done" + FirstPos.position);
        Debug.Log("D" + player.transform.position);
        ManYouEnd.SetActive(false);
        //AnimationPause();
        if (Guider_UI != null)
            Guider_UI.SetActive(false);
    }

}
