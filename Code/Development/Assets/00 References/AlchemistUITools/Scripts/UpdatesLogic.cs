using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdatesLogic : MonoBehaviour {

    public Transform backGear;
    public Transform forwardGear;
    public Transform shadowGear;
    public Transform icons;
    public int count = 8;
    public Sprite[] sprites;
    public Transform rightPanels;

    private float _currAngle = 0;
    private float _aimAngle = 0;
    private int _lastIndex = -1;
    private Transform[] _miniGears;
    private Transform[] _icons;
    private Image[] _indicators;
    private Image[] _indicatorsLights;
    private Coroutine _panelEnumerator;
    private int currHidePanelIndex = -1;

    private IEnumerator HideShowPanel(int index, int oldIndex)
    {
        if(oldIndex >= 0)
        {
            currHidePanelIndex = oldIndex;
            Transform oldPanel = rightPanels.GetChild(oldIndex);
            for (int i = oldPanel.childCount - 2; i >= 0; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    oldPanel.GetChild(i).GetChild(0).position = Vector3.Lerp(oldPanel.GetChild(i).GetChild(0).position, oldPanel.GetChild(oldPanel.childCount - 1).position, Time.deltaTime * 20);
                    yield return new WaitForEndOfFrame();
                }
            }
            rightPanels.GetChild(oldIndex).gameObject.SetActive(false);
            currHidePanelIndex = -1;
        }
        Transform panel = rightPanels.GetChild(index);
        panel.gameObject.SetActive(true);
        int n = panel.childCount - 1;
        for(int i = 0; i < panel.childCount - 1; i++)
            panel.GetChild(i).GetChild(0).position = panel.GetChild(n).position;

        yield return new WaitForSeconds(1);
        for (int i = 0; i < panel.childCount - 1; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                panel.GetChild(i).GetChild(0).position = Vector3.Lerp(panel.GetChild(i).GetChild(0).position, panel.GetChild(i).position, Time.deltaTime * 12);
                yield return new WaitForEndOfFrame();
            }
            
        }
        /*for (int i = 0; i < panel.childCount - 1; i++)
            panel.GetChild(i).position = linesPos[i];*/
        panel.gameObject.SetActive(true);
    }

    private void OnClickIcon(int index)
    {
        _aimAngle = 360f * index / count;
        while(_aimAngle - _currAngle > 180)
        {
            _aimAngle -= 360;
        }

        while (_aimAngle - _currAngle < -180)
        {
            _aimAngle += 360;
        }

        if (_lastIndex >= 0)
        {
            _indicators[_lastIndex].color = new Color(0.5f, 0.5f, 1);
            _indicatorsLights[_lastIndex].enabled = false;
        }
        _indicators[index].color = new Color(1, 1, 1);
        _indicatorsLights[index].enabled = true;
        if(_panelEnumerator != null)
        {
            if (currHidePanelIndex >= 0)
                rightPanels.GetChild(currHidePanelIndex).gameObject.SetActive(false);
            StopCoroutine(_panelEnumerator);
        }
        _panelEnumerator = StartCoroutine(HideShowPanel(index, _lastIndex));
        _lastIndex = index;
    }

    // Use this for initialization
    void Start () {
        GameObject icon = icons.GetChild(0).gameObject;
        _miniGears = new Transform[count];
        _icons = new Transform[count];
        _indicators = new Image[count];
        _indicatorsLights = new Image[count];
        for (int i = 0; i < count; i++)
        {
            Transform currIcon = Instantiate(icon).transform;
            currIcon.SetParent(icons);
            currIcon.localPosition = Vector3.zero;
            currIcon.localRotation = Quaternion.identity;
            currIcon.Rotate(new Vector3(0, 0, -360f * i / count));
            currIcon.localScale = Vector3.one;
            int _i = i;
            currIcon.Find("btn").gameObject.GetComponent<Button>().onClick.AddListener(() => OnClickIcon(_i));
            _miniGears[i] = currIcon.Find("gear");
            _icons[i] = currIcon.Find("icon");
            currIcon.Find("icon/icon").GetComponent<Image>().sprite = sprites[i];
            _indicators[i] = currIcon.Find("indicator").GetComponent<Image>();
            _indicators[i].color = new Color(0.5f, 0.5f, 1);
            _indicatorsLights[i] = currIcon.Find("lightIndiator").GetComponent<Image>();
            _indicatorsLights[i].enabled = false;
        }
        OnClickIcon(0);
        icon.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        _currAngle = Mathf.Lerp(_currAngle, _aimAngle + 3 * Mathf.Sin(Time.time * 1.5f), Time.deltaTime * 1.5f);
        backGear.transform.localRotation = Quaternion.identity;
        backGear.transform.Rotate(new Vector3(0, 0, _currAngle));
        shadowGear.transform.localRotation = Quaternion.identity;
        shadowGear.transform.Rotate(new Vector3(0, 0, _currAngle));
        icons.transform.localRotation = Quaternion.identity;
        icons.transform.Rotate(new Vector3(0, 0, _currAngle));
        forwardGear.transform.localRotation = Quaternion.identity;
        forwardGear.transform.Rotate(new Vector3(0, 0, -_currAngle * 2));
        for (int i = 0; i < count; i++)
        {
            _miniGears[i].localRotation = Quaternion.identity;
            _miniGears[i].Rotate(new Vector3(0, 0, _currAngle * 10), Space.Self);
            _icons[i].localRotation = Quaternion.identity;
            _icons[i].Rotate(new Vector3(0, 0, -_currAngle - 15 * Mathf.Sin(Time.time * 1.5f) + 360f * (i ) / count), Space.Self);
        }
    }
}
