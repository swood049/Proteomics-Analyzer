// Authors: Steven Woodard and Sambridee Mahat
// Date: 8-Feb-2020
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace bioinformatics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            referenceTextBox.Text = "protein1.txt";
            tumorTextBox.Text = "protein2.txt";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // take in two files as single strings
            string proteinOne = File.ReadAllText(referenceTextBox.Text).ToLower();
            string proteinTwo = File.ReadAllText(tumorTextBox.Text).ToLower();
            // variable to hold all of the possible amino acid mutation location values max at 1000
            int[] possibleWeaknessLocations = new int[1000];
            //variable to count the mutations
            int count = 0;
            // more specific counters
            int shouldBePositiveIsNegative = 0;
            int shouldBeNegativeIsPositive = 0;
            int shouldBePositiveIsNeutral = 0;
            int shouldBeNeutralIsPositive = 0;
            int shouldBeNegativeIsNeutral = 0;
            int shouldBeNeutralIsNegative = 0;

            //clear any previous entries
            outputTextbox.Clear();

            // loop through the characters in the string, and count the problems by looking at the charges
            // and comparing the changes of the good file with the suspect file.
            for (int i = 0; i < proteinOne.Length; i++)
            {
                // if the correct amino acids are positive, but the ones on file are negative, that's a problem.
                // count that.
                if((proteinOne[i] == 'k' || proteinOne[i] == 'r' || proteinOne[i] == 'h') && 
                    (proteinTwo[i] == 'd' || proteinTwo[i] == 'e'))
                {
                    shouldBePositiveIsNegative++;
                    possibleWeaknessLocations[count] = i;
                    count++;
                }
                // if the correct amino acids are negative, but the ones of file are positive, that's a problem.
                // count it.
                else if((proteinOne[i] == 'd' || proteinOne[i] == 'e') &&
                    (proteinTwo[i] == 'k' || proteinTwo[i] == 'r' || proteinTwo[i] == 'h'))
                {
                    shouldBeNegativeIsPositive++;
                    possibleWeaknessLocations[count] = i;
                    count++;
                }
                // if the correct amino acids are positive, but the ones on file are neutral, that's a big problem.
                else if((proteinOne[i] == 'k' || proteinOne[i] == 'r' || proteinOne[i] == 'h') && 
                    (proteinTwo[i] == 'a' || proteinTwo[i] == 'g' || proteinTwo[i] == 'v' || proteinTwo[i] == 'i' ||
                    proteinTwo[i] == 'w' || proteinTwo[i] == 'f' || proteinTwo[i] == 'p' || proteinTwo[i] == 'm'||
                        proteinTwo[i] == 'l'))
                {
                    shouldBePositiveIsNeutral++;
                    possibleWeaknessLocations[count] = i;
                    count++;
                }
                // if the correct amino acids are neutral, but the ones on file are positive, that's a big problem.
                else if((proteinOne[i] == 'a' || proteinOne[i] == 'g' || proteinOne[i] == 'v' || proteinOne[i] == 'i' ||
                    proteinOne[i] == 'w' || proteinOne[i] == 'f' || proteinOne[i] == 'p' || proteinOne[i] == 'm' ||
                        proteinOne[i] == 'l') && (proteinTwo[i] == 'k' || proteinTwo[i] == 'r' || proteinTwo[i] == 'h'))
                {
                    shouldBeNeutralIsPositive++;
                    possibleWeaknessLocations[count] = i;
                    count++;
                }
                // if the correct amino acids are negative, but the ones of file are neutral, that's a problem (big).
                else if((proteinOne[i] == 'd' || proteinOne[i] == 'e') && (proteinTwo[i] == 'a' || proteinTwo[i] == 'g' || proteinTwo[i] == 'v' || proteinTwo[i] == 'i' ||
                    proteinTwo[i] == 'w' || proteinTwo[i] == 'f' || proteinTwo[i] == 'p' || proteinTwo[i] == 'm' ||
                        proteinTwo[i] == 'l'))
                {
                    shouldBeNegativeIsNeutral++;
                    possibleWeaknessLocations[count] = i;
                    count++;
                }
                // if the correct amino acids are neutral, but the ones on file are negative, that's a problem.
                else if((proteinOne[i] == 'a' || proteinOne[i] == 'g' || proteinOne[i] == 'v' || proteinOne[i] == 'i' ||
                    proteinOne[i] == 'w' || proteinOne[i] == 'f' || proteinOne[i] == 'p' || proteinOne[i] == 'm' ||
                        proteinOne[i] == 'l') && (proteinTwo[i] == 'd' || proteinTwo[i] == 'e'))
                {
                    shouldBeNeutralIsNegative++;
                    possibleWeaknessLocations[count] = i;
                    count++;
                }

            }
            // display the problems or lack thereof.
            if (count > 1)
            {
                outputTextbox.Text = "There are " + count + " serious mutations in this protein.\n";
            }
            else
            {
                outputTextbox.Text += "There are " + count + " serious mutations in this protein.\n";
                outputTextbox.Text += "YAY!!!!!";
            }
            if (shouldBePositiveIsNegative >= 1)
            {
                outputTextbox.Text += "There is/are " + shouldBePositiveIsNegative + " amino acids in the mutated protein that is/are negative and should be positive.\n";
            }
            if(shouldBeNegativeIsPositive >= 1)
            {
                outputTextbox.Text += "There is/are " + shouldBeNegativeIsPositive + " amino acids in the mutated protein that is/are positive and should be negative.\n";
            }
            if (shouldBePositiveIsNeutral >= 1)
            {
                outputTextbox.Text += "There is/are " + shouldBePositiveIsNeutral + " amino acids in the mutated protein that shoud be positive but is/are neutral.\n";
            }
            if (shouldBeNeutralIsPositive >= 1)
            {
                outputTextbox.Text += "There is/are " + shouldBeNeutralIsPositive + " amino acids in the mutated protein that should be neutral but is/are positive.\n";
            }
            if(shouldBeNegativeIsNeutral >= 1)
            {
                outputTextbox.Text += "There is/are " + shouldBeNegativeIsNeutral + " amino acids in the mutated protein that should be negative but is/are neutral.\n";
            }
            if (shouldBeNeutralIsNegative >= 1)
            {
                outputTextbox.Text += "There is/are " + shouldBeNeutralIsNegative + " amino acids in the mutated protein that should be neutral but is/are negative.\n";
            }
            // Make it the output 'pop'
            pictureBox2.Visible = true;
            pictureBox1.Visible = true;
            positiveErrorsLabel.Text = shouldBeNeutralIsPositive + shouldBeNegativeIsPositive + "";
            negativeErrorsLabel.Text = shouldBeNeutralIsNegative + shouldBePositiveIsNegative + "";
            positiveErrorsLabel.Visible = true;
            negativeErrorsLabel.Visible = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                referenceTextBox.Text = openFileDialog1.FileName;

            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                tumorTextBox.Text = openFileDialog2.FileName;
            }
        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }
    }
}
