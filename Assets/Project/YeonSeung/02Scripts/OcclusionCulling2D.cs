using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OcclusionCulling2D : MonoBehaviour
{
    [System.Serializable] public class ObjectSettings
    {
        [HideInInspector] public string title;
        public GameObject theGameObject;

        [SerializeField] public Vector2 size = Vector2.one;
        [SerializeField] public Vector2 offset = Vector2.zero;
        [SerializeField] public bool multiplySizeByTransformScale = true;


        public Vector2 sized { get; set; }
        public Vector2 center { get; set; }
        public Vector2 TopRight { get; set; }
        public Vector2 TopLeft { get; set; }
        public Vector2 BottomLeft { get; set; }
        public Vector2 BottomRight { get; set; }
        public float right { get; set; }
        public float left { get; set; }
        public float top { get; set; }
        public float bottom { get; set; }

        public Color DrawColor = Color.white;
        public bool showBorders = true;
    }


    public ObjectSettings[] objectSettings = new ObjectSettings[1];

    private new Camera _camera;
    private float _cameraHalfWidth;

    public float updateRateInSeconds = 0.1f;

    private float timer;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _cameraHalfWidth = _camera.orthographicSize * ((float)Screen.width / (float)Screen.height);

        foreach (ObjectSettings obs in objectSettings)
        {
            obs.sized = obs.sized * (obs.multiplySizeByTransformScale ? new Vector2(Mathf.Abs(obs.theGameObject.transform.localScale.x),
               Mathf.Abs(obs.theGameObject.transform.localScale.y)) : Vector2.one);
            obs.center = (Vector2)obs.theGameObject.transform.position + obs.offset;

            obs.TopRight = new Vector2(obs.center.x + obs.sized.x, obs.center.y + obs.sized.y);
            obs.TopLeft = new Vector2(obs.center.x - obs.sized.x, obs.center.y + obs.sized.y);

            obs.BottomRight = new Vector2(obs.center.x + obs.sized.x, obs.center.y - obs.sized.y);
            obs.BottomLeft = new Vector2(obs.center.x - obs.sized.x, obs.center.y - obs.sized.y);

            obs.right = obs.center.x + obs.sized.x;
            obs.left = obs.center.x - obs.sized.x;
            obs.top = obs.center.y + obs.sized.y;
            obs.bottom = obs.center.x - obs.sized.x;

        }

    }

    private void OnDrawGizmosSelected()
    {
        foreach(ObjectSettings obs in objectSettings)
        {
            if (obs.theGameObject)
            {
                obs.title = obs.theGameObject.name;

                if (obs.showBorders)
                {
                    obs.TopRight = new Vector2(obs.center.x + obs.sized.x, obs.center.y + obs.sized.y);
                    obs.TopLeft = new Vector2(obs.center.x - obs.sized.x, obs.center.y + obs.sized.y);
                    obs.BottomRight = new Vector2(obs.center.x + obs.sized.x, obs.center.y - obs.sized.y);
                    obs.BottomLeft = new Vector2(obs.center.x - obs.sized.x, obs.center.y - obs.sized.y);

                    Gizmos.color = obs.DrawColor;

                    Gizmos.DrawLine(obs.TopRight, obs.TopLeft);
                    Gizmos.DrawLine(obs.BottomRight, obs.BottomLeft);
                    Gizmos.DrawLine(obs.TopRight, obs.BottomRight);
                    Gizmos.DrawLine(obs.TopLeft, obs.BottomLeft);

                }
            }
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > updateRateInSeconds) timer = 0;
        else return;

        float cameraRight = _camera.transform.position.x + _cameraHalfWidth;
        float cameraLeft = _camera.transform.position.x - _cameraHalfWidth;
        float cameraTop = _camera.transform.position.y + _camera.orthographicSize;
        float cameraBottom = _camera.transform.position.y - _camera.orthographicSize;

        foreach(ObjectSettings obs in objectSettings)
        {
            if (obs.theGameObject)
            {
                bool IsObjectVisibleInCastingCamera = obs.right > cameraLeft & obs.left < cameraRight & // 좌우 체크
                    obs.top > cameraBottom & obs.bottom < cameraTop;                                    // 위아래

                obs.theGameObject.SetActive(IsObjectVisibleInCastingCamera);
            }
        }

    }


}
