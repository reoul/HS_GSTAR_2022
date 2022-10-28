using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
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
        Debug.Assert(_stageInfo != null);
        
        GameObject.Find("Map").SetActive(false);

        int rand;
        Stage stage;
        switch (Type)
        {
            case StageType.Battle:
                rand = Random.Range(0, _stageInfo.BattleStageInfos.Length);
                BattleStageInfo stageInfo = _stageInfo.BattleStageInfos[rand];
                
                GameObject stageObj = new GameObject("BattleStage", typeof(BattleStage));
                stageObj.transform.parent = GameObject.Find("StageParent").transform;
                stageObj.transform.localPosition = Vector3.zero;
                stageObj.transform.localRotation = Quaternion.identity;
                stageObj.transform.localScale = Vector3.one;

                BattleStage battleStage = stageObj.GetComponent<BattleStage>();
                battleStage.BattleStageInfo = stageInfo;
                stage = battleStage;
                break;
            case StageType.Event:
                rand = Random.Range(0, _stageInfo.EventStageInfos.Length);
                EventStageInfo eventInfo = _stageInfo.EventStageInfos[rand];

                EventStage eventStage = FindObjectOfType<EventStage>(true);
                stage = eventStage;
                eventStage.InitEvent(eventInfo.Title, eventInfo.Description);
                eventStage.gameObject.SetActive(true);
                eventStage.EventStageInfo = eventInfo;
                
                FadeManager.Instance.FadeInFinishEvent.AddListener(() =>
                {
                    List<string> cardCodes = new List<string>();
                    // todo : 주석 처리한거 되는지 확ㅇ니
                    /*foreach (string cardCode in eventInfo.EventCardTypes)
                    {
                        cardCodes.Add(cardCode);
                    }*/

                    int createdCardCount = CardManager.Instance.CreateCards(cardCodes, FindObjectOfType<Player>(true).gameObject, Vector3.one, 0.2f);
                    Logger.Log($"이벤트 카드 {createdCardCount}장 생성");
                    DiceManager.Instance.CreateDices(createdCardCount, 0.2f);
                });
                break;
            case StageType.Boss:
                throw new NotImplementedException();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        StageManager.Instance.OpenStage(stage);
    }
}
