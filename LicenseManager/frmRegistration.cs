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
    public partial class frmRegistration : Form
    {
        public frmRegistration()
        {
            InitializeComponent();
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            txtProductID.Text = ComputerInfo.GetComputerId();
        }
        const int ProductCode = 1;
        private void btnOk_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtProductID.Text);
            string productKey = txtProductKeys.Text;
            if(km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if(km.DisassembleKey(productKey,ref kv))
                {
                    LicenseInfo licenseInfo = new LicenseInfo();
                    licenseInfo.ProductKey = productKey;
                    licenseInfo.FullName = "PetroDATA Ticaret";
                    if(kv.Type == LicenseType.TRIAL)
                    {
                        licenseInfo.Day = kv.Expiration.Day;
                        licenseInfo.Month = kv.Expiration.Month;
                        licenseInfo.Year = kv.Expiration.Year;
                    }
                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), licenseInfo);
                    MessageBox.Show("You have been successfully registered.",
                                    "Message",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Your product key is invalid..",
                                    "Message",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Your product key is invalid..",
                                "Message",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
    }
}
