using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


#if UNITY_EDITOR
[CustomEditor(typeof(Field))]
public class FieldInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Type"));
        int propertyField = serializedObject.FindProperty("Type").enumValueIndex;
        Field field = (Field) target;
        Sprite[] fieldIcon = Resources.LoadAll<Sprite>("MapIcon");
        switch ((Field.StageType) propertyField)
        {
            case Field.StageType.Battle:
                field.GetComponent<Image>().sprite = fieldIcon[0];
                field.GetComponent<Image>().SetNativeSize();
                field.transform.localScale = new Vector3(0.15f, 0.15f, 1);
                break;
            case Field.StageType.Event:
                field.GetComponent<Image>().sprite = fieldIcon[1];
                field.GetComponent<Image>().SetNativeSize();
                field.transform.localScale = new Vector3(0.1f, 0.1f, 1);
                break;
            case Field.StageType.Boss:
                field.GetComponent<Image>().sprite = fieldIcon[2];
                field.GetComponent<Image>().SetNativeSize();
                field.transform.localScale = new Vector3(0.2f, 0.2f, 1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

[Serializable]
public class Field : MonoBehaviour
{
    [SerializeField] private StageInfo _stageInfo;

    public enum StageType
    {
        Battle,
        Event,
        Boss
    }

    public StageType Type;

    private void OnMouseUp()
    {
        if (!FadeManager.Instance.IsFading)
        {
            FadeManager.Instance.FadeInStartEvent.AddListener(Test);
            FadeManager.Instance.StartFadeOut();
        }
    }

    private void Test()
    {
        Logger.Assert(_stageInfo != null);

        GameObject.Find("Map").SetActive(false);
        switch (Type)
        {
            case StageType.Battle:
                int rand = Random.Range(0, _stageInfo.BattleStageInfos.Length);
                BattleStageInfo stageInfo = _stageInfo.BattleStageInfos[rand];
                
                GameObject gameObject = new GameObject();
                gameObject.transform.parent = GameObject.Find("StageParent").transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
                gameObject.transform.localScale = Vector3.one;
                
                BattleStage battleStage = gameObject.AddComponent<BattleStage>();
                battleStage.BattleStageInfo = stageInfo;
                
                Debug.Log(stageInfo.StageEnemyInfos[0].EnemyType);
                //stageInfo.
                break;
            case StageType.Event:
                break;
            case StageType.Boss:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        //StageManager.Instance.OpenStage(stage);
    }
}
