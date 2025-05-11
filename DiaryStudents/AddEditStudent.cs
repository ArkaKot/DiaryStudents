using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DiaryStudents
{
    public partial class AddEditStudent : Form
    {

        private int _studentId;

        private Student _student;

        private List<Group> _groupList;


        private FileHelper<List<Student>> _fileHelper =
          new FileHelper<List<Student>>(Program.FilePath);


        public AddEditStudent(int id = 0)
        {
            InitializeComponent();

            _studentId = id;
            _groupList = GroupList.GetStudentGroupList("Brak Wyboru");

            CmbGroupStatus();

            GetStudentData();

            tbFirstName.Select();

        }

        private void CmbGroupStatus()
        {
            cmbSelectClas.DataSource = _groupList;
            cmbSelectClas.DisplayMember = "Name";
            cmbSelectClas.ValueMember = "Id";
        }

        private void GetStudentData()
        {
            if (_studentId != 0)
            {

                Text = "Edytowanie danych ucznia";

                var students = _fileHelper.DeserializeFromFile();

                _student = students.FirstOrDefault(x => x.Id == _studentId);

                if (_student == null)
                    throw new Exception("Brak użytkownika o podanym Id");

                FilLTextBoxes();

            }
        }

        private void FilLTextBoxes()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName.ToString();
            tbLastName.Text = _student.LastName.ToString();
            tbMath.Text = _student.Math.ToString();
            tbPhysics.Text = _student.Physics.ToString();
            tbTechnology.Text = _student.Technology.ToString();
            tbPolishLang.Text = _student.PolishLang.ToString();
            tbForeignLang.Text = _student.ForeignLang.ToString();
            rtbComments.Text = _student.Comments.ToString();
            cbBonusActivities.Checked = _student.BonusActivities;
            cmbSelectClas.SelectedItem = _groupList.FirstOrDefault(x => x.Id == _student.GroupId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (cmbSelectClas.Text == "Brak Wyboru")
            {
                MessageBox.Show("Wybierz klasę");
            }
            else
            {
                if (_studentId != 0)
                    students.RemoveAll(x => x.Id == _studentId);
                else
                    AssignIdToNewStudent(students);

                AddNewUserToList(students);

                _fileHelper.SerializeToFile(students);

                Close();
            }
        }

        private void AddNewUserToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Comments = rtbComments.Text,
                ForeignLang = tbForeignLang.Text,
                Math = tbMath.Text,
                Physics = tbPhysics.Text,
                PolishLang = tbPolishLang.Text,
                Technology = tbTechnology.Text,
                BonusActivities = cbBonusActivities.Checked,
                GroupId = (cmbSelectClas.SelectedItem as Group).Id,
                GroupName = (cmbSelectClas.SelectedItem as Group).Name

            };

            students.Add(student);
        }

        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students
               .OrderByDescending(x => x.Id).FirstOrDefault();

            _studentId = studentWithHighestId == null ?
                1 : studentWithHighestId.Id + 1;
        }
    }
}
