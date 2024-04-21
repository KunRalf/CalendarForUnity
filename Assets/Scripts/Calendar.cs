using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Calendar : MonoBehaviour
    {
        public static event Action<DateSlot> SelectDate;
        
        [SerializeField] private List<DateSlot> _dates;
        private int _month = DateTime.Now.Month;
        private DateTime _curSelectedDate;
        
        private void OnEnable()
        {
            SelectDate += DefaultAllSlots;
        }

        private void OnDisable()
        {
            SelectDate -= DefaultAllSlots;
        }


        public static void OnSelectDate(DateSlot slot)
        {
            SelectDate?.Invoke(slot);
        }
        
        public void Init()
        {
            Debug.Log(_month);
            DateTime dayInCalendar = new DateTime(DateTime.Now.Year, _month, 1);//DateTime.Now.AddMonths(_month);
            
            Debug.Log(dayInCalendar.Year);
            int firstDayOfWeek = DayOfWeek(dayInCalendar.DayOfWeek);
            int daysInMonth = DateTime.DaysInMonth(dayInCalendar.Year, dayInCalendar.Month);
            int index = 0;
            int dayOfMonth = 0;
            bool isStartFill = false;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (!isStartFill)
                    {
                        isStartFill = firstDayOfWeek == index;
                    }
                    if (!isStartFill)
                    {
                        if (index < _dates.Count)
                            _dates[index].NonInteract();
                    }
                    else
                    {
                        
                            dayOfMonth++;
                            if (dayOfMonth <= daysInMonth)
                            {
                                if (index < _dates.Count)
                                {
                                    DateTime date = new DateTime(dayInCalendar.Year, dayInCalendar.Month, dayOfMonth);
                                    _dates[index].SetDateTime(new DateTime(dayInCalendar.Year,dayInCalendar.Month,dayOfMonth));
                                    if(date == _curSelectedDate)
                                        _dates[index].Active();
                                }
                            }
                            else
                            {
                                if (index < _dates.Count)
                                    _dates[index].NonInteract();
                                isStartFill = false;
                            }
                    }
                    index++;
                }
            }
        }

        private void DefaultAllSlots(DateSlot slot)
        {
            foreach (var date in _dates)
            {
                date.Default();
            }
            slot.Active();
            _curSelectedDate = slot.Date;
        }
        
        private int DayOfWeek(DayOfWeek day)
        {
            switch (day)
            {
                case System.DayOfWeek.Monday:
                    return 0;
                case System.DayOfWeek.Tuesday:
                    return 1;
                case System.DayOfWeek.Wednesday:
                    return 2;
                case System.DayOfWeek.Thursday:
                    return 3;
                case System.DayOfWeek.Friday:
                    return 4;
                case System.DayOfWeek.Saturday:
                    return 5;
                case System.DayOfWeek.Sunday :
                    return 6;
                default:
                    return -1;
            }
        }
        
        public void PlusMonth()
        {
            _month++;
            if (_month == 13)
            {
                _month = 1;
            }
            Init();
        }
        
        public void MinusMonth()
        {
            _month--;
            if (_month == 0)
            {
                _month = 12;
            }
            Init();
        }
        
    }
}