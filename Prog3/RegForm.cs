/**
 *  Grading ID:  L8486
 *  Class:       CIS 199-75
 *  Program:     3
 *  Due date:    11/07/19
 *  Description: Determines the earliest time that a continuing UofL undergraduate student may register for Spring 2020 courses using the priority registration schedule available from the Registrar's site.
 *               And by utilizing parallel arrays to efficiently search for registration times.
 * **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog3
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        // Find and display earliest registration time
        private void FindRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string DAY1 = "November 4"; // 1st day of registration
            const string DAY2 = "November 5"; // 2nd day of registration
            const string DAY3 = "November 6"; // 3rd day of registration
            const string DAY4 = "November 7";  // 4th day of registration
            const string DAY5 = "November 8";  // 5th day of registration
            const string DAY6 = "November 11";  // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            const float SOPHOMORE = 30; // Hours needed to be sophomore
            const float JUNIOR = 60;    // Hours needed to be junior
            const float SENIOR = 90;    // Hours needed to be senior

            // Begin: student contribution 1/3:
            char[] arrAlphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }; // Alphabet array using char datatype
            string[] arrTime1 = { TIME3, TIME3, TIME3, TIME3, TIME4, TIME4, TIME4, TIME4, TIME4, TIME5, TIME5, TIME5, TIME5, TIME5, TIME5, TIME1, TIME1, TIME1, TIME1, TIME2, TIME2, TIME2, TIME2, TIME2, TIME2, TIME2 }; // Time array for Senior/Junior
            string[] arrTime2 = { TIME5, TIME5, TIME1, TIME1, TIME2, TIME2, TIME3, TIME3, TIME3, TIME4, TIME4, TIME4, TIME5, TIME5, TIME5, TIME1, TIME1, TIME2, TIME2, TIME3, TIME3, TIME3, TIME4, TIME4, TIME4, TIME4 }; // Time array for Sophmore/Freshmen
            // Array reference:    'A',   'B',   'C',   'D',   'E',   'F',   'G',   'H',   'I',   'J',   'K',   'L',   'M',   'N',   'O',   'P',   'Q',   'R',   'S',   'T',   'U',   'V',   'W',   'X',   'Y',   'Z'
            bool found = false;
            // End: student contribution

            string lastNameStr;         // Entered last name
            char lastNameLetterCh;      // First letter of last name, as char
            string dateStr = "Error";   // Holds date of registration
            string timeStr = "Error";   // Holds time of registration
            float creditHours;          // Previously earned credit hours
            bool isUpperClass;          // Upperclass or not?

            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                if (float.TryParse(creditHoursTxt.Text, out creditHours) && creditHours >= 0)
                {
                    if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                    {
                        isUpperClass = (creditHours >= JUNIOR);

                        // Juniors and Seniors share same schedule but different days
                        if (isUpperClass)
                        {
                            if (creditHours >= SENIOR)
                                dateStr = DAY1;
                            else // Must be juniors
                                dateStr = DAY2;

                            // Begin: student contribution 2/3:
                            for (int i = 0; i < arrAlphabet.Length && !found; ++i)
                            {
                                if (lastNameLetterCh == arrAlphabet[i])
                                {
                                    found = true;
                                    timeStr = arrTime1[i];
                                }
                            }
                            // End: student contribution

                        }
                        // Sophomores and Freshmen
                        else // Must be soph/fresh
                        {
                            if (creditHours >= SOPHOMORE)
                            {
                                // A-B, P-Z on day one
                                if ((lastNameLetterCh <= 'B') ||  // <= B
                                    (lastNameLetterCh >= 'P'))    // >= P
                                    dateStr = DAY3;
                                else // All other letters on next day
                                    dateStr = DAY4;
                            }
                            else // must be freshman
                            {
                                // A-B, P-Z on day one
                                if ((lastNameLetterCh <= 'B') ||  // <= B
                                    (lastNameLetterCh >= 'P'))    // >= P
                                    dateStr = DAY5;
                                else // All other letters on next day
                                    dateStr = DAY6;
                            }

                            // Begin: student contribution 3/3:
                            for (int i = 0; i < arrAlphabet.Length && !found; ++i)
                            {
                                if (lastNameLetterCh == arrAlphabet[i])
                                {
                                    found = true;
                                    timeStr = arrTime2[i];
                                }
                            }
                            // End: student contribution

                        }

                        // Output results
                        dateTimeLbl.Text = $"{dateStr} at {timeStr}";
                    }
                    else // Not A-Z
                        MessageBox.Show("Make sure last name starts with a letter!");
                }
                else
                    MessageBox.Show("Enter a valid number of credit hours!");
            }
            else // Empty textbox
                MessageBox.Show("Please enter last name!");
        }
    }
}
