using BusinessLayer.DTO;
using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1Framework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var errorMessage = ValidateInputs();
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                MessageBox.Show(errorMessage);
            }
            else {
                var service = new UserService(ConfigurationManager.AppSettings["ConnectionString"]);
                var result = service.Login(new LoginRequest
                {
                    Password = textBox2.Text.Trim(),
                    Username = textBox1.Text.Trim()
                });

                if (!result.IsSucces)
                {
                    MessageBox.Show(result.ErrorMessage);
                }
                else {
                    MessageBox.Show("Login succeeded");
                }
            }
        }

        private string ValidateInputs() {
            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()))
            {
               return "Please insert a username";
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text.Trim()))
            {
                return "Please insert a password";
            }

            return string.Empty;
        }
    }
}
