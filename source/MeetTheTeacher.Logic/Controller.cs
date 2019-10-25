using System;
using System.Collections.Generic;
using System.Text;

namespace MeetTheTeacher.Logic
{
    /// <summary>
    /// Verwaltung der Lehrer (mit und ohne Detailinfos)
    /// </summary>
    public class Controller
    {
        private readonly List<Teacher> _teachers = new List<Teacher>();
        private readonly Dictionary<string, int> _details = new Dictionary<string, int>();

        /// <summary>
        /// Liste für Sprechstunden und Dictionary für Detailseiten anlegen
        /// </summary>
        public Controller(string[] teacherLines, string[] detailsLines)
        {
            InitDetails(detailsLines);
            InitTeachers(teacherLines);
        }

        static Controller()
        {
        }

        public int Count => _teachers.Count;

        public int CountTeachersWithoutDetails => Count - CountTeachersWithDetails;

        /// <summary>
        /// Anzahl der Lehrer mit Detailinfos in der Liste
        /// </summary>
        public int CountTeachersWithDetails => _details.Count;

        /// <summary>
        /// Aus dem Text der Sprechstundendatei werden alle Lehrersprechstunden 
        /// eingelesen. Dabei wird für Lehrer, die eine Detailseite haben
        /// ein TeacherWithDetails-Objekt und für andere Lehrer ein Teacher-Objekt angelegt.
        /// </summary>
        /// <returns>Anzahl der eingelesenen Lehrer</returns>
        private void InitTeachers(string[] lines) 
        { 
            foreach (KeyValuePair<string,int> pair in _details)
            {
                foreach(string line in lines)
                {
                    string[] splitLine = line.Split(";");

                    if (String.Compare(splitLine[0], pair.Key, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        _teachers.Add(new TeacherWithDetail(splitLine[0], splitLine[1], splitLine[2], splitLine[3], splitLine[4], _details[pair.Key]));
                    } 
                }
            }

            string[] names = new string[_teachers.Count];
            for (int i = 0; i < _teachers.Count; i++)
            {
                names[i] = _teachers[i].Name;
            }

            foreach(string line in lines)
            {
                string[] splitLine = line.Split(";");

                if (!IsTeacherInList(splitLine[0], names) && splitLine[0].Contains(" "))        //Damit auch nur echte Namen eingefügt werden und keine Überschriften wie Name, Zeit etc.
                {
                    _teachers.Add(new Teacher(splitLine[0], splitLine[1], splitLine[2], splitLine[3], splitLine[4]));
                }
            }
        }

        private bool IsTeacherInList(string teacher, string[] names)
        {

            for (int i = 0; i < names.Length; i++)
            {
                if(names[i].Contains(teacher, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Lehrer, deren Name in der Datei IgnoredTeachers steht werden aus der Liste 
        /// entfernt
        /// </summary>
        public void DeleteIgnoredTeachers(string[] names)
        {
            for (int i = 0; i < _teachers.Count; i++)
            {
                foreach (string name in names)
                {
                    if (_teachers[i].Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        _teachers.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Sucht Lehrer in Lehrerliste nach dem Namen
        /// </summary>
        /// <param name="teacherName"></param>
        /// <returns>Index oder -1, falls nicht gefunden</returns>
        private int FindIndexForTeacher(string teacherName)
        {
            for (int i = 0; i < _teachers.Count; i++)
            {
                if(_teachers[i].Name.Equals(teacherName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }


        /// <summary>
        /// Ids der Detailseiten für Lehrer die eine
        /// derartige Seite haben einlesen.
        /// </summary>
        private void InitDetails(string[] lines)
        {
            foreach (string line in lines)
            {
                string[] splitLine = line.Split(";");

                _details.Add(splitLine[0], Convert.ToInt32(splitLine[1]));
            }
        }

        /// <summary>
        /// HTML-Tabelle der ganzen Lehrer aufbereiten.
        /// </summary>
        /// <returns>Text für die Html-Tabelle</returns>
        public string GetHtmlTable()
        {
            StringBuilder sb = new StringBuilder();

            _teachers.Sort();

            sb.Append("<table id=\"tabelle\"\n");
            sb.Append("\n");
            sb.Append("<tr>\n");
            sb.Append("<th align=\"center\">Name</th>\n");
            sb.Append("<th align=\"center\">Tag</th>\n");
            sb.Append("<th align=\"center\">Zeit</th>\n");
            sb.Append("<th align=\"center\">Raum</th>\n");
            sb.Append("</tr>\n");
            sb.Append("\n");
            sb.Append("\n");

            foreach (Teacher teacher in _teachers)
            {
                sb.Append("<tr>\n");
                sb.Append(teacher.GetTeacherHtmlRow());
                sb.Append("\n");
                sb.Append("</tr>\n");
                sb.Append("\n");
                sb.Append("\n");
            }

            return sb.ToString();
        }

    }
}
