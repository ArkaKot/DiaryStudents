using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryStudents
{
    public class GroupList
    {
        public static List<Group>  GetStudentGroupList(string NameStatus)
        {
            return new List<Group>
            {
                new Group {Id = 0, Name = NameStatus },
                new Group {Id = 1, Name = "Klasa 1" },
                new Group {Id = 2, Name = "Klasa 2" },
                new Group {Id = 3, Name = "Klasa 3" },
            };
        }
    }
}
