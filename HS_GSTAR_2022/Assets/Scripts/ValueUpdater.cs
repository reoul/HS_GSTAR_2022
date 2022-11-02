using UnityEngine;
using TMPro;

public class ValueUpdater : MonoBehaviour
{
    [SerializeField]
    private TMP_Text powText, piercingText, defText;

    struct ValStruct {
        public TMP_Text text;
        public float val;
        public float targetVal;

        public ValStruct(TMP_Text tmpText)
        {
            text = tmpText;
            val = System.Convert.ToInt32(tmpText.text);
            targetVal = val;
        }
    }

    ValStruct powVal, piercingVal, defVal;

    public enum valType{ pow, piercing, def }

    private void Start()
    {
        powVal = new ValStruct(powText);
        piercingVal = new ValStruct(piercingText);
        defVal = new ValStruct(defText);
    }

    private void Update()
    {
        valUpdate(ref powVal);
        valUpdate(ref piercingVal);
        valUpdate(ref defVal);

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddVal(10, valType.pow);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddVal(-10, valType.pow);
        }
    }

    private void valUpdate(ref ValStruct valStruct)
    {
        valStruct.val = Mathf.Lerp(valStruct.val, valStruct.targetVal, 0.025f);

        valStruct.text.text = Mathf.Round(valStruct.val).ToString();

        
        if (System.Convert.ToInt32(valStruct.text.text) > valStruct.targetVal)
        {
            valStruct.text.color = Color.red;
        }
        else if(System.Convert.ToInt32(valStruct.text.text) < valStruct.targetVal)
        {
            valStruct.text.color = Color.green;
        }
        else
        {
            valStruct.text.color = Color.white;
        }
    }


    public void AddVal(int val, valType type)
    {
        switch (type)
        {
            case valType.pow:
                powVal.targetVal += val;
                break;
            case valType.piercing:
                piercingVal.targetVal += val;
                break;
            case valType.def:
                defVal.targetVal += val;
                break;
        }

        if(val > 0)
        {
            SoundManager.Instance.PlaySound("StatU");
        }
        else
        {
            SoundManager.Instance.PlaySound("StatD");
        }
    }
}
