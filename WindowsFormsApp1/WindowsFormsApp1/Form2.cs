using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {

        public string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=phone;";
        public string querysub1, querysub2, querymulti1, querymulti2;
        public static bool nonexist;
        public static int idnumber;
        public static string[] ischecked = new string[30];

        public Form2()
        {
            InitializeComponent();
           

            showsubpanel();
            showmulti();

        }

    
        

        private void searchDB(string query)
        {
           
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 

                // If there are available rows
                if (reader.HasRows)
                {
                    MessageBox.Show("중복값발견!!");
                    nonexist = false;
                }
                else
                {
                    MessageBox.Show("중복값없음");
                    nonexist = true;
                    
                  
                }

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } // 칼럼 중복 값 확인

   
        private void saveDB(string query)
        {


            
     

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                // MessageBox.Show("User succesfully registered");
                databaseConnection.Close();
                

            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        } // db 저장 함수

        


        private void savecolum()
        {
            for (int i = 0; i< Form3.clicksub; i++)
            {
                searchDB("SELECT 1 FROM Information_schema.columns WHERE table_schema = 'phone' AND table_name = 'sub' AND column_name = '" + Form3.sublabel[i].Text + "';");
                if (nonexist)
                {
                    saveDB("alter table sub add " + Form3.sublabel[i].Text + " varchar(255);");
                }
            }

            for (int i = 0; i < Form6.clickmulti; i++)
            {
                searchDB("SELECT 1 FROM Information_schema.columns WHERE table_schema = 'phone' AND table_name = 'multi' AND column_name = '" + Form6.multiqna[i].Text + "';");
                if (nonexist)
                {
                    saveDB("alter table multi add " + Form6.multiqna[i].Text + " varchar(255) after id;");
                }
            }
        }// 항목값들을 칼럼으로 넣어주기 위해서 DB에 넣으려는 칼럼이 이미 존재하는지(중복되는지) 확인한 후 값을 넣어준다.

        private void savedb()
        {
            for (int i = 0; i < Form3.clicksub; i++)
            {
                querysub1 += ", `" + Form3.sublabel[i].Text + "`";
                querysub2 += ", '" + Form3.subjectbox[i].Text + "'"; 
            }

            for (int i = 0; i < Form6.clickmulti; i++)
            {
                querymulti1 += ", `" + Form6.multiqna[i].Text + "`";
                querymulti2 += ", '" + ischecked[i] + "'";
            }

            saveDB("INSERT INTO sub(`id`"+ querysub1 +") VALUES ('"+ idnumber +"'"+querysub2+")");
            saveDB("INSERT INTO multi(`id`"+ querymulti1 + ") VALUES ('" + idnumber + "'"+ querymulti2+ ")");
        } // 주관식 값과 객관식 라디오버튼 선택값을 DB에 넣어주기 위해서 쿼리문을 for문을 이용해서 작성한다.

       

        private void radiochecked()
        {
            int i = 0 ,a = 0;
            while (i < Form6.buttoncount)
            {
                for (int j = 0; j < int.Parse(Form6.panel1[a].Name); j++) {
                    if (Form6.multiselect[i].Checked == true) { ischecked[a] = Form6.multiselect[i].Text; }
                    i++;
                }
                a++;
            }
        } // 객관식 라디오버튼 체크 유무 판단, ischecked라는 string 배열에 넣어줌



        private void showsubpanel()
        {
            for (int i=0; i<Form3.clicksub; i++)
            {
                flowLayoutPanel1.Controls.Add(Form3.subpanel[i]);
            }
        } //주관식 판넬 배치

        private void showmulti()
        {
            for (int i=0; i<Form6.clickmulti; i++)
            {
                flowLayoutPanel1.Controls.Add(Form6.multiqna[i]);
            }
        } //객관식 그룹박스 배치
      

    

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form7 frm7 = new Form7();      // 세번째 창을 여는 방법, 현재창은 컨트롤 불가 
            frm7.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Form3.clicksub == 0 && Form6.clickmulti == 0)
            {
                MessageBox.Show("질문을입력해주세요!!"); //아무런 질문이 없으면 다시 입력유도
            }
            else
            {
                idnumber++;
                radiochecked();
                savecolum();
                savedb();
                this.Visible = false;             // 현재창을 종료하고 네번째 창을 여는 방법
                Form4 showForm4 = new Form4();
                showForm4.ShowDialog();
            }
            
        }

       
    }
}


