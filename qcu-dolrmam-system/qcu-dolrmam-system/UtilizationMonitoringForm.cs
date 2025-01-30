using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace qcu_dolrmam_system
{
    public partial class UtilizationMonitoringForm : Form
    {
        public UtilizationMonitoringForm()
        {
            InitializeComponent();
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            // SUBMIT BUTTON

            string apiUrl = "https://qcu-rest-api-attendance.vercel.app/api/create";

            var formData = new
            {
                name = fullNameTextBox.Text,
                Date = dateTextBox.Text,
                Day = dayTextBox.Text,
                Time = timeTextBox.Text,
                SubjectCode = subjectTextBox.Text,
                FacultyName = facultyTextBox.Text,
                Building = buildingComboBox.Text,
                Room = roomComboBox.Text,
                TerminalNo = terminalTextBox.Text,
                Email = emailTextBox.Text,
                StudentNo = StudentNumTextBox.Text,
                YearAndSection = yrSecTextBox.Text,
                MobileNumber = mobileNumTextBox.Text,
                SystemUnit = systemUnitCheckBox.Checked,
                Monitor = monitorCheckBox.Checked,
                Keyboard = keyboardCheckBox.Checked,
                Mouse = mouseCheckBox.Checked,
                Network = networkConnCheckBox.Checked,
                Remarks = remarksCheckBox.Text
            };

            try
            {
                // Serialize form data to JSON
                string jsonData = JsonConvert.SerializeObject(formData);

                // Send POST request
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Attendance Submitted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string errorResult = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to Submit Attendance. Error: {errorResult}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            {
                errorProvider1.Clear();

                bool isValid = true;

                // Check if the facultyTextBox is empty
                if (string.IsNullOrWhiteSpace(facultyTextBox.Text))
                {
                    errorProvider1.SetError(facultyTextBox, "Textbox is empty.");
                    isValid = false;
                }
                // Check if the input contains only letters
                else if (!System.Text.RegularExpressions.Regex.IsMatch(facultyTextBox.Text, @"^[a-zA-Z\s]+$"))
                {
                    errorProvider1.SetError(facultyTextBox, "Only letters and spaces are allowed.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(subjectTextBox.Text))
                {
                    errorProvider1.SetError(subjectTextBox, "Subject name cannot be empty.");
                    isValid = false;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(subjectTextBox.Text, @"^[a-zA-Z0-9]+$"))
                {
                    errorProvider1.SetError(subjectTextBox, "Only letters and numbers are allowed in subject name (no spaces).");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(StudentNumTextBox.Text))
                {
                    errorProvider1.SetError(StudentNumTextBox, "Student number cannot be empty.");
                    isValid = false;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(StudentNumTextBox.Text, @"^[0-9-]+$"))
                {
                    errorProvider1.SetError(StudentNumTextBox, "Only numbers and dashes are allowed in student number.");
                    isValid = false;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(StudentNumTextBox.Text, @"^\d{2}-\d{4}$"))
                {
                    errorProvider1.SetError(StudentNumTextBox, "Student number must be in this format (e.g., 21-2205 )");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(fullNameTextBox.Text))
                {
                    errorProvider1.SetError(fullNameTextBox, "Textbox is empty.");
                    isValid = false;
                }
                // Check if the input contains only letters
                else if (!System.Text.RegularExpressions.Regex.IsMatch(fullNameTextBox.Text, @"^[a-zA-Z\s]+$"))
                {
                    errorProvider1.SetError(fullNameTextBox, "Only letters and spaces are allowed.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                {
                    errorProvider1.SetError(emailTextBox, "Email cannot be empty.");
                    isValid = false;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(emailTextBox.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    errorProvider1.SetError(emailTextBox, "Please enter a valid email address.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(mobileNumTextBox.Text))
                {
                    errorProvider1.SetError(mobileNumTextBox, "Mobile number cannot be empty.");
                    isValid = false;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(mobileNumTextBox.Text, @"^\d{11}$"))
                {
                    errorProvider1.SetError(mobileNumTextBox, "Mobile number must be exactly 11 digits.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(yrSecTextBox.Text))
                {
                    errorProvider1.SetError(yrSecTextBox, "Year and section cannot be empty.");
                    isValid = false;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(yrSecTextBox.Text, @"^[A-Za-z]+-\d{1}[A-Za-z]$"))
                {
                    errorProvider1.SetError(yrSecTextBox, "Year and section must be in this format (e.g., SBIT-4L).");
                    isValid = false;
                }

                if (buildingComboBox.SelectedIndex == -1)
                {
                    errorProvider1.SetError(buildingComboBox, "Please select a building.");
                    isValid = false;
                }

                if (roomComboBox.SelectedIndex == -1)
                {
                    errorProvider1.SetError(roomComboBox, "Please select a room.");
                    isValid = false;
                }

                if (isValid)
                {
                    MessageBox.Show("Form submitted successfully!");
                    // You can add your form submission logic here
                }
            }

        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            // CANCEL BUTTON

        }

        private void UtilizationMonitoringForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTextBox.Text = DateTime.Now.ToString("dd-MM-yy");
            dayTextBox.Text = DateTime.Now.ToString("dddd");
            timeTextBox.Text = DateTime.Now.ToString("hh-mm-ss");
        }

        private void facultyTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
