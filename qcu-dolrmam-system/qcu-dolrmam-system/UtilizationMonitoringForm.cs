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
	}
}
