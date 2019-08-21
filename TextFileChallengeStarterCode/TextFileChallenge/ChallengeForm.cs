using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextFileChallenge
{
    public partial class ChallengeForm : Form
    {
        BindingList<UserModel> users = new BindingList<UserModel>();

        public ChallengeForm()
        {
            InitializeComponent();

            WireUpDropDown();
        }

        private void WireUpDropDown()
        {
            MyData md = new MyData();
            users=md.GetUsersAdvanced();
            //users = md.GetUsers();
            usersListBox.DataSource = users;
            usersListBox.DisplayMember = nameof(UserModel.DisplayText);
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            UserModel newUser = new UserModel();
            newUser.FirstName = firstNameText.Text;
            newUser.LastName = lastNameText.Text;
            int age;
            newUser.Age = Convert.ToInt32(agePicker.Value);
            if (isAliveCheckbox.Checked) newUser.IsAlive = true;
            //Add the new user to Users list
            users.Add(newUser);
            usersListBox.DataSource = users;
            usersListBox.DisplayMember = nameof(UserModel.DisplayText);
        }

        private void SaveListButton_Click(object sender, EventArgs e)
        {
            MyData md = new MyData();
            md.WriteUser(users);
        }
    }
}
