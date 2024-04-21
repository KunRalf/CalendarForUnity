using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Button))]
    public class DateSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private List<Color> _states;
        private bool _isActive;
        private DateTime _slotDateTime;
        private Button _button;
        private Image _image;

        public DateTime Date => _slotDateTime;
        public bool IsActive => _isActive;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _button.onClick.AddListener(Select);
        }

        public void SetDateTime(DateTime date)
        {
            _slotDateTime = date;
            _text.text = date.Day.ToString();
            _button.interactable = true;
            Default();
        }

        private void Select()
        {
            Calendar.OnSelectDate(this);
        }
        
        public void Active()
        {
            _isActive = true;
            _image.color = _states[1];
        }
        
        public void Default()
        {
            _isActive = false;
            _image.color = !_button.interactable ? _states[2] : _states[0];
        }

        public void NonInteract()
        {
            _button.interactable = false;
            _isActive = false;
            _image.color = _states[2];
            _text.text = string.Empty;
        }
    }
}