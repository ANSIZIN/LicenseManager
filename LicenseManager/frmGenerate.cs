using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LicenseManager
{
    public partial class frmGenerate : Form
    {
        public frmGenerate()
        {
            InitializeComponent();
        }
        const int ProductCode = 1;
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtProductID.Text);
            KeyValuesClass kv;
            string productKey = string.Empty;
            if(cmbLicenseType.SelectedIndex == 0)
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.FULL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode =(byte)ProductCode,
                    Edition = Edition.ENTERPRISE,
                    Version = 1
                };
                if(!km.GenerateKey(kv, ref productKey))
                    txtProductKeys.Text = "ERROR";

            }
            else
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.TRIAL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode = (byte)ProductCode,
                    Edition = Edition.ENTERPRISE,
                    Version = 1,
                    Expiration = DateTime.Now.Date.AddDays(Convert.ToInt32(txtExperienceDays.Text))
                };
                if (!km.GenerateKey(kv, ref productKey))
                    txtProductKeys.Text = "ERROR";

            }
            txtProductKeys.Text = productKey;
        }

        private void frmGenerate_Load(object sender, EventArgs e)
        {
            cmbLicenseType.SelectedIndex = 0;
            txtProductID.Text = ComputerInfo.GetComputerId();
        }
    }
}
