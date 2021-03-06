using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace school_management_system
{
    public partial class frmHostelFeePaymentsReport : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        Connectionstring cs = new Connectionstring();
        public frmHostelFeePaymentsReport()
        {
            InitializeComponent();
        }


        private void frmHostelFeePaymentsReport_Load(object sender, EventArgs e)
        {
            AutocompleteCourse();
            AutocompleteScholarNo();
        }
         
        private void AutocompleteCourse()
        {

            try
            {


                SqlConnection CN = new SqlConnection(cs.DBcon);

                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Class) FROM HostelFeePayment,Student,Hosteler where HostelFeePayment.AdmissionNo=Student.AdmissionNo and Hosteler.AdmissionNo=Student.AdmissionNo", CN);
                ds = new DataSet("ds");

                adp.Fill(ds);
                dtable = ds.Tables[0];
                Course.Items.Clear();

                foreach (DataRow drow in dtable.Rows)
                {
                    Course.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AutocompleteScholarNo()
        {

            try
            {


                SqlConnection CN = new SqlConnection(cs.DBcon);

                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(hostelfeepayment.AdmissionNo) FROM HostelFeePayment,student where hostelfeepayment.admissionNo=student.admissionNo", CN);
                ds = new DataSet("ds");

                adp.Fill(ds);
                dtable = ds.Tables[0];
                AdmissionNo.Items.Clear();

                foreach (DataRow drow in dtable.Rows)
                {
                    AdmissionNo.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (Course.Text == "")
            {
                MessageBox.Show("Please select class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Course.Focus();
                return;
            }
            if (Branch.Text == "")
            {
                MessageBox.Show("Please select Section", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Branch.Focus();
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                RptHostelFeePaymentReport rpt = new RptHostelFeePaymentReport();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                ERPS_DBDataSet myDS = new ERPS_DBDataSet();
                //The DataSet you created.


                myConnection = new SqlConnection(cs.DBcon);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * FROM   Student INNER JOIN Hosteler ON Student.AdmissionNo = Hosteler.AdmissionNo INNER JOIN HostelFeePayment ON Student.AdmissionNo = HostelFeePayment.AdmissionNo and DateOfPayment between @date1 and @date2 and Class= '" + Course.Text + "'and Section='" + Branch.Text + "' order by DateOfPayment";
                MyCommand.Parameters.Add("@date1", SqlDbType.DateTime, 30, "DateOfPayment").Value = Date_from.Value.Date;
                MyCommand.Parameters.Add("@date2", SqlDbType.DateTime, 30, "DateOfPayment").Value = Date_to.Value.Date;

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "HostelFeePayment");
                myDA.Fill(myDS, "Hostelinfo");
                myDA.Fill(myDS, "Hosteler");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);

                crystalReportViewer1.ReportSource = rpt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdmissionNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
               RptHostelFeePaymentReport rpt = new RptHostelFeePaymentReport();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                ERPS_DBDataSet myDS = new ERPS_DBDataSet();
                //The DataSet you created.


                myConnection = new SqlConnection(cs.DBcon);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * FROM   Student INNER JOIN Hosteler ON Student.AdmissionNo = Hosteler.AdmissionNo INNER JOIN HostelFeePayment ON Student.AdmissionNo = HostelFeePayment.AdmissionNo and hostelfeepayment.admissionNo='" + AdmissionNo.Text + "'order by DateOfPayment";
               

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "HostelFeePayment");
                myDA.Fill(myDS, "Hostelinfo");
                myDA.Fill(myDS, "Hosteler");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);

                crystalReportViewer2.ReportSource = rpt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                RptHostelFeePaymentReport rpt = new RptHostelFeePaymentReport();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                ERPS_DBDataSet myDS = new  ERPS_DBDataSet();
                //The DataSet you created.


                myConnection = new SqlConnection(cs.DBcon);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * FROM  Student INNER JOIN Hosteler ON Student.AdmissionNo = Hosteler.AdmissionNo INNER JOIN HostelFeePayment ON Student.AdmissionNo = HostelFeePayment.AdmissionNo and DateOfPayment between @date1 and @date2 order by DateOfPayment";
                MyCommand.Parameters.Add("@date1", SqlDbType.DateTime, 30, "DateOfPayment").Value = PaymentDateFrom.Value.Date;
                MyCommand.Parameters.Add("@date2", SqlDbType.DateTime, 30, "DateOfPayment").Value = PaymentDateTo.Value.Date;

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "HostelFeePayment");
                myDA.Fill(myDS, "Hostelinfo");
                myDA.Fill(myDS, "Hosteler");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);

                crystalReportViewer3.ReportSource = rpt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                RptHostelFeePaymentReport rpt = new RptHostelFeePaymentReport();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                ERPS_DBDataSet myDS = new ERPS_DBDataSet();
                //The DataSet you created.


                myConnection = new SqlConnection(cs.DBcon);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * FROM   Student INNER JOIN Hosteler ON Student.AdmissionNo = Hosteler.AdmissionNo INNER JOIN HostelFeePayment ON Student.AdmissionNo = HostelFeePayment.AdmissionNo and DateOfPayment between @date1 and @date2 and DueFees > 0 order by DateOfPayment";
                MyCommand.Parameters.Add("@date1", SqlDbType.DateTime, 30, "DateOfPayment").Value = DateFrom.Value.Date;
                MyCommand.Parameters.Add("@date2", SqlDbType.DateTime, 30, "DateOfPayment").Value = DateTo.Value.Date;

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "HostelFeePayment");
                myDA.Fill(myDS, "Hostelinfo");
                myDA.Fill(myDS, "Hosteler");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);

                crystalReportViewer4.ReportSource = rpt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                RptHostelFeePaymentReport rpt = new RptHostelFeePaymentReport();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                ERPS_DBDataSet myDS = new ERPS_DBDataSet();
                //The DataSet you created.


                myConnection = new SqlConnection(cs.DBcon);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * FROM   Student INNER JOIN Hosteler ON Student.AdmissionNo = Hosteler.AdmissionNo INNER JOIN HostelFeePayment ON Student.AdmissionNo = HostelFeePayment.AdmissionNo and DateOfPayment between @date1 and @date2 and fine > 0 order by DateOfPayment";
                MyCommand.Parameters.Add("@date1", SqlDbType.DateTime, 30, "DateOfPayment").Value = dateTimePicker1.Value.Date;
                MyCommand.Parameters.Add("@date2", SqlDbType.DateTime, 30, "DateOfPayment").Value = dateTimePicker2.Value.Date;

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "HostelFeePayment");
                myDA.Fill(myDS, "Hostelinfo");
                myDA.Fill(myDS, "Hosteler");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);

                crystalReportViewer5.ReportSource = rpt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            Branch.Items.Clear();
            Branch.Text = "";
            Branch.Enabled = true;

            try
            {

                con = new SqlConnection(cs.DBcon);
                con.Open();


                string ct = "select distinct RTRIM(Section) from HostelFeePayment,Hosteler,Student where HostelFeePayment.AdmissionNo=Student.AdmissionNo and Hosteler.AdmissionNo=Student.AdmissionNo and Student.class= '" + Course.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Branch.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Course.Text = "";
            Branch.Text = "";
            Date_from.Text = System.DateTime.Today.ToString();
            Date_to.Text = System.DateTime.Today.ToString();
            crystalReportViewer1.ReportSource = null;
            AdmissionNo.Text = "";
            crystalReportViewer2.ReportSource = null;
            PaymentDateFrom.Text = System.DateTime.Today.ToString();
            PaymentDateTo.Text = System.DateTime.Today.ToString();
            crystalReportViewer3.ReportSource = null;
            DateFrom.Text = System.DateTime.Today.ToString();
            DateTo.Text = System.DateTime.Today.ToString();
            crystalReportViewer4.ReportSource = null;
            dateTimePicker1.Text = System.DateTime.Today.ToString();
            dateTimePicker2.Text = System.DateTime.Today.ToString();
            crystalReportViewer5.ReportSource = null;
            Branch.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Course.Text = "";
            Branch.Text = "";
            Date_from.Text = System.DateTime.Today.ToString();
            Date_to.Text = System.DateTime.Today.ToString();
            crystalReportViewer1.ReportSource = null;
            Branch.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AdmissionNo.Text = "";
            crystalReportViewer2.ReportSource = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PaymentDateFrom.Text = System.DateTime.Today.ToString();
            PaymentDateTo.Text = System.DateTime.Today.ToString();
            crystalReportViewer3.ReportSource = null;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DateFrom.Text = System.DateTime.Today.ToString();
            DateTo.Text = System.DateTime.Today.ToString();
            crystalReportViewer4.ReportSource = null;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Text = System.DateTime.Today.ToString();
            dateTimePicker2.Text = System.DateTime.Today.ToString();
            crystalReportViewer5.ReportSource = null;
        }

        private void frmHostelFeePaymentsReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frm_Main_Menu frm = new frm_Main_Menu();
            frm.UserType.Text = lblUserType.Text;
            frm.User.Text = lblUser.Text;
            frm.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
