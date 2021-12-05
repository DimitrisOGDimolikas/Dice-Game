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

namespace DiceGame
{
    public partial class startForm : Form
    {
        public startForm()
        {
            InitializeComponent();
        }

        // Το Path του αρχείου κειμένου στο οποίο αποθηκεύονται τα Usernames
        string path = Environment.CurrentDirectory + "/" + "usernames.text";
                
        // Χρησιμεύει στον έλεγχο του πεδίου Username
        bool emptyUsername = true;
                
        private void startForm_Load(object sender, EventArgs e)
        {
            // Με απλό τρόπο βγάζω το Focus από το πεδίο Username
            this.ActiveControl = label1;

            // Προσπαθώ να δημιουργήσω μια λίστα με τους αριθμούς από το αρχείο
            using (StreamReader read = new StreamReader(path))
            {
                List<int> numbers = new List<int>();

                while (true)
                {
                    // Προσπαθώ να βρω το μέγιστο σκορ που υπάρχει αυτή τη στιγμή στο αρχείο
                    string FindMax = read.ReadLine();

                    // Σε περίπτωση που δεν υπάρχει κανένα στοιχείο βγαίνει σχετικό μήνυμα 
                    if (FindMax == null)
                    {
                        textBox1.Text = ("No high score yet...");
                        break;
                    }
                                       
                    if (Int32.TryParse(FindMax, out int usernames))
                    {
                        numbers.Add(usernames);
                    }
                }
                // Εδώ είναι το μόνο σημείο το οποίο δεν έφερα εις πέρας, θα εκτιμούσα να δω την λύση, ευχαριστώ πολύ
                //textBox1.Text = ("" + numbers.Max());
            }
        }

        // Επιπλέον κουμπί που κλεινει την εφαρμογή
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        // Κουμπί εκκίνησης παιχνιδιού
        private void goButton_Click(object sender, EventArgs e)
        {            
            // Δημιουργία αρχείου κειμένου σε περίπτωση μη ύπαρξης αυτού
            if (!File.Exists(path))
            {
                File.CreateText(path);
                //MessageBox.Show("File created sucessfully!");
            }

            // Διαδικασία ελέγχου ύπαρξης Username και επιλογής επιπέδου
            if (levelOne.Checked == true && !emptyUsername)
            {
                // Εγγραφή Username στο αρχείο κειμένου
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write(usernameTextBox.Text);
                }

                // Μετάβαση σε νέα καρτέλα
                this.Hide();
                Form2 fm = new Form2();
                fm.Show();
                
            }
            else if (levelTwo.Checked == true && !emptyUsername)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write(usernameTextBox.Text);
                }

                this.Hide();
                Form3 fm = new Form3();
                fm.Show();
            }
            else if (levelThree.Checked == true && !emptyUsername)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write(usernameTextBox.Text);
                }

                this.Hide();
                Form4 fm = new Form4();
                fm.Show();
            }
            else
            {
                // Μήνυμα προς χρήστη
                MessageBox.Show("Please make sure you chose a level and entered a username!");
            }
        }

        // Εισαγωγή Username
        private void usernameTextBox_Enter(object sender, EventArgs e)
        {
            if(usernameTextBox.Text == "Username")
            {
                usernameTextBox.Text = "";
                usernameTextBox.ForeColor = Color.Black;
                emptyUsername = false;
            }
        }

        // Μη συμπλήρωση Username
        private void usernameTextBox_Leave(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "")
            {
                usernameTextBox.Text = "Username";
                usernameTextBox.ForeColor = Color.Silver;
                emptyUsername = true;
            }
        }

        // Η εφαρμογή κλείνει τελείως αν πατήσουμε το Χ
        private void startForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
