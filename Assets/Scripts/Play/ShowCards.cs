using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentCard.Leader;

namespace GwentCard.Play
{
    public class ShowCards : Singleton<ShowCards>
    {
        public enum ShowBehavior { draw, show, leader, replace, nurse, dummy, warhorn, agile }
        public Transform grid;
        public UIPopupList popupList;
        [SerializeField] GameObject obj;
        [SerializeField] UILabel label;
        [SerializeField] UILabel messageLabel;
        [SerializeField] UIScrollView scrollView;
        [SerializeField] UIButton OKButton;
        [SerializeField] UIButton returnButton;
        [HideInInspector] public Transform card;
        [HideInInspector] public Transform totalGrid;
        [HideInInspector] public int totalLine = 0;
        [HideInInspector] public int replaceInt = 0;
        ShowBehavior showBehavior;

        public void Show(ShowBehavior behavior, Transform showGrid, bool repeat)
        {
            showBehavior = behavior;
            popupList.items.Clear();
            popupList.AddItem("近战");
            popupList.AddItem("远程");
            popupList.AddItem("攻城");
            OKButton.onClick.Clear();
            returnButton.onClick.Clear();
            EventDelegate.Add(returnButton.onClick, () => Hide());

            if (!repeat)
            {
                BlackShow.GetInstance().Show(true);
                PlayerController.GetInstance().obj.SetActive(false);
                EnemyController.GetInstance().obj.SetActive(false);
                obj.SetActive(true);
            }

            switch (showBehavior)
            {
                case ShowBehavior.draw:
                    label.text = "请选择要打出的牌";
                    goto default;
                case ShowBehavior.show:
                    label.text = "显示卡牌";
                    goto default;
                case ShowBehavior.leader:
                    label.text = "领导牌";
                    PlayerLeaderBehavior leaderBehavior = LeaderController.GetInstance().obj[0].GetComponent<PlayerLeaderBehavior>();
                    messageLabel.text = leaderBehavior.message;
                    messageLabel.gameObject.SetActive(true);
                    popupList.gameObject.SetActive(false);
                    OKButton.gameObject.SetActive(true);
                    OKButton.GetComponent<UIButton>().isEnabled = leaderBehavior.IsEnabled;
                    EventDelegate.Add(OKButton.onClick, () => leaderBehavior.Play());
                    break;
                case ShowBehavior.replace:
                    label.text = string.Format("请选择要替换的牌 {0}/2", replaceInt);
                    EventDelegate.Add(returnButton.onClick, () => GameController.GetInstance().StartGame());
                    goto default;
                case ShowBehavior.nurse:
                    label.text = "从墓地中打出卡牌";
                    totalGrid = showGrid;
                    messageLabel.gameObject.SetActive(false);
                    popupList.gameObject.SetActive(false);
                    OKButton.gameObject.SetActive(false);
                    EventDelegate.Add(returnButton.onClick, () => EnemyController.GetInstance().Play(EnemyController.GetInstance().grids[1]));
                    break;
                case ShowBehavior.dummy:
                    label.text = "请选择要替换的牌";
                    messageLabel.gameObject.SetActive(false);
                    popupList.gameObject.SetActive(true);
                    OKButton.gameObject.SetActive(false);
                    break;
                case ShowBehavior.warhorn:
                    label.text = "战争号角";
                    messageLabel.gameObject.SetActive(false);
                    popupList.gameObject.SetActive(true);
                    OKButton.gameObject.SetActive(true);
                    OKButton.GetComponent<UIButton>().isEnabled = true;
                    EventDelegate.Add(OKButton.onClick, () => WarhornController.GetInstance().Warhorn());
                    break;
                case ShowBehavior.agile:
                    label.text = "请选择出牌的排";
                    messageLabel.gameObject.SetActive(false);
                    popupList.gameObject.SetActive(true);
                    popupList.items.Remove("攻城");
                    OKButton.gameObject.SetActive(true);
                    OKButton.GetComponent<UIButton>().isEnabled = true;
                    EventDelegate.Add(OKButton.onClick, () => AgileController.GetInstance().Agile());
                    break;
                default:
                    totalGrid = showGrid;
                    messageLabel.gameObject.SetActive(false);
                    popupList.gameObject.SetActive(false);
                    OKButton.gameObject.SetActive(false);
                    break;
            }

            if (repeat) grid.DestroyChildren();

            for (int i = 0; i < showGrid.childCount; i++)
            {
                GameObject card = Instantiate(showGrid.GetChild(i).gameObject, grid);
                UISprite sprite = card.GetComponent<UISprite>();
                sprite.width = 250;
                sprite.height = 450;
                UIButton cardButton = card.GetComponent<UIButton>();
                switch (showBehavior)
                {
                    case ShowBehavior.draw:
                        EventDelegate.Add(cardButton.onClick, () => card.GetComponent<CardBehavior>().Play());
                        cardButton.enabled = true;
                        break;
                    case ShowBehavior.show:
                        cardButton.enabled = false;
                        break;
                    case ShowBehavior.replace:
                        EventDelegate.Add(cardButton.onClick, () => card.GetComponent<CardBehavior>().Replace());
                        cardButton.enabled = true;
                        break;
                    case ShowBehavior.nurse:
                        EventDelegate.Add(cardButton.onClick, () => card.GetComponent<CardBehavior>().Play());
                        cardButton.enabled = true;
                        break;
                    case ShowBehavior.dummy:
                        EventDelegate.Add(cardButton.onClick, () => card.GetComponent<CardBehavior>().Dummy());
                        cardButton.enabled = true;
                        break;
                    case ShowBehavior.warhorn:
                        cardButton.enabled = false;
                        break;
                    case ShowBehavior.agile:
                        cardButton.enabled = false;
                        break;
                }
                card.GetComponent<UIDragScrollView>().scrollView = scrollView;
                card.GetComponent<BoxCollider>().size = new Vector3(250, 450, 1);
            }

            grid.GetComponent<UIGrid>().Reposition();
        }

        public void ShowLeader(ArrayList cardList, Transform targetGrid, bool buttonEnabled, EventDelegate.Callback returnCallBack)
        {
            OKButton.gameObject.SetActive(false);
            returnButton.onClick.Clear();
            EventDelegate.Add(returnButton.onClick, returnCallBack);
            grid.DestroyChildren();
            totalGrid = targetGrid;

            if (LeaderController.GetInstance().obj[0].GetComponent<PlayerLeaderBehavior>().GetType() == typeof(MonsterBehavior3))
            {
                if (replaceInt < 2)
                    label.text = string.Format("请选择要丢弃的卡牌 {0}/2", replaceInt);
                else
                    label.text = "请从牌组中选取卡牌";
            }

            for (int i = 0; i < cardList.Count; i++)
            {
                GameObject card = cardList[i] as GameObject;
                card.transform.SetTarget(grid);
                UISprite sprite = card.GetComponent<UISprite>();
                sprite.width = 250;
                sprite.height = 450;
                card.GetComponent<UIButton>().enabled = buttonEnabled;
                card.GetComponent<UIDragScrollView>().scrollView = scrollView;
                card.GetComponent<BoxCollider>().size = new Vector3(250, 450, 1);
            }

            grid.GetComponent<UIGrid>().Reposition();
        }

        public void Hide()
        {
            totalGrid = null;
            grid.DestroyChildren();
            BlackShow.GetInstance().Show(false);
            PlayerController.GetInstance().obj.SetActive(true);
            EnemyController.GetInstance().obj.SetActive(true);
            obj.SetActive(false);
        }

        public void LineChanged()
        {
            totalLine = popupList.GetItemsInt();
            Show(showBehavior, PlayerController.GetInstance().grids[totalLine + 2], true);
        }
    }
}