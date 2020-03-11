using System.Collections.Generic;
using Characters;
using Skills;
using DG.Tweening;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SlotUi : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float offset;
        [SerializeField] private List<RectTransform> slotIconPositions = new List<RectTransform>();
        [SerializeField] private RectTransform indicator;
        [SerializeField] private GameObject tooltip;
        private List<Skill> _skills;

        private CharacterControl _characterControl; 
        
        public void Init(CharacterControl characterControl)
        {
            _characterControl = characterControl;
            
            TextMeshProUGUI descText = tooltip.transform.Find("Desc").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI propsText = tooltip.transform.Find("Props").GetComponent<TextMeshProUGUI>();
            Image imageIcon = tooltip.transform.Find("Icon").GetComponent<Image>();

            _skills = _characterControl.skillSlot;
            for (int i = 0; i < _skills.Count; i++)
            {
                slotIconPositions[i].GetComponent<Image>().sprite = _skills[i].sprite;
                slotIconPositions[i].GetComponent<Image>().OnPointerEnterAsObservable()
                    .Subscribe(_ =>
                    {
                        //update tooltip ui
                        
                        // show tooltip ui
                        
                        tooltip.SetActive(true);
                    });
                slotIconPositions[i].GetComponent<Image>().OnPointerExitAsObservable()
                    .Subscribe(_ =>
                    {
                        // hide tooltip ui 
                        tooltip.SetActive(false);
                    });
            }
            
            _characterControl.slotAction
            .Where(slotAction => slotAction.Action is SlotAction.ActionType.Increment)
            .Subscribe(async next =>
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(indicator.DOLocalRotate(new Vector3(0, 0, 20), speed / 2f)
                    .SetEase(Ease.OutBounce));
                sequence.Append(indicator.DOLocalRotate(new Vector3(0, 0, 0), speed)
                    .SetEase(Ease.OutElastic));
                
                Vector2 lastPos = slotIconPositions[0].anchoredPosition;
                slotIconPositions[slotIconPositions.Count - 1].anchoredPosition = lastPos;
                for (int i = 1; i < slotIconPositions.Count; i++)
                    sequence.Insert(0,
                        slotIconPositions[i]
                            .DOAnchorPos(
                                new Vector2(slotIconPositions[i].anchoredPosition.x - offset,
                                    slotIconPositions[i].anchoredPosition.y),
                                speed / 2f));
                slotIconPositions.Insert(0,slotIconPositions[slotIconPositions.Count - 1]);
                slotIconPositions.RemoveAt(slotIconPositions.Count - 1);
        
                sequence.Play();
                
                await Utils.AwaitObservable(
                    this.UpdateAsObservable()
                        .TakeWhile(_ => sequence.IsActive() && sequence.IsPlaying())
                );
                
                _characterControl.slotAction.OnNext(new SlotAction { Action = SlotAction.ActionType.Sync});
            }).AddTo(this);
        
        _characterControl.slotAction
            .Where(slotAction => slotAction.Action == SlotAction.ActionType.Decrement)
            .Subscribe(async next =>
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(indicator.DOLocalRotate(new Vector3(0, 0, -20), speed/2)
                    .SetEase(Ease.OutBounce));
                sequence.Append(indicator.DOLocalRotate(new Vector3(0, 0, 0), speed)
                    .SetEase(Ease.OutElastic));
                
                Vector2 lastPos = slotIconPositions[slotIconPositions.Count - 1].anchoredPosition;
                slotIconPositions[0].anchoredPosition = lastPos;
                for (int i = 0; i < slotIconPositions.Count - 1; i++)
                    sequence.Insert(0,
                        slotIconPositions[i]
                            .DOAnchorPos(
                                new Vector2(slotIconPositions[i].anchoredPosition.x + offset,
                                    slotIconPositions[i].anchoredPosition.y),
                                speed / 2));
                slotIconPositions.Add(slotIconPositions[0]);
                slotIconPositions.RemoveAt(0);
        
                sequence.Play();
                
                await Utils.AwaitObservable(
                    this.UpdateAsObservable()
                        .TakeWhile(_ => sequence.IsActive() && sequence.IsPlaying())
                );
                
                _characterControl.slotAction.OnNext(new SlotAction { Action = SlotAction.ActionType.Sync});
            }).AddTo(this);

        _characterControl.slotAction
            .Where(slotAction => slotAction.Action == SlotAction.ActionType.Shuffle)
            .Subscribe(async next =>
            {
                Sequence sequence = DOTween.Sequence();
                Sequence sequenceSlot = DOTween.Sequence();
                Sequence sequenceIndic = DOTween.Sequence();

                Transform parent = slotIconPositions[slotIconPositions.Count - 1].parent;
                Vector3 lastPos = slotIconPositions[slotIconPositions.Count - 1].position;
                
                for (int i = 0; i < next.Skills.Count; i++)
                {
                    var go = new GameObject("Skill" + i);
                    go.transform.parent = parent;
                    go.transform.position = lastPos;
                    go.transform.localScale = Vector3.one;
                    go.AddComponent<RectTransform>().anchoredPosition =
                        new Vector2(((RectTransform) go.transform).anchoredPosition.x + i * offset,
                            ((RectTransform) go.transform).anchoredPosition.y);
                    go.AddComponent<Image>().sprite = next.Skills[i].sprite;
                    slotIconPositions.Add(go.GetComponent<RectTransform>());
                }

                for (int i = 0; i < next.Skills.Count; i++)
                {
                    sequenceIndic.Append(indicator.DORotate(new Vector3(0, 0, 20), speed/5)
                        .SetEase(Ease.OutBounce));
                    sequenceIndic.Append(indicator.DORotate(new Vector3(0, 0, 0), speed/5)
                        .SetEase(Ease.OutElastic));   
                }
                
                for (int i = 0; i < slotIconPositions.Count; i++)
                    sequenceSlot.Insert(0,
                            slotIconPositions[i]
                                .DOAnchorPos(
                                    new Vector2(slotIconPositions[i].anchoredPosition.x - (next.Skills.Count - 1) * offset,
                                        slotIconPositions[i].anchoredPosition.y),
                                    speed / 2.5f * next.Skills.Count));
                
                
                sequence.Insert(0, sequenceSlot);
                sequence.Insert(0, sequenceIndic);
                sequence.Play();
        
                await Utils.AwaitObservable(
                    this.UpdateAsObservable()
                        .SkipWhile(_ => sequence.IsActive() && sequence.IsPlaying())
                        .Take(1)
                );
                
                for (int i = 0; i < next.Skills.Count; i++)
                {
                    Destroy(slotIconPositions[0].gameObject);
                    slotIconPositions.RemoveAt(0);
                }
                
                _characterControl.slotAction.OnNext(new SlotAction { Action = SlotAction.ActionType.Sync});
            }).AddTo(this);
        }
    }
}
