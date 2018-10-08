using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

using ResourceInformation;


public class DropdownSkillList : MonoBehaviour
{
    Dropdown dropdownUI;

    void Awake()
    {
        dropdownUI = GetComponent<Dropdown>();
        dropdownUI.ClearOptions();


        List<string> dropdownOptions = new List<string>();
        DirectoryInfo di
            = new DirectoryInfo(ResourceInformation.Skill.Path.Skill);

        // https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.getfiles?view=netframework-4.7.2#System_IO_DirectoryInfo_GetFiles
        FileInfo[] fileInfo = di.GetFiles("*.cs");

        foreach(FileInfo iter in fileInfo)
        {
            dropdownOptions.Add(iter.Name);
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
