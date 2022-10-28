using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public enum StageType { Event, Battle }

    public struct StageInfo
    {
        public StageType type;
        public GameObject mapIcon;

        public StageInfo(StageType initType, Transform parent, Vector3 createPos)
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
            mapIcon.transform.localPosition = createPos;
        }
    }

    Queue<StageInfo> mapQueue;

    private float IconWidth;

    [SerializeField]
    Transform createPos;

    private void Awake()
    {
        mapQueue = new Queue<StageInfo>();
        IconWidth = Resources.Load<GameObject>("EventMapIcon").GetComponent<Image>().rectTransform.sizeDelta.x;
    }

    public void addStage(StageType type)
    {
        mapQueue.Enqueue(new StageInfo(type, this.transform, createPos.localPosition));
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
        Vector3 lastPos = transform.position;
        foreach(StageInfo value in mapQueue)
        {
            value.mapIcon.GetComponent<IconMover>().targetPos = lastPos;

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
