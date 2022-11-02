using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StageType
{
    Event,
    Battle
}

public class MapManager : MonoBehaviour
{
    public struct StageInfo
    {
        public StageType type;
        public GameObject mapIcon;

        public StageInfo(StageType initType, Transform parent, Vector3 createPos)
        {
            type = initType;
            if (type == StageType.Event)
            {
                mapIcon = Instantiate(Resources.Load<GameObject>("EventMapIcon"), parent);
            }
            else
            {
                mapIcon = Instantiate(Resources.Load<GameObject>("BattleMapIcon"), parent);
            }

            mapIcon.transform.localPosition = createPos;
        }
    }

    private Queue<StageInfo> _mapQueue;

    private float IconWidth;

    [SerializeField] Transform createPos;

    private void Awake()
    {
        _mapQueue = new Queue<StageInfo>();
        IconWidth = Resources.Load<GameObject>("EventMapIcon").GetComponent<Image>().rectTransform.sizeDelta.x;
    }

    public void AddStage(StageType type)
    {
        _mapQueue.Enqueue(new StageInfo(type, this.transform, createPos.localPosition));
        UpdateIconPosition();
    }

    public void SubStage()
    {
        if (_mapQueue.TryDequeue(out StageInfo result))
        {
            Destroy(result.mapIcon);
            UpdateIconPosition();
        }
    }

    private void UpdateIconPosition()
    {
        Vector3 lastPos = transform.position;
        foreach (StageInfo value in _mapQueue)
        {
            value.mapIcon.GetComponent<IconMover>().targetPos = lastPos;

            lastPos += new Vector3(IconWidth + IconWidth * 0.5f, 0, 0);
        }
    }
}
