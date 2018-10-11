using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;


// In origin UIMgr, I will set UI dynamic allocation not static allocation.
public class UIMgr_Tool_Skill : SingletonBase<UIMgr_Tool_Skill>
{
    private enum Dropdowns
    {
        Dropdown_SelectCharacter,

        Dropdown_ML,
        Dropdown_Wheel,
        Dropdown_MR,

        Dropdown_Q,
        Dropdown_E,
        Dropdown_Shift,
        Dropdown_Space,

        MAX
    }

    private enum Buttons
    {
        Button_Save,

        MAX
    }

    private Dropdown[] dropdowns;
    private Button[] buttons;

    protected UIMgr_Tool_Skill()
    {
        Init();
    }

    private void Init()
    {
        // dropdowns
        dropdowns = new Dropdown[(int)Dropdowns.MAX];
        dropdowns[(int)Dropdowns.Dropdown_SelectCharacter] = GameObject.Find("/2D/MainCanvas/UI_Tool/Dropdown_SelectCharacter").GetComponent<Dropdown>();

        dropdowns[(int)Dropdowns.Dropdown_ML] = GameObject.Find("/2D/MainCanvas/UI_Tool/Dropdown_SelectSkills/Dropdown_ML").GetComponent<Dropdown>();
        dropdowns[(int)Dropdowns.Dropdown_Wheel] = GameObject.Find("/2D/MainCanvas/UI_Tool/Dropdown_SelectSkills/Dropdown_Wheel").GetComponent<Dropdown>();
        dropdowns[(int)Dropdowns.Dropdown_MR] = GameObject.Find("/2D/MainCanvas/UI_Tool/Dropdown_SelectSkills/Dropdown_MR").GetComponent<Dropdown>();

        dropdowns[(int)Dropdowns.Dropdown_Q] = GameObject.Find("/2D/MainCanvas/UI_Tool/Dropdown_SelectSkills/Dropdown_Q").GetComponent<Dropdown>();
        dropdowns[(int)Dropdowns.Dropdown_E] = GameObject.Find("/2D/MainCanvas/UI_Tool/Dropdown_SelectSkills/Dropdown_E").GetComponent<Dropdown>();
        dropdowns[(int)Dropdowns.Dropdown_Shift] = GameObject.Find("/2D/MainCanvas/UI_Tool/Dropdown_SelectSkills/Dropdown_Shift").GetComponent<Dropdown>();
        dropdowns[(int)Dropdowns.Dropdown_Space] = GameObject.Find("/2D/MainCanvas/UI_Tool/Dropdown_SelectSkills/Dropdown_Space").GetComponent<Dropdown>();

        // buttons
        buttons = new Button[(int)Buttons.MAX];
        buttons[(int)Buttons.Button_Save] = GameObject.Find("/2D/MainCanvas/UI_Tool/Button_BasicFunc/Button_Save").GetComponent<Button>();
    }
}
