using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialAlerts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLookUp_Click(object sender, EventArgs e)
        {
            if (txtTickerSymbol.Text != "")
            {
                txtResults.Text = "";
                //place interval into static store class
                switch (cmbInterval.Text)
                {
                    case "Daily":
                        StaticStore.interval = "d";
                        break;
                    case "Weekly":
                        StaticStore.interval = "w";
                        break;
                    case "Monthly":
                        StaticStore.interval = "m";
                        break;
                    default:
                        StaticStore.interval = "d";
                        break;
                }
                LookUp lu = new LookUp(txtTickerSymbol.Text, dtpStart.Text, dtpEnd.Text);
                txtResults.Text = StaticStore.lookUpResults;
            }
        }
    }
}
