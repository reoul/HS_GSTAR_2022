using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ItemEffectInvokeTimeType
{
    [InspectorName("전투 시작 시")] BattleStart,
    [InspectorName("전투 종료 시")] BattleFinish,
    [InspectorName("공격 후")] AttackFinish,
}

public enum ItemEffectType
{
    [InspectorName("체력 회복")] Heal,
    [InspectorName("공격력 증가")] OffensivePower,
    [InspectorName("관통 데미지 증가")] PiercingDamage,
    [InspectorName("방어력 증가")] DefensivePower,
    [InspectorName("최대 체력 증가")] MaxHp,
    [InspectorName("골드 획득")] Gold,
}

#if UNITY_EDITOR
[CustomEditor(typeof(ItemInfo))]
public class ItemInfoInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Name"), new GUIContent("이름"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Description"), new GUIContent("설명"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("EffectInvokeTimeType"), new GUIContent("발동 시점"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("EffectType"), new GUIContent("발동 효과 타입"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Num"), new GUIContent("수치"));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

[Serializable]
[CreateAssetMenu(fileName = "ItemInfo", menuName = "StageInfo/ItemInfo", order = int.MaxValue)]
public class ItemInfo : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemEffectInvokeTimeType EffectInvokeTimeType;
    public ItemEffectType EffectType;
    public int Num;
}
