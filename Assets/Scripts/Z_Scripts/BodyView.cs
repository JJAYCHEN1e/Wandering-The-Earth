
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyView : InteractBase
{
    [Header("模型根节点")]
    public GameObject mModelRoot = null;
    [Header("结构模型根节点")]
    public GameObject mStructureModelRoot = null;
    [Header("人体系统物体数组")]
    public GameObject[] mHumanbodyObjAry = null;

    [Header("人体经脉物体数组")]
    public GameObject[] mAcupointObjAry = null;

    [Header("界面根节点")]
    public GameObject mUIRoot = null;
    [Header("人体系统网格")]
    public GameObject mSystemGrid = null;
    [Header("人体按钮")]
    public UISwitchButton mHumanbodBtn = null;
    [Header("人体经脉网格")]
    public GameObject mAcupointGrid = null;
    [Header("人体经脉按钮")]
    public UISwitchButton mAcupointBtn = null;
    [Header("姿态按钮")]
    public UISwitchButton mPoseBtn = null;

    public Text mPoseText = null;
    public Image mPoseImage = null;
    public Sprite mPoseUpSprite = null;
    public Sprite mPoseDownSprite = null;
    public Transform mPoseUpRoot = null;
    public Transform mPoseDownRoot = null;
    [Header("隐藏按钮")]
    public UISwitchButton mHideBtn = null;
    [Header("分离按钮")]
    public UISwitchButton mSplitBtn = null;
    [Header("标注按钮")]
    public UISwitchButton mLabelBtn = null;
    public Image mLabelImage = null;
    public Sprite mLabelOffSprite = null;
    public Sprite mLabelOnSprite = null;
    public Text mLabelText = null;
    [Header("人体系统按钮数组")]
    public UISwitchButton[] mHumanbodBtnAry = null;
    [Header("人体经脉按钮数组")]
    public UISwitchButton[] mAcupointBtnAry = null;

    private BodyControl mObserveControl = null;

    private XmlDocument mDocument = new XmlDocument();

    private Dictionary<string, string> mNameDict = new Dictionary<string, string>();

    private Vector3 mDragObjStartPot3 = Vector3.zero;

    private Vector3 mDragStartPot3 = Vector3.zero;

    private Vector3 mDragDiffuseVet3 = Vector3.zero;

    private bool mIsDragActive = false;

    private GameObject mDragObj = null;

    private Vector3 offsetPos = Vector3.zero;

    private Vector3 offsetScale = Vector3.zero;

    private Transform offsetParent = null;

    protected override void Start()
    {
        base.Start();

        InitXML();

        mDragObj = mStructureModelRoot;
        mObserveControl = mStructureModelRoot.GetComponent<BodyControl>();

        offsetPos = transform.localPosition;
        offsetScale = transform.localScale;
        offsetParent = transform.parent;
    }

    private void InitXML()
    {
        DirectoryInfo folder = new DirectoryInfo(Application.streamingAssetsPath + "/Table");
        FileInfo[] fileList = folder.GetFiles("HumanbodySystem.xml");
        mDocument.Load(fileList[0].FullName);
        XmlNode node = mDocument.SelectSingleNode("root");
        SetXMLNode(node);
    }

    private void SetXMLNode(XmlNode node)
    {
        XmlNodeList list = node.SelectNodes("children/sub");

        if (list.Count <= 0) return;

        foreach (XmlNode n in list)
        {
            string index = n.Attributes["ObjName"].Value;
            string name = n.Attributes["ShowName"].Value;

            mNameDict.Add(index, name);

            SetXMLNode(n);
        }
    }

    protected override void Update()
    {
        base.Update();

        //if (SteamVRControllerBase.Instance.rightHand.menu.GetStateDown(Valve.VR.SteamVR_Input_Sources.RightHand))
        //{
        //    if (mUIRoot.activeInHierarchy)
        //    {
        //        mUIRoot.SetActive(!mUIRoot.activeInHierarchy);
        //        transform.SetParent(SteamVRControllerBase.Instance.mainCamera.transform);
        //        transform.localPosition = Vector3.forward * 2f;
        //        transform.localRotation = Quaternion.identity;
        //        transform.localScale = Vector3.one;
        //    }
        //    else
        //    {
        //        transform.SetParent(SteamVRControllerBase.Instance.transform);
        //        mUIRoot.SetActive(!mUIRoot.activeInHierarchy);
        //    }
        //}

        if (SteamVRControllerBase.Instance.leftHand.mRayHitObj != null && SteamVRControllerBase.Instance.leftHand.mDragInteract!=null)
        {
            mLabelText.transform.position = SteamVRControllerBase.Instance.leftHand.mRayHitPoint + Vector3.back * 0.05f;

            string name = SteamVRControllerBase.Instance.leftHand.mRayHitObj.name;
            if (mNameDict.ContainsKey(name))
            {
                mLabelText.text = mNameDict[name];
            }
            else
            {
                mLabelText.text = "";
            }

        }
        else if (SteamVRControllerBase.Instance.rightHand.mRayHitObj != null && SteamVRControllerBase.Instance.rightHand.mDragInteract != null)
        {
            mLabelText.transform.position = SteamVRControllerBase.Instance.rightHand.mRayHitPoint + Vector3.back * 0.05f;

            string name = SteamVRControllerBase.Instance.rightHand.mRayHitObj.name;
            if (mNameDict.ContainsKey(name))
            {
                mLabelText.text = mNameDict[name];
            }
            else
            {
                mLabelText.text = "";
            }
        }
        else
        {
            mLabelText.text = "";
        }

        if (SteamVRControllerBase.Instance.leftHand.grip.GetStateDown(Valve.VR.SteamVR_Input_Sources.LeftHand))
        {
            mIsDragActive = true;

            mDragStartPot3 = SteamVRControllerBase.Instance.leftHand.transform.position;

            mDragObjStartPot3 = mDragObj.transform.position;
        }

        if (mIsDragActive)
        {
            mDragDiffuseVet3 = SteamVRControllerBase.Instance.leftHand.transform.position - mDragStartPot3;

            mDragObj.transform.position = mDragObjStartPot3 + mDragDiffuseVet3;
        }

        if (SteamVRControllerBase.Instance.leftHand.grip.GetStateUp(Valve.VR.SteamVR_Input_Sources.LeftHand))
        {
            mIsDragActive = false;
        }
    }

    public void OnHumanbodyBtnClick()
    {
        if (mHumanbodBtn.GetIsSwitchOn())
        {
            mAcupointBtn.SetClick(SwitchType.Off);
            mSplitBtn.SetClick(SwitchType.Off);
            SetAcupointBtnArySwitch(SwitchType.Off);
            SetHumanbodBtnArySwitch(SwitchType.Off);
            ToolCenter.SetActive(mSystemGrid, true);
            ToolCenter.SetActive(mHumanbodyObjAry, true);
        }
        else
        {
            ToolCenter.SetActive(mSystemGrid, false);
        }
    }

    public void OnAcupointBtnClick()
    {
        if (mAcupointBtn.GetIsSwitchOn())
        {
            mHumanbodBtn.SetClick(SwitchType.Off);
            mSplitBtn.SetClick(SwitchType.Off);
            SetAcupointBtnArySwitch(SwitchType.Off);
            SetHumanbodBtnArySwitch(SwitchType.Off);
            ToolCenter.SetActive(mAcupointGrid, true);
            ToolCenter.SetActive(mAcupointObjAry, true);
        }
        else
        {
            ToolCenter.SetActive(mAcupointGrid, false);
        }
    }

    public void OnPoseBtnClick()
    {
        if (mPoseBtn.GetIsSwitchOn())
        {
            mPoseText.text = "躺下";
            mPoseImage.sprite = mPoseDownSprite;
            mObserveControl.SetRotation(mPoseDownRoot.localRotation);
            mSplitBtn.SetClick(SwitchType.Off);
        }
        else
        {
            mPoseText.text = "直立";
            mPoseImage.sprite = mPoseUpSprite;
            mObserveControl.SetRotation(mPoseUpRoot.localRotation);
            mSplitBtn.SetClick(SwitchType.Off);
        }
    }

    public void OnResetBtnClick()
    {
        mPoseBtn.SetClick(SwitchType.Off);
        mSplitBtn.SetClick(SwitchType.Off);

        SetReset();
    }

    public void OnHideBtnClick()
    {
        if (mHideBtn.GetIsSwitchOn())
        {
            SteamVRControllerBase.Instance.rightHand.mIsHide = true;
        }
        else
        {
            SteamVRControllerBase.Instance.rightHand.mIsHide = false;
        }
    }

    public void OnSplitBtnClick()
    {
        SetSplit();
    }

    private void SetSplit()
    {

        List<GameObject> humanbodyAry = new List<GameObject>();

        for (int i = 0; i < mHumanbodyObjAry.Length; i++)
        {
            if (mHumanbodyObjAry[i].activeSelf)
            {
                humanbodyAry.Add(mHumanbodyObjAry[i]);
            }
        }

        if (mSplitBtn.GetIsSwitchOn())
        {
            int countInt = humanbodyAry.Count;
            float spaceValueFlt = 0f;
            float startValueFlt = 0f;
            List<Vector3> pointAry = new List<Vector3>();

            if (mPoseBtn.GetIsSwitchOn())
            {
                spaceValueFlt = 0.2f;
            }
            else
            {
                spaceValueFlt = 0.45f;
            }

            int integerInt = countInt / 2;
            int remainderInt = countInt % 2;

            if (remainderInt == 0)
            {
                startValueFlt = -(integerInt - 0.5f) * spaceValueFlt;
            }
            else
            {
                startValueFlt = -integerInt * spaceValueFlt;
            }

            if (mPoseBtn.GetIsSwitchOn())
            {
                for (int i = 0; i < countInt; i++)
                {
                    float valueFlt = startValueFlt + i * spaceValueFlt;
                    Vector3 valueVct3 = Vector3.forward * valueFlt;
                    pointAry.Add(valueVct3);
                }
            }
            else
            {
                for (int i = 0; i < countInt; i++)
                {
                    float valueFlt = startValueFlt + i * spaceValueFlt;
                    Vector3 valueVct3 = Vector3.right * valueFlt;
                    pointAry.Add(valueVct3);
                }
            }

            StartCoroutine(DoSplit(humanbodyAry, pointAry));
        }
        else
        {
            for (int i = 0; i < humanbodyAry.Count; i++)
            {
                humanbodyAry[i].transform.localPosition = Vector3.zero;
            }
        }
    }

    public IEnumerator DoSplit(List<GameObject> objAry, List<Vector3> pointAry)
    {

        for (int i = 0; i < objAry.Count; i++)
        {
            StartCoroutine(DoSplitOne(objAry[i], pointAry[i]));
        }

        yield return null;
    }

    public IEnumerator DoSplitOne(GameObject obj, Vector3 point)
    {
        for (int i = 0; i < 10; i++)
        {
            obj.transform.localPosition = Vector3.Lerp(Vector3.zero, point, i / 10f);

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    public void OnLabelBtnClick()
    {
        if (mLabelBtn.GetIsSwitchOn())
        {
            mLabelImage.sprite = mLabelOnSprite;
            ToolCenter.SetActive(mLabelText.gameObject, true);
        }
        else
        {
            mLabelImage.sprite = mLabelOffSprite;
            ToolCenter.SetActive(mLabelText.gameObject, false);
        }
    }

    public void OnHumanbodySystemBtnClick(int index)
    {
        SetHumanbodySystem(index);
    }

    private void SetHumanbodySystem(int index)
    {
        int oneOnIndex;

        if (IsHumanbodBtnAryOneOn(out oneOnIndex))
        {
            ToolCenter.SetActive(mHumanbodyObjAry, oneOnIndex, true);
        }
        else if (IsHumanbodyBtnAryAllOff())
        {
            ToolCenter.SetActive(mHumanbodyObjAry, true);
        }
        else
        {
            ToolCenter.SetActive(mHumanbodyObjAry[index], mHumanbodBtnAry[index].GetIsSwitchOn());
        }
    }

    public void OnAcupointBtnClick(int index)
    {
        SetAcupoint(index);
    }

    public void OnQuitBtnClick()
    {
        Application.Quit();
    }

    private void SetAcupoint(int index)
    {
        int oneOnIndex;

        if (IsAcupointBtnAryOneOn(out oneOnIndex))
        {
            ToolCenter.SetActive(mAcupointObjAry, oneOnIndex, true);
        }
        else if (IsAcupointBtnAryAllOff())
        {
            ToolCenter.SetActive(mAcupointObjAry, true);
        }
        else
        {
            ToolCenter.SetActive(mAcupointObjAry[index], mAcupointBtnAry[index].GetIsSwitchOn());
        }
    }


    private void SetReset()
    {
        if (mObserveControl != null)
        {
            mObserveControl.ResetData();
        }
    }

    private void SetHumanbodBtnArySwitch(SwitchType type)
    {
        for (int i = 0; i < mHumanbodBtnAry.Length; i++)
        {
            mHumanbodBtnAry[i].SetClick(type);
        }
    }

    private void SetAcupointBtnArySwitch(SwitchType type)
    {
        for (int i = 0; i < mAcupointBtnAry.Length; i++)
        {
            mAcupointBtnAry[i].SetClick(type);
        }
    }

    private bool IsHumanbodBtnAryOneOn(out int index)
    {
        bool isOneOn = false;
        int onCount = 0;
        index = 0;

        for (int i = 0; i < mHumanbodBtnAry.Length; i++)
        {
            if (mHumanbodBtnAry[i].GetIsSwitchOn())
            {
                index = i;
                onCount++;
            }
        }

        if (onCount == 1) isOneOn = true;

        return isOneOn;
    }

    private bool IsAcupointBtnAryOneOn(out int index)
    {
        bool isOneOn = false;
        int onCount = 0;
        index = 0;

        for (int i = 0; i < mAcupointBtnAry.Length; i++)
        {
            if (mAcupointBtnAry[i].GetIsSwitchOn())
            {
                index = i;
                onCount++;
            }
        }

        if (onCount == 1) isOneOn = true;

        return isOneOn;
    }

    private bool IsHumanbodyBtnAryAllOff()
    {
        bool isAllOff = true;

        for (int i = 0; i < mHumanbodBtnAry.Length; i++)
        {
            if (mHumanbodBtnAry[i].GetIsSwitchOn())
            {
                isAllOff = false;
            }
        }

        return isAllOff;
    }

    private bool IsAcupointBtnAryAllOff()
    {
        bool isAllOff = true;

        for (int i = 0; i < mAcupointBtnAry.Length; i++)
        {
            if (mAcupointBtnAry[i].GetIsSwitchOn())
            {
                isAllOff = false;
            }
        }

        return isAllOff;
    }
}
