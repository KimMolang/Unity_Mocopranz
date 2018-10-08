using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using ResourceInformation;


public class DropdownCharacterList : MonoBehaviour
{

    // 다녀와서 해야함
    // 캐릭터 리스트
    // 캐릭터 클릭 후
    // 그 전에 할당되어 있던 리소스 모두 해제
    // 해당 캐릭터에 해당하는 스킬 정보 및 값 셋팅 (스킬 Pre Load)

    Dropdown dropdownUI;

    void Awake()
    {
        dropdownUI = GetComponent<Dropdown>();
        dropdownUI.ClearOptions();

        List<string> dropdownOptions = new List<string>();

        int controllableCharacterNum
            = (int)ResourceInformation.Character.ControllableCharacter.MAX;

        for( int i = 0; i < controllableCharacterNum; ++i)
        {
            ResourceInformation.Character.ControllableCharacter characterIndex
                = (ResourceInformation.Character.ControllableCharacter)i;
            dropdownOptions.Add(characterIndex.ToString());
        }

        dropdownUI.AddOptions(dropdownOptions);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
