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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;

namespace Demo
{
    /// <summary>
    /// Author: Alex McGill (40276245)
    /// Description: Main window for a Student Module Management System. Allows adding, searching, listing and removing of students
    /// Date last modified: 21/10/2017
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModuleList store = new ModuleList();
        public MainWindow()
        {
            InitializeComponent();
            // Set the title of the window
            this.Title = "Module Management System";
        }

        // Set the current matric number to the minimum valid matric value
        private int currentMatric = 10001;
        // Set the maximum valid matric number
        const int maximumMatric = 50000;
        // Declare list for storing matric numbers that have been used
        public List<int> usedMatrics = new List<int>();

        /// <summary>
        /// Add the created student to the store of students and the display list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Attempt to make a student from the inputs provided
            Student created = useTextInputs();
            // If the student was created then
            if (created != null)
            {
                // Run the add student method using the created student
                addStudent(created); 
            }   
            // Reset any selection from the list box
            lstEnrolled.SelectedIndex = -1;
        }

        /// <summary>
        /// Gets the text inputs provided by the user and returns a student
        /// </summary>
        /// <returns></returns>
        public Student useTextInputs()
        {
            // Initialise the required variables
            int inputMatric = -1;
            int inputCourseworkMark = -1;
            int inputExamMark = -1;
            bool result;

            // Try to get the matric number, coursework mark and exam mark
            try
            {
                // Generate a matric number using the 'GenerateUniqueMatric' method
                int matric = GenerateUniqueMatric();
                // If a valid matric number was not generated
                if (matric == -1)
                {
                    // Throw an exception telling the user that there are no valid matric numbers left to use
                    throw new ArgumentException("All valid matric numbers have been used (10001 - 50000");
                }
                else
                {
                    // Set the input matric number to the generated matric number
                    inputMatric = matric;
                }
                
                // Try to convert the coursework input string to an integer
                result = Int32.TryParse(txtCoursework.Text, out inputCourseworkMark);
                // If converting the coursework input from a string to an integer failed
                if (!result)
                {
                    // Set the input coursework mark to -1 (out of valid range)
                    inputCourseworkMark = -1;
                }

                // Try to convert the exam input string to an integer
                result = Int32.TryParse(txtExam.Text, out inputExamMark);
                // If converting the exam input from a string to an integer failed
                if (!result)
                {
                    // Set the input exam mark to -1 (out of valid range)
                    inputExamMark = -1;
                }
            }
            // Catch any exceptions
            catch (Exception excep)
            {
                // Display the exception in a message box
                MessageBox.Show(excep.Message);
            }

            // Call the Create Student method using the provided inputs and set the inputStudent as the created student
            Student inputStudent = CreateStudent(inputMatric, inputCourseworkMark, inputExamMark);
            return inputStudent;
        }

        /// <summary>
        /// Generate a unique matric number that has not been used previously
        /// </summary>
        /// <returns></returns>
        public int GenerateUniqueMatric()
        {
            // Initialise the final matric number
            int finalMatric = -1;

            // While the matric counter is still in the valid matric range
            while (currentMatric <= maximumMatric)
            {
                if (!(usedMatrics.Contains(currentMatric)))
                {
                    // Set the final matric number to be used to the current matric counter value
                    finalMatric = currentMatric;
                    break;
                }
                else
                {
                    // Increment the matric counter
                    currentMatric++;
                }
            }
            // Return the final matric
            return finalMatric;
        }

        /// <summary>
        /// Create a student object using the details input into the text boxes
        /// </summary>
        /// <param name="matric"></param>
        /// <param name="coursework"></param>
        /// <param name="exam"></param>
        public Student CreateStudent(int matric, int coursework, int exam)
        {
            // Create a new student object
            Student studentInput = new Student();
            // Try and set each of the student attributes
            try
            {
                // Set matric to the matric value passed in
                studentInput.Matric = matric;

                // Set first name to the text from the text box
                studentInput.FirstName = txtFirstName.Text;
                // Set the surname to the text from the text box
                studentInput.Surname = txtSurname.Text;
                // Set the coursework mark to the coursework value passed in
                studentInput.CourseworkMark = coursework;
                // Set the exam mark to the exam value passed in
                studentInput.ExamMark = exam;
                // If a date has been selected on the date picker
                if (dPicker.SelectedDate != null)
                {
                    // Set the date of birth to the date selected
                    studentInput.DateOfBirth = dPicker.SelectedDate.Value;
                }
                else
                {
                    // Throw an exception
                    throw new ArgumentException("Date of birth is not valid (DD/MM/YYYY)");
                }
                return studentInput;
            }
            // Catch any exceptions
            catch (Exception excep)
            {
                // Display the exception in a message box
                MessageBox.Show(excep.Message);
            }
            return null;
        }

        /// <summary>
        /// Store a student and add to the student listbox
        /// </summary>
        /// <param name="toAdd"></param>
        private void addStudent(Student toAdd)
        {
            // Store the student in the list of students
            store.add(toAdd);
            // Call the method to clear user inputs
            ClearInputs();
            // Add the matric number to the list of used matric numbers
            usedMatrics.Add(toAdd.Matric);
            // Add the matric number to the list box of matric numbers
            lstEnrolled.Items.Add(toAdd.Matric);
        }

        /// <summary>
        /// Clear all of the user inputs
        /// </summary>
        public void ClearInputs()
        {
            // Clear the first name text box
            txtFirstName.Text = "";
            // Clear the surname text box
            txtSurname.Text = "";
            // Clear the coursework mark text box
            txtCoursework.Text = "";
            // Clear the exam mark text box
            txtExam.Text = "";
            // Clear the date picker field
            dPicker.SelectedDate = null;

            // Clear the matric number text box
            txtEnterMatric.Text = "";
        }

        /// <summary>
        /// Find a student using their matric number as identification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int matric;
            // Convert the matric text input string to an integer
            Int32.TryParse(txtEnterMatric.Text, out matric);
            // Using the matric number, get the student from the list of students with the matching matric number
            Student found = store.find(matric);

            try
            {
                // If there is a student with a matching matric number
                if (found != null)
                {
                    string output = found.ToString();
                    // Set the first name text box to the first name of the chosen student
                    txtFirstName.Text = found.FirstName;
                    // Set the surname text box to the surname of the chosen student
                    txtSurname.Text = found.Surname;
                    // Set the coursework mark text box to the coursework mark of the chosen student
                    txtCoursework.Text = found.CourseworkMark.ToString();
                    // Set the exam mark text box to the exam mark of the chosen student
                    txtExam.Text = found.ExamMark.ToString();
                    // Set the date picker to the date of birth of the chosen student
                    dPicker.SelectedDate = found.DateOfBirth;
                    // Reset the selected student on the list box of matric numbers
                    lstEnrolled.SelectedIndex = -1;
                }
                else
                {
                    // Tell the user that there isn't a student with the chosen matric number
                    throw new ArgumentException("There is not a student with that matric number");
                }
            }

            // Catch any exceptions
            catch (Exception excep)
            {
                // Display the exception in a message box
                MessageBox.Show(excep.Message);
            }

        }

        /// <summary>
        /// Delete a student from the student list and enrolled list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Initialise the value used for matric
            int matric;
            // Convert the matric number string input to an integer
            Int32.TryParse(txtEnterMatric.Text, out matric);
            
            // Get the student with the matching matric number from the student store list
            Student delete = store.find(matric);
            // Get the size of the enrolled students list
            int enrolledSize = lstEnrolled.Items.Count - 1;

            // Create a new list to store the matric numbers that aren't being deleted
            List<string> toKeep = new List<string>();
            // If there does actually exist a student with the chosen matric number
            try
            {
                if (delete != null)
                {
                    // Loop through the list of enrolled students
                    for (int i = enrolledSize; i >= 0; i--)
                    {
                        // If the current enrolled student matric number isn't the matric of the student to delete
                        if (lstEnrolled.Items[i].ToString() != matric.ToString())
                        {
                            // Add the matric number to the the list of matrics to keep
                            toKeep.Add(lstEnrolled.Items[i].ToString());
                        }
                    }
                    // Reset the selected item from the list box
                    lstEnrolled.SelectedIndex = -1;
                    // Clear everything from the list
                    lstEnrolled.Items.Clear();
                    // Loop backwards through each matric number from the list of matrics to keep
                    for (int i = toKeep.Count - 1; i >= 0; i--)
                    {
                        // Add the current matric number back to the list of enrolled students
                        lstEnrolled.Items.Add(toKeep[i]);
                    }

                    // Delete the student with the matching matric number from the list of students 'store'
                    store.delete(matric);
                    // Reset all of the user inputs
                    ClearInputs();
                }
                else
                {
                    // Throw an exception
                    throw new ArgumentException("There is not a student with that matric number");
                }
            }
            // Catch any exceptions
            catch (Exception excep)
            {
                // Display the exception in a message box
                MessageBox.Show(excep.Message);
            }
            
        }

        /// <summary>
        /// Clear all of the input text boxes and unselect any students from the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Reset all of the user inputs
            ClearInputs();
            // Reset the selection in the list box
            lstEnrolled.SelectedIndex = -1;
        }

        /// <summary>
        /// Get the matric number of the selected student and pass to the display method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstEnrolled_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If a student matric number on the list has been selected
            if (lstEnrolled.SelectedIndex != -1)
            {
                // Display the selected student
                DisplaySelected(lstEnrolled.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Display the details of the student that the matric number belongs to
        /// </summary>
        /// <param name="matricString"></param>
        public void DisplaySelected(string matricString)
        {
            // Convert the matric number string to an integer
            int matric = Int32.Parse(matricString);
            // Get the details of the student with the matching matric number
            Student selected = store.find(matric);

            // Set the matric number text box to the selected student matric number
            txtEnterMatric.Text = selected.Matric.ToString();
            // Set the first name text box to the selected student first name
            txtFirstName.Text = selected.FirstName;
            // Set the surname text box to the selected student surname
            txtSurname.Text = selected.Surname;
            // Set the coursework text box to the selected student coursework mark
            txtCoursework.Text = selected.CourseworkMark.ToString();
            // Set the exam text box to the selected student exam mark
            txtExam.Text = selected.ExamMark.ToString();
            // Set the date picker to the selected student date of birth
            dPicker.SelectedDate = selected.DateOfBirth;
    }

        /// <summary>
        /// Open a new window with the list of student strings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {
            // Declare a temporary student
            Student current = new Student();

            // Create a list of matrics using the matrics from the store
            List<int> matrics = store.matrics;
            // Create a list of strings for the string representations of each student
            List<string> studentStrings = new List<string>();
            // Loop through the number of matric numbers in the matric list
            for (int i = 0; i < matrics.Count; i++)
            {
                // Set the current student to the current student
                current = store.find(matrics[i]);
                // Add the current student string to the list of student strings
                studentStrings.Add(current.ToString());
            }
            // Create a new window and pass in the list of student strings
            ListAllStudents listAllWindow = new ListAllStudents(studentStrings);
            // Show the new window for listing all of the student details
            listAllWindow.Show();
        }

        /// <summary>
        /// Automatically create 3 students
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoCreate_Click(object sender, RoutedEventArgs e)
        {
            // Create arrays for first names, last names and date of births
            string[] first_names = { "Danna","Sarina","Vincenzo","Dorethea","Contessa","Lucile","Jessika","Fredrick","Zane","Reid","Shane","Eusebio",
                                       "Elba","Evan","Dean","Pam","Margery","Marsha","Tia","Mahalia","Leon","Marilee","Classie","Dianna","Brandi",
                                       "Adelina","Dorthey","Isaiah","Inez","Dortha","Gregorio","Frances","Twanda","Francina","Candace","Logan","Jutta",
                                       "Hisako","Betsy","Yun","Kristin","Tai","Aleta","Johnny","Aracely","Leatrice","Mercedez","Leighann","Mafalda"
                                   };
            string[] last_names = { "Hurlbert","Prado","Tremper","Polasek","Lamp","Paquette","Melody","Lacher","Swift","Gott","Middaugh","Duer",
                                      "Ruzicka","Dehaan","Choice","Nettles","Pedretti","Funnell","Corbell","Dezern","Rowles","Mill","Bos","Wager",
                                      "Dallman","Edler","Quinto","Tolman","Wooten","Welch","Symonds","Aquilino","Lassen","Hebb","Ostrowski","Conniff",
                                      "Joynt","Cybulski","Desormeaux","Cordes","Reddington","Marquardt","Kohn","Styron","Kemp","Jara","Oser",
                                      "Christine","Delpozo","Cowell"
                                  };
            string[] dates = { "22/11/1991","31/12/1991","24/02/1992","07/03/1992","28/10/1992","29/01/1993","01/01/1994","21/06/1994","18/08/1994",
                                 "11/10/1994","21/01/1995","22/07/1995","26/08/1995","10/11/1995","15/02/1996","05/04/1996","19/04/1996","26/08/1996",
                                 "04/06/1997","19/10/1997","10/02/1998","12/02/1999","18/07/1999","11/08/1999","11/10/1999","09/04/1992","15/06/1992","11/08/1992",
                                 "11/09/1992","31/01/1993","05/03/1993","17/04/1993","10/11/1993","03/01/1995","22/03/1995","04/05/1995","12/09/1995","11/02/1996",
                                 "29/02/1996","24/08/1996","14/12/1996","14/04/1997","07/08/1997","03/09/1997","17/03/1998","19/07/1998","07/09/1998","31/03/1999",
                                 "14/08/1999","23/12/1999","19/01/1992","20/01/1992","23/03/1992","07/08/1992","18/09/1992","26/02/1993","23/05/1993","04/10/1993",
                                 "15/12/1993","10/04/1994","27/04/1994","21/12/1994","23/10/1995","02/03/1996","28/05/1996","23/02/1997","21/12/1997","11/01/1998",
                                 "16/12/1998","17/12/1998","24/12/1998","25/01/1999","20/06/1999","19/10/1999","25/12/1999" 
                             };

            // Initialise a random number
            Random rand = new Random();
            // Loop three times
            for (int i = 0; i < 3; i++)
            {
                // Generate a student using entirely random data for each variable of the student
                AutoStudent(GenerateUniqueMatric(), first_names[rand.Next(0, first_names.Length)], last_names[rand.Next(0, last_names.Length)],
                    rand.Next(0, 21), rand.Next(0, 41), dates[rand.Next(0, dates.Length)]);
            }
        }

        /// <summary>
        /// Use the details passed in to create a student object and add them to the student lists
        /// </summary>
        /// <param name="matric"></param>
        /// <param name="firstName"></param>
        /// <param name="surname"></param>
        /// <param name="courseworkMark"></param>
        /// <param name="examMark"></param>
        /// <param name="stringDateOfBirth"></param>
        public void AutoStudent(int matric, string firstName, string surname, int courseworkMark, int examMark, string stringDateOfBirth)
        {
            // If there isn't already a student with a matching matric number
            if(store.find(matric) == null)
            {
                // Create the new student object
                Student auto = new Student();
                // Set each variable to the relevant value passed into the method
                auto.Matric = matric;
                auto.FirstName = firstName;
                auto.Surname = surname;
                auto.CourseworkMark = courseworkMark;
                auto.ExamMark = examMark;
                auto.DateOfBirth = Convert.ToDateTime(stringDateOfBirth);
                // Add the generated student using the add student method
                addStudent(auto);
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
