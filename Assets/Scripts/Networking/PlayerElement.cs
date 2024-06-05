using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerElement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _nameText;


    public void SetName(string name)
    {
        _nameText.text = name;
    }
}
