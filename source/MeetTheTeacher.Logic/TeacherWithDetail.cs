using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MeetTheTeacher.Logic
{
    /// <summary>
    /// Klasse, die einen Detaileintrag mit Link auf dem Namen realisiert.
    /// </summary>
    public class TeacherWithDetail : Teacher
    {
        int _id;

        public TeacherWithDetail(string name, string day, string time, string hour, string room, int id) : base(name, day, time, hour, room)
        {
            _id = id;
        }

        public override string GetHtmlForName()
        {
            return $"<a href=\"?id={_id}\">{Name}</a>";
        }

    }
}
