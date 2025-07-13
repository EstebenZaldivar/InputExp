using UnityEngine;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{
    Button m_swordButton;
    Button m_axeButton;
    Button m_magicButton;
    Button m_stealthButton;

    Button m_earthButton;
    Button m_monsterButton;
    Button m_healButton;
    Button m_rageButton;

    Button[] m_buttonSet1 = new Button[4];
    Button[] m_buttonSet2 = new Button[4];


    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        m_swordButton = root.Q<Button>("sword-icon__button");
        m_axeButton = root.Q<Button>("axe-icon__button");
        m_magicButton = root.Q<Button>("magic-icon__button");
        m_stealthButton = root.Q<Button>("stealth-icon__button");

        m_buttonSet1[0] = m_swordButton;
        m_buttonSet1[1] = m_axeButton;
        m_buttonSet1[2] = m_magicButton;
        m_buttonSet1[3] = m_stealthButton;

        m_earthButton = root.Q<Button>("earth-icon__button");
        m_monsterButton = root.Q<Button>("monster-icon__button");
        m_healButton = root.Q<Button>("heal-icon__button");
        m_rageButton = root.Q<Button>("rage-icon__button");

        m_buttonSet2[0] = m_earthButton;
        m_buttonSet2[1] = m_monsterButton;
        m_buttonSet2[2] = m_healButton;
        m_buttonSet2[3] = m_rageButton;

    }

    private void OnEnable()
    {
        ButtonActionsSubscribe();
    }

    private void OnDisable()
    {
        ButtonActionsUnsubscribe();
    }

    private void ButtonActionsSubscribe()
    {
        m_swordButton.clicked += OnSwordButtonClicked;
        m_axeButton.clicked += OnAxeButtonClicked;
        m_magicButton.clicked += OnMagicButtonClicked;
        m_stealthButton.clicked += OnStealthButtonClicked;

        m_earthButton.clicked += OnEarthButtonClicked;
        m_monsterButton.clicked += OnMonsterButtonClicked;
        m_healButton.clicked += OnHealButtonClicked;
        m_rageButton.clicked += OnRageButtonClicked;
    }

    private void ButtonActionsUnsubscribe()
    {
        m_swordButton.clicked -= OnSwordButtonClicked;
        m_axeButton.clicked -= OnAxeButtonClicked;
        m_magicButton.clicked -= OnMagicButtonClicked;
        m_stealthButton.clicked -= OnStealthButtonClicked;

        m_earthButton.clicked -= OnEarthButtonClicked;
        m_monsterButton.clicked -= OnMonsterButtonClicked;
        m_healButton.clicked -= OnHealButtonClicked;
        m_rageButton.clicked -= OnRageButtonClicked;
    }

    private void OnSwordButtonClicked() => InventoryButtonClicked("Sword");
    private void OnAxeButtonClicked() => InventoryButtonClicked("Axe");
    private void OnMagicButtonClicked() => InventoryButtonClicked("Magic");
    private void OnStealthButtonClicked() => InventoryButtonClicked("Stealth");
    private void OnEarthButtonClicked() => InventoryButtonClicked("Earth");
    private void OnMonsterButtonClicked() => InventoryButtonClicked("Monster");
    private void OnHealButtonClicked() => InventoryButtonClicked("Heal");
    private void OnRageButtonClicked() => InventoryButtonClicked("Rage");

    private void InventoryButtonClicked(string message)
    {
        Debug.Log(message + " clicked");
    }

}


