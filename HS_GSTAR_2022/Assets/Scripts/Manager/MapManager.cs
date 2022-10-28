using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum StageType { Event, Battle }

    public struct StageInfo
    {
        public StageType type;
        public GameObject mapIcon;

        public StageInfo(StageType initType,Transform parent)
        {
            type = initType;
            if(type == StageType.Event)
            {
                mapIcon = GameObject.Instantiate(Resources.Load<GameObject>("EventMapIcon"), parent);
            }
            else
            {
                mapIcon = GameObject.Instantiate(Resources.Load<GameObject>("BattleMapIcon"), parent);
            }
            mapIcon.transform.position = Vector3.zero;
        }
    }

    Queue<StageInfo> mapQueue;

    private float IconWidth;

    private void Awake()
    {
        mapQueue = new Queue<StageInfo>();
        IconWidth = Resources.Load<GameObject>("EventMapIcon").transform.localScale.x;
    }

    public void addStage(StageType type)
    {
        StageInfo stage = new StageInfo();
        stage.type = type;

        mapQueue.Enqueue(new StageInfo(type, this.transform));
        UpdateIconPosition();
    }

    public void subStage()
    {
        if(mapQueue.TryDequeue(out StageInfo result))
        {
            Destroy(result.mapIcon);
            UpdateIconPosition();
        }
    }

    private void UpdateIconPosition()
    {
        Vector3 lastPos = Vector3.zero;
        foreach(StageInfo value in mapQueue)
        {
            value.mapIcon.transform.position = lastPos;
            lastPos += new Vector3(IconWidth + IconWidth *0.5f, 0, 0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            addStage(StageType.Event);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            addStage(StageType.Battle);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            subStage();
        }
    }
}
