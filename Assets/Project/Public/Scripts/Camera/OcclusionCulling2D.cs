using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class OcclusionCulling2D : MonoBehaviour
{
    [SerializeField] public GameObject[] objects = new GameObject[0];
    [System.Serializable] public class ObjectSettings
    {
        [SerializeField, HideInInspector] internal string title;
        [SerializeField, HideInInspector] internal GameObject theGameObject;
        [SerializeField, HideInInspector] internal Renderer theRenderer;
        [SerializeField, HideInInspector] internal Vector2 sized, center, topRight, topLeft, bottomLeft, bottomRight;
        [SerializeField, HideInInspector] internal float right, left, top, bottom;

        [SerializeField] internal Vector2 additiveSize = Vector2.zero;

        [SerializeField] internal bool showBorders = true;

        [SerializeField] internal Color drawColor = Color.white;

        // static 확인으로 런타임서 보더위치 사이즈 계산 업데이트 방지로, 더 나은 퍼포먼스 
        [SerializeField] internal bool isStatic = true;
    }


    [SerializeField] public ObjectSettings[] objectsSettings = new ObjectSettings[1];
    [SerializeField] public float updateRateInSeconds = 0.1f;

    [SerializeField] private Vector2 _additiveSizeAll = Vector2.zero;
    [Space, SerializeField] private bool overrideAllObjectsSettings = true;
    [SerializeField, HideInInspector] private bool overridingShowBordersAll = true;
    [SerializeField, HideInInspector] private Color overridingDrawColorAll = Color.white;
    [SerializeField, HideInInspector] private bool overridingIsStaticAll = true;


    private new Camera _camera;

    private float timer;

    [SerializeField, HideInInspector] private bool isInitialized;
    private bool hasSavedOverridingSettings = true;

    private Bounds GetCombinedBounds(GameObject parent)
    {
        // Getting absolute value from the parent, 부모에서 한번에 가져다 쓸수 있도록...
        Vector3 absScale = new Vector3(Mathf.Abs(parent.transform.localScale.x), Mathf.Abs(parent.transform.localScale.y), 0);
        Bounds combineBounds = new Bounds(parent.transform.position, absScale);

        // Grab all children renderers
        Renderer[] renderers = parent.GetComponentsInChildren<Renderer>(GetComponent<Renderer>());

        // Grow combined bounds with every children renderes,
        // foreach will not be called if there no renderers
        foreach (Renderer rendererChild in renderers)
        {
            if(combineBounds.size == absScale)
            {
                combineBounds = rendererChild.bounds;
            }
            combineBounds.Encapsulate(rendererChild.bounds);
        }
        // at this point combinedBounds should be size of renderer and all its renderers children
        return combineBounds;

    }

    // Inspector 에서 실행중 확인가능
#if UNITY_EDITOR
    [CustomEditor(typeof(OcclusionCulling2D))]
    private class OcclusionCulling2DEditor : Editor
    {
        private OcclusionCulling2D reference;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(reference == null)
            {
                reference = (OcclusionCulling2D)target;
                return;
            }

            if (reference.overrideAllObjectsSettings)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("overridingShowBordersAll"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("overridingDrawColorAll"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("overridingIsStaticAll"));

                if(GUILayout.Button("Replace Objects' Setting with overriding settings"))
                {
                    reference.hasSavedOverridingSettings = false;
                    if(!reference.hasSavedOverridingSettings)
                    {
                        foreach(ObjectSettings obs in reference.objectsSettings)
                        {
                            obs.showBorders = reference.overridingShowBordersAll;
                            obs.drawColor = reference.overridingDrawColorAll;
                            obs.isStatic = reference.overridingIsStaticAll;

                            if(obs.theGameObject == reference.objectsSettings[reference.objectsSettings.Length - 1].theGameObject)
                            {
                                reference.hasSavedOverridingSettings = true;
                            }
                        }
                    }
                }

            }

            serializedObject.ApplyModifiedProperties();

            if (!Application.isPlaying)
            {
                if(reference.objectsSettings.Length != reference.objects.Length)
                {
                    reference.isInitialized = false;
                    reference.objectsSettings = new ObjectSettings[reference.objects.Length];
                    return;
                }
                if(!reference.isInitialized)
                {
                    for(int i = 0; i < reference.objectsSettings.Length; i++)
                    {
                        reference.objectsSettings[i].theGameObject = reference.objects[i];
                        reference.objectsSettings[i].theRenderer = reference.objects[i].GetComponent<Renderer>();

                        if(i == reference.objectsSettings.Length - 1)
                        {
                            reference.isInitialized = true;
                        }

                    }
                }
            }

        }

        public void OnSceneGUI()
        {
            if(reference == null)
            {
                reference = (OcclusionCulling2D)target;
                return;
            }

            if(reference.isInitialized)
            {
                foreach(ObjectSettings obs in reference.objectsSettings)
                {
                    if(obs.theGameObject)
                    {
                        obs.title = obs.theGameObject.name;

                        if(Selection.activeGameObject == reference.gameObject | !Application.isPlaying)
                        {
                            Vector2 addSize = reference._additiveSizeAll + obs.additiveSize;
                            Bounds bounds = reference.GetCombinedBounds(obs.theGameObject);
                            obs.sized = (Vector2)bounds.extents + addSize;
                            obs.center = bounds.center;

                            obs.topRight = new Vector2(obs.center.x + obs.sized.x, obs.center.y + obs.sized.y);
                            obs.topLeft = new Vector2(obs.center.x - obs.sized.x, obs.center.y + obs.sized.y);

                            obs.bottomRight = new Vector2(obs.center.x + obs.sized.x, obs.center.y - obs.sized.y);
                            obs.bottomLeft = new Vector2(obs.center.x - obs.sized.x, obs.center.y - obs.sized.y);

                            obs.right = obs.center.x + obs.sized.x;
                            obs.left = obs.center.x - obs.sized.x;
                            obs.top = obs.center.y + obs.sized.y;
                            obs.bottom = obs.center.x - obs.sized.x;

                            bool checkShowBorders = reference.overrideAllObjectsSettings ? reference.overridingShowBordersAll : obs.showBorders;

                            if(checkShowBorders)
                            {
                                obs.topRight = new Vector2(obs.center.x + obs.sized.x, obs.center.y + obs.sized.y);
                                obs.topLeft = new Vector2(obs.center.x - obs.sized.x, obs.center.y + obs.sized.y);

                                obs.bottomRight = new Vector2(obs.center.x + obs.sized.x, obs.center.y - obs.sized.y);
                                obs.bottomLeft = new Vector2(obs.center.x - obs.sized.x, obs.center.y - obs.sized.y);

                                Handles.color = reference.overrideAllObjectsSettings ? reference.overridingDrawColorAll : obs.drawColor;
                                Handles.DrawLine(obs.topRight, obs.topLeft);
                                Handles.DrawLine(obs.bottomRight, obs.bottomLeft);
                                Handles.DrawLine(obs.topRight, obs.bottomRight);
                                Handles.DrawLine(obs.topLeft, obs.bottomLeft);
                            }

                            bool checkStatic = reference.overrideAllObjectsSettings ? reference.overridingIsStaticAll : obs.isStatic;
                            obs.theGameObject.isStatic = checkStatic;
                        }
                    }
                }
            }
        }
    }
#endif

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {

        timer += Time.deltaTime;
        if (timer > updateRateInSeconds) timer = 0;
        else return;

        float _cameraHalfWidth = _camera.orthographicSize * ((float)Screen.width / (float)Screen.height);
        float _cameraRight = _camera.transform.position.x + _cameraHalfWidth;
        float _cameraLeft = _camera.transform.position.x - _cameraHalfWidth;
        float _cameraTop = _camera.transform.position.y + _camera.orthographicSize;
        float _cameraBottom = _camera.transform.position.y - _camera.orthographicSize;

        foreach(ObjectSettings obs in objectsSettings)
        {
            if(obs.theGameObject)
            {
                bool checkStatic = overrideAllObjectsSettings ? overridingIsStaticAll : obs.isStatic;

                if(!checkStatic)
                {
                    obs.center = GetCombinedBounds(obs.theGameObject).center;
                    obs.right = obs.center.x + obs.sized.x;
                    obs.left = obs.center.x - obs.sized.x;
                    obs.top = obs.center.y + obs.sized.y;
                    obs.bottom = obs.center.y - obs.sized.y;
                }
                bool IsObjectVisibleInCastingCamera = obs.right >= _cameraLeft & obs.left <= _cameraRight & // 좌우 체크
                    obs.top >= _cameraBottom & obs.bottom <= _cameraTop;                                    // 위아래

                obs.theGameObject.SetActive(IsObjectVisibleInCastingCamera);

            }
        }
    }
}
