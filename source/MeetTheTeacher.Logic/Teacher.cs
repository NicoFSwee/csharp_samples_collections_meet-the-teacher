using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MeetTheTeacher.Logic
{
    /// <summary>
    /// Verwaltet einen Eintrag in der Sprechstundentabelle
    /// Basisklasse für TeacherWithDetail
    /// </summary>
    public class Teacher : IComparable
    {
        private string _day;
        private string _hour;
        private string _time;
        private string _room;

        public Teacher(string name, string day, string time, string hour, string room)
        {
            _day = day;
            _hour = hour;
            _time = time;
            _room = room;
            Name = name;
        }

        virtual public string GetHtmlForName()
        {
            return Name;
        }

        public string Name { get; set; }

        public int CompareTo(object obj)
        {
            Teacher other = (Teacher)obj;
            return this.Name.CompareTo(other.Name);
        }

        virtual public string GetTeacherHtmlRow()
        {
            return $"<td align=\"left\">{GetHtmlForName()}</td>\n" +
                $"<td align=\"left\">{GetHtmlForDay()}</td>\n" +
                $"<td align=\"left\">{GetHtmlForHour()}</td>\n" +
                $"<td align=\"left\">{GetHtmlForRoom()}</td>";
        }

        private object GetHtmlForRoom()
        {
            return _room;
        }

        private object GetHtmlForHour()
        {
            return _hour;
        }

        private object GetHtmlForDay()
        {
            return _day;
        }
    }
}
