using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EarthConroller : MonoBehaviour
{
    public GameObject jupiter;
    public Rigidbody earthRigidbody;
    public ConstantForce earthConstantForce;
    public Slider forceSlider;
    public Slider timeSlider;
    public float forceScale = 10.0f;
    public float velocityScale = 2.0f;
    //public Toggle forceToggle;
    public GameObject forceDirection;
    public GameObject Smoke;
    public Transform MainCamera;
    public Transform TopCamera;
    public LineRenderer lineRenderer;

    /// <summary>
    /// 力的作用时间，分钟！
    /// </summary>
    private float _forceTime = 1;
    /// <summary>
    /// 轨迹点
    /// </summary>
    /// <typeparam name="Vector3"></typeparam>
    /// <returns></returns>
    private List<Vector3> _positons = new List<Vector3>();
    /// <summary>
    /// 是否开始
    /// </summary>
    private bool _started = false;

    private float _timer = 0;

    private float _lastDistance = 0;
    private float _positiveTime = 0;
    private UIControl _uiControl = null;

    private float _totalTime = 20;

    private Vector3 _mainCamPosInit;


    // Start is called before the first frame update
    void Start()
    {
        forceDirection.SetActive(false);
        Smoke.SetActive(false);
        _uiControl = GameObject.FindObjectOfType<UIControl>();
        _mainCamPosInit = MainCamera.position;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetUp()
    {
        forceDirection.SetActive(true);
        Vector3 pos = this.transform.position;
        pos.y = 10;
        TopCamera.DOMove(pos, 2).SetEase(Ease.InOutSine);
        _uiControl.SetWindowActive(true);
    }

    public void CancelSetUp()
    {
        Vector3 initialVelocityDirection = (jupiter.transform.position - transform.position).normalized;
        earthRigidbody.velocity = initialVelocityDirection * velocityScale;

        if (_totalTime > 5)
        {
            StartCoroutine(ShowTip(60f));
        }
    }

    private IEnumerator ShowTip(float delay)
    {
        yield return new WaitForSeconds(delay);
        string text = "地球与木星将在" + (_totalTime - 5).ToString() + "分钟后相撞" + "\r\n是否改变地球运行轨迹？";
        _uiControl.ShowTip(text);
        earthRigidbody.velocity = Vector3.zero;
    }

    public void Simulate()
    {
        TopCamera.DORotate(Vector3.zero, 2).SetEase(Ease.InOutSine);
        TopCamera.DOMove(new Vector3(-0.5f, 1.5f, -85f), 2).SetEase(Ease.InOutSine);
        StartCoroutine(DoSimulate(2));

        forceDirection.SetActive(false);
    }

    private IEnumerator DoSimulate(float delay)
    {
        yield return new WaitForSeconds(delay);
        _forceTime += 19f * timeSlider.value;
        earthConstantForce.force = -forceDirection.transform.forward * forceSlider.value * forceScale;
        StartCoroutine(CancelForce(_forceTime * 60));
        _positons.Clear();
        _started = true;
        _timer = 0;
        _lastDistance = (jupiter.transform.position - transform.position).magnitude;

        Vector3 initialVelocityDirection = (jupiter.transform.position - transform.position).normalized;
        earthRigidbody.velocity = initialVelocityDirection * velocityScale;
        Debug.Log(earthRigidbody.velocity);
        Smoke.SetActive(true);
        Vector3 angle = Smoke.transform.eulerAngles;
        angle.y = forceDirection.transform.eulerAngles.y;
        Smoke.transform.eulerAngles = angle;
    }

    IEnumerator CancelForce(float delay)
    {
        yield return new WaitForSeconds(delay);
        earthConstantForce.force = Vector3.zero;
        Smoke.SetActive(false);
    }

    private void FollowEarth()
    {
        if (_started)
        {
            /// <summary>
            /// camera follow
            /// </summary>
            TopCamera.forward = earthRigidbody.velocity.normalized;
            Vector3 pos = this.transform.position;
            pos += earthRigidbody.velocity.normalized * -5f;
            pos.y += 1.5f;
            TopCamera.position = pos;

            /// <summary>
            /// smoke follow
            /// </summary>
            Smoke.transform.position = this.transform.position;

            if (!IsVisible())
            {
                /// <summary>
                /// MainCamera
                /// </summary>
                float xValue = _mainCamPosInit.x;
                xValue += this.transform.position.x;
                Vector3 mainCamPos = MainCamera.position;
                mainCamPos.x = xValue;
                MainCamera.DOMove(mainCamPos,1).SetEase(Ease.InOutSine);
            }


        }
    }

    private bool IsVisible()
    {
        Camera gameCamera = MainCamera.GetComponent<Camera>();
        Vector3 pos = gameCamera.WorldToViewportPoint(this.transform.position);
        bool isVisible = (gameCamera.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
        return isVisible;
    }

    private void OnInVisible()
    {


    }

    private void LateUpdate()
    {
        FollowEarth();
    }

    private void FixedUpdate()
    {
        if (_started)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.5f)
            {
                _positons.Add(this.transform.position);
                lineRenderer.positionCount = _positons.Count;
                lineRenderer.SetPositions(_positons.ToArray());
                _timer = 0;
            }

            float distence = (jupiter.transform.position - transform.position).magnitude;

            if (distence - _lastDistance >= 0)
            {
                _positiveTime += Time.deltaTime;
            }
            else
            {
                _positiveTime = 0;
            }



            if (_positiveTime > 5f)
            {

                _started = false;
                _uiControl.ShowResult("成功避开撞击");
                earthConstantForce.force = Vector3.zero;
                earthRigidbody.velocity = Vector3.zero;
                Smoke.SetActive(false);
            }

            _lastDistance = distence;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _started = false;
        _uiControl.ShowResult("地球和木星发生碰撞");
        earthConstantForce.force = Vector3.zero;
        earthRigidbody.velocity = Vector3.zero;
        Smoke.SetActive(false);
    }

}
