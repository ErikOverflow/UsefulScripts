﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public enum ListenerType
    {
        Standard,
        GameObject,
        Integer
    }
    public GameEvent[] Events;
    public ListenerType listenerType;
    public UnityEvent Response;
    public EventGameObject ResponseWithObject;
    public EventInteger ResponseWithInteger;

    private void OnEnable()
    {
        foreach(GameEvent e in Events)
        {
            e.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        foreach (GameEvent e in Events)
        {
            e.UnregisterListener(this);
        }
    }

    public void OnEventsRaised()
    {
        Response.Invoke();
    }

    public void OnEventsRaised(GameObject go)
    {
        ResponseWithObject.Invoke(go);
    }

    public void OnEventsRaised(int num)
    {
        ResponseWithInteger.Invoke(num);
    }
}

[Serializable]
public class EventGameObject : UnityEvent<GameObject> { }

[Serializable]
public class EventInteger : UnityEvent<int> { }

[CustomEditor(typeof(GameEventListener))]
[CanEditMultipleObjects]
public class GameEventListenerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GameEventListener gel = target as GameEventListener;
        EditorGUILayout.BeginHorizontal();
        gel.listenerType = (GameEventListener.ListenerType) EditorGUILayout.EnumPopup("Payload Type: ", gel.listenerType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Events"), true);

        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Response"));
        if (gel.listenerType == GameEventListener.ListenerType.GameObject)
        {
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ResponseWithObject"));
        }
        if (gel.listenerType == GameEventListener.ListenerType.Integer)
        {
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ResponseWithInteger"));
        }
        serializedObject.ApplyModifiedProperties();
    }

}