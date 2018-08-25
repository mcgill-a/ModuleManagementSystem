using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{

    /// <summary>
    /// Author: Alex McGill
    /// Description: The constructors and methods for the student object
    /// Date last modified: 17/10/2017
    /// </summary>
    public class Student
    {
        // Declare private student variables
        private int _matricNo;
        private string _firstName;
        private string _surname;
        private int _courseworkMark;
        private int _examMark;
        private DateTime _dateOfBirth;

        /// <summary>
        /// Student constructor
        /// </summary>
        public Student()
        {

        }
        
        /// <summary>
        /// Method for getting and setting the student matric number
        /// </summary>
        public int Matric
        {
            get
            {
                return _matricNo;
            }
            set
            {
                // If matric number is not valid (10001 - 50000)
                if (value < 10001 || value > 50000)
                {
                    // Throw an exception stating that the matric number is out of range
                    throw new ArgumentException("Matric number outside of the valid range (10001 > 50000)");
                }
                else
                {
                    // Set the matric number
                    _matricNo = value;
                }                
            }
        }

        /// <summary>
        /// Method for getting and setting the student first name
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                // If the first name is empty or only white space
                if (String.IsNullOrWhiteSpace(value))
                {
                    // Throw an exception stating that the name is not valid
                    throw new ArgumentException("Name is not valid (cannot be empty)");
                }
                else
                {
                    // Set the first name
                    _firstName = value;
                }

            }
        }

        /// <summary>
        /// Method for getting and setting the student surname
        /// </summary>
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                // If the surname is empty or only white space
                if (String.IsNullOrWhiteSpace(value))
                {
                    // Throw an exception stating that the surname is not valid
                    throw new ArgumentException("Surname is not valid (cannot be empty)");
                }
                else
                {
                    // Set the surname
                    _surname = value;
                }
            }
        }

        /// <summary>
        /// Method for getting and setting the student coursework mark
        /// </summary>
        public int CourseworkMark
        {
            get
            {
                return _courseworkMark;
            }
            set
            {
                // If the coursework mark is not valid (0-20)
                if (value < 0 || value > 20)
                {
                    // Throw an exception stating that the coursework mark is not valid
                    throw new ArgumentException("Coursework mark is outside of the valid range (0-20)");
                }
                else
                {
                    // Set the coursework mark
                    _courseworkMark = value;
                }
            }
        }

        /// <summary>
        /// Method for getting and setting the student exam mark
        /// </summary>
        public int ExamMark
        {
            get
            {
                return _examMark;
            }
            set
            {
                // If the exam mark is not valid (0-40)
                if (value < 0 || value > 40)
                {
                    // Throw an exception stating that the exam mark is not valid
                    throw new ArgumentException("Exam mark is outside of the valid range (0-40)");
                }
                else
                {
                    // Set the exam mark
                    _examMark = value;
                }

            }
        }

        /// <summary>
        /// Method for getting and setting the student date of birth
        /// </summary>
        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth.Date;
            }
            set
            {
                // If the date of birth is empty
                if (value == null)
                {
                    // Throw an exception stating that the date of birth is not valid
                    throw new ArgumentException("Date of birth is not valid (DD/MM/YYYY)");
                }
                else
                {
                    // Set the date of birth
                    _dateOfBirth = value;
                }
            }
        }

        /// <summary>
        /// Get all the individual details of a student and add them to a single string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Declare the student string
            string studentString = "";

            // Add the matric number, coursework mark, exam mark, total mark, date of birth, first name and surname to the string
            studentString += Matric +
                "\t" + CourseworkMark +
                "\t" + ExamMark +
                "\t" + getMark(CourseworkMark, ExamMark) +
                "%\t" + DateOfBirth.ToShortDateString() +
                "\t" + FirstName +
                " " + Surname + "\n";

            return studentString;
        }

        /// <summary>
        /// Return the total mark of the weighted exam mark and weighted coursework mark
        /// </summary>
        /// <param name="coursework"></param>
        /// <param name="exam"></param>
        /// <returns></returns>
        public int getMark(int coursework, int exam)
        {
            // Declare the maximum mark for the exam and coursework
            int maxCourseworkMark = 20;
            int maxExamMark = 40;

            // Calculate the student exam mark percentage
            int percentForExam = (int)Math.Round((double)(100 * exam) / maxExamMark);
            // Calculate the student coursework mark percentage
            int percentForCw = (int)Math.Round((double)(100 * coursework) / maxCourseworkMark);

            // Calculated the overall weighted mark using the exam percentage and coursework percentage
            double weightedMark = (percentForExam / 2) + (percentForCw / 2);
            
            // Round the weighted mark to the nearest whole integer
            int roundedTotal = Convert.ToInt32(weightedMark);

            return roundedTotal;
        }
    }
}
