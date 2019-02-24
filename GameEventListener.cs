using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public bool ContainsGameObject;
    public UnityEvent Response;
    public EventGameObject ResponseWithObject;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventsRaised()
    {
        Response.Invoke();
    }

    public void OnEventsRaised(GameObject go)
    {
        ResponseWithObject.Invoke(go);
    }
}

[Serializable]
public class EventGameObject : UnityEvent<GameObject> { }

[CustomEditor(typeof(GameEventListener))]
[CanEditMultipleObjects]
public class GameEventListenerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GameEventListener gel = target as GameEventListener;
        EditorGUILayout.BeginHorizontal();
        gel.ContainsGameObject = EditorGUILayout.Toggle("Does this Event contain a GameObject?", gel.ContainsGameObject);
        EditorGUILayout.EndHorizontal();

        gel.Event = (GameEvent) EditorGUILayout.ObjectField("Event", gel.Event, typeof(GameEvent), true);

        if (gel.ContainsGameObject)
        {
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ResponseWithObject"));
        }
        else
        {
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Response"));
        }
        serializedObject.ApplyModifiedProperties();
    }

}