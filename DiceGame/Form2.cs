using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace DiceGame
{
    public partial class Form2 : Form
    {
        // Τρόπος με τον οποίο κρύβω τον κέρσορα από το Textbox που χρησιμοποιώ ως Countdown
        [DllImport("user32")]
        public static extern bool HideCaret(IntPtr hWnd);

        // Μεταβλητές που χρειάζομαι για διεργασίες
        bool moveRight = true;
        bool moveUp = true;

        int score = 0;
        int clickCounter = 0;

        int firstClickValue = 0;
        int clickValue = 0;

        public Form2()
        {
            InitializeComponent();
        }

        // Το Path του αρχείου κειμένου στο οποίο αποθηκεύονται τα σκορ
        string path = Environment.CurrentDirectory + "/" + "usernames.text";

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.GotFocus += new EventHandler(textBox1_GotFocus);

            // Timer υπεύθυνος για το Countdown
            timer2.Start();

            // Επιλογή τυχαίας έδρας ζαριού κατά την εκκίνηση
            // Αρχικοποίηση τυχαίου στοιχείου
            var random = new Random();

            // Παίρνω τυχαία την θέση μιας έδρας από το σύνολο
            var startIndex = random.Next(this.imageList1.Images.Count);
            
            // Αντιστοιχώ την επιλεγμένη θέση με την εικόνα στην οποία ανήκει
            Image image = this.pictureBox1.Image = this.imageList1.Images[startIndex];

            // Προβολή επιλεγμένης έδρας
            this.pictureBox1.Image = image;

            // Τιμή έδρας ζαριού κατά την εκκίνηση
            // Τα indexes είναι 0-5 ενώ οι αξίες των εδρών 1-6 αυτή η διαφορά λύνεται με το +1
            firstClickValue = startIndex + 1;
        }

        // Κρύβω τον κέρσορα από το Countdown
        void textBox1_GotFocus(object sender, EventArgs e)
        {
            HideCaret(textBox1.Handle);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Υπολογισμός σκορ
            if (clickCounter == 0)
            {
                score = score + firstClickValue;
            }
            else
            {
                score = score + clickValue;
            }

            // Ίδια διαδικασία με παραπάνω
            var random = new Random();
            var index = random.Next(this.imageList1.Images.Count);

            Image image = this.pictureBox1.Image = this.imageList1.Images[index];
            this.pictureBox1.Image = image;

            // Ίδια λογική με παραπάνω
            clickValue = index + 1;

            // Με αυτή την μεταβλητή καταφέρνω να περάσω την αξία της έδρας από την οποία ξεκινά το παιχνίδι καθώς με άλλον τρόπο
            // θα παρέβλεπα την αξία της δίνοντας λανθασμένο σκορ
            clickCounter++;
        }

        // Δημιουργία της κίνησης του ζαριού βάσει των συνόρων της καρτέλας και την λογική μιας μπάλας που αναπηδά
        // κάθε φορά που βρίσκει εμπόδιο
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (moveRight)
            {
                pictureBox1.Left += 5;
            }
            else
            {
                pictureBox1.Left -= 5;
            }

            if (moveUp)
            {
                pictureBox1.Top -= 5;
            }
            else
            {
                pictureBox1.Top += 5;
            }
            
            if(pictureBox1.Left <= this.ClientRectangle.Left)
            {
                moveRight = true;
            }

            if (pictureBox1.Left + pictureBox1.Width >= this.ClientRectangle.Right)
            {
                moveRight = false;
            }

            if (pictureBox1.Top <= this.ClientRectangle.Top)
            {
                moveUp = false;
            }

            if (pictureBox1.Top + pictureBox1.Height >= this.ClientRectangle.Bottom)
            {
                moveUp = true;
            }
        }

        // Μεταβλητές χρήσιμες για το Countdown
        int mil = 0;
        int sec = 0;
        int min = 0;
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            mil++;
            if(mil > 99)
            {
                mil = 0;
                sec++;
            }
            if(sec == 60)
            {
                sec = 0;
                min++;
                timer2.Stop();

                // Σταματάει το ζάρι και κατά συνέπεια το παιχνίδι
                timer1.Stop();

                // Εμφάνιση σκορ και επιλογή για ακόμα ένα παιχνίδι ή έξοδο
                DialogResult dialogResult = MessageBox.Show("Your score is: " + score + " Want to play again?", "Time's up!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Hide();
                    startForm fm = new startForm();
                    fm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                    Application.Exit();
                }

                // Εγγραφή σκορ δίπλα από το Username
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(", " + score);
                }                                
            }

            // Εμφάνιση του Countdown
            textBox1.Text = min.ToString() + ":" + sec.ToString() + ":" + mil.ToString();
        }

        // Η εφαρμογή κλείνει τελείως αν πατήσουμε το Χ
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
