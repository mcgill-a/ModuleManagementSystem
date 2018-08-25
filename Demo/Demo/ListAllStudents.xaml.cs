using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Demo
{
    /// <summary>
    /// Author: Alex McGill
    /// Description: Takes a list of strings including all the student details and displays them in a list box
    /// Date last modified: 05/10/2017
    /// </summary>
    public partial class ListAllStudents : Window
    {
        public ListAllStudents(List<string> students)
        {
            InitializeComponent();
            // Set the title of the window
            this.Title = "Student Details";
            // Display the list students on the list box
            DisplayAll(students);
        }

        /// <summary>
        /// Add each student from the list of student strings to the list box
        /// </summary>
        /// <param name="students"></param>
        public void DisplayAll(List<string> students)
        {
            // Declare the header
            string header = "";
            // Add 'matric', 'mark', 'date of birth' and 'full name' to the header
            header = "Matric" + "\tCW" + "\tExam" + "\tTotal" + "\tDate of Birth" + "\tFull Name" + "\n";
            // Add the header to the top of the list
            lstListAll.Items.Add(header);
            // Loop through the list of student strings
            for (int i = 0; i < students.Count; i++)
            {
                // Add the current student string to the list box
                lstListAll.Items.Add(students[i]);
            }
        }

        /// <summary>
        /// Closes the current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
