using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace school_management_system
{
    public partial class Frmclass : Form
    {
        SqlDataReader rdr;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        Connectionstring cs = new Connectionstring();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;
        public Frmclass()
        {
            InitializeComponent();
        }

        private void Frmsection_Load(object sender, EventArgs e)
        {
            FillSections();
            GetData();
        }

        public void FillSections()     
        {
            try
            {

                con = new SqlConnection(cs.DBcon);
                con.Open();
                string ct1 = "SELECT distinct RTRIM(Sectionname) FROM Section ";    
                cmd = new SqlCommand(ct1);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbSection.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBcon);
                con.Open();
                string ct = "select className , section from class where className='" +txtClass.Text+ "' and section='"+cmbSection.Text+"'";

               cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Record Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClass.Text = "";
                    Reset();
                    txtClass.Focus();

                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBcon);
                con.Open();

                string cb = "insert into class(ClassName,Section) VALUES ('" +txtClass.Text + "','" +cmbSection.Text+"')";

                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                 GetData();
                 st1 = lblUser.Text;
                 st2 = "Added the New Class='" + txtClass.Text + "'";
                 cf.LogFunc(st1, System.DateTime.Now, st2);
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnSave.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBcon);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(class.ID), RTRIM(class.ClassName),RTRIM(Section) from Class order by Classname , Section", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    DataGridView1.Rows.Add(rdr[0], rdr[1],rdr[2]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

          private void Reset()
        {
           txtSectionID.Text= "";
           cmbSection.Text = "";
           cmbSection.SelectedIndex =-1;
           txtClass.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }
        private void delete_records()
        {

            try
            {
              
               
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBcon);

                con.Open();
                string cm = "select ClassID from CourseFee where classID=@find";


                cmd = new SqlCommand(cm);

                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.Int, 10, "ClassID"));


                cmd.Parameters["@find"].Value =txtClassID.Text;


                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Unable to delete..Already in use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reset();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBcon);
                con.Open();
                string cq1 = "delete from class  where ID='" +txtClassID.Text+ "'";
                cmd = new SqlCommand(cq1);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    st1 = lblUser.Text;
                    st2 = "Deleted the Class='" + txtClass.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    Reset();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
          

               GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

         
        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete_records();
        }

     
        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                
                    con = new SqlConnection(cs.DBcon);
                    con.Open();
                    string cb2 = "Update class set Classname= '" + txtClass.Text + "', Section= '" + cmbSection.Text+ "' where ID='"+txtClassID.Text+"'";
                    cmd = new SqlCommand(cb2);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();

              

                GetData();  //  1 table update karna h to yha tak ka code hata de
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                st1 = lblUser.Text;
                st2 = "Updated the Class='" + txtClass.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btnUpdate.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
          
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = DataGridView1.SelectedRows[0];
                txtClassID.Text = dr.Cells[0].Value.ToString();
                txtClass.Text = dr.Cells[1].Value.ToString();
                cmbSection.Text = dr.Cells[2].Value.ToString();

                btnUpdate.Enabled = true; //ye button item select karne pe dikhe
                btnSave.Enabled = false;
                btnDelete.Enabled = true;                            //ye hide ho jaye kyu ki grid view se data laye he update k liye save ke lie nhi 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSection_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

       }
        }
        
        
    

