using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiceGame
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        // Στέλνω τον χρήστη πίσω... ενώ σχεδίαζα να φτιάξω και τρίτο επίπεδο, προσωπικοί λόγοι μπήκαν εμπόδιο
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            startForm fm = new startForm();
            fm.Show();
        }
        
        // Η εφαρμογή κλείνει τελείως αν πατήσουμε το Χ
        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
