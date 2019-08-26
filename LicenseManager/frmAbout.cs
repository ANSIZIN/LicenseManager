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
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }
        const int ProductCode = 1;

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            txtProductID.Text = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(txtProductID.Text);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath),ref lic);
            string productKey = lic.ProductKey;
            if(km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if(km.DisassembleKey(productKey,ref kv))
                {
                    txtProductName.Text = "PetroDATA Ticaret";
                    txtProductKeys.Text = productKey;
                    if(kv.Type == LicenseType.TRIAL)
                    {
                        txtLicenseType.Text = string.Format("{0} days", (kv.Expiration - DateTime.Now.Date).Days);
                    }
                    else
                    {
                        txtLicenseType.Text = "FULL";
                    }
                }
            }
        }
    }
}
