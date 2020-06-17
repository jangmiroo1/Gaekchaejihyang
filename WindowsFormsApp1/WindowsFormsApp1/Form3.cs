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
    public partial class Form3 : Form
    {


        public static TextBox[] subjectbox = new TextBox[30];
        public static Label[] sublabel = new Label[30];
        public static Panel[] subpanel = new Panel[30];
        public static int clicksub;
        int norun;
   
        

        public Form3()
        {
            InitializeComponent();
        }

        private void SaveinPanel()
        {
            clicksub -= 1;

            subjectbox[clicksub] = new TextBox();
            subjectbox[clicksub].Size = new Size(100, 20);
            subjectbox[clicksub].Location = new Point(80, 10);
            subjectbox[clicksub].Multiline = true;
            subjectbox[clicksub].Text = "입력하세요";

            sublabel[clicksub] = new Label();
            sublabel[clicksub].AutoSize = true;
            //sublabel[clicksub].Size = new Size(30, 20);
            sublabel[clicksub].Location = new Point(10, 10);
            sublabel[clicksub].Text = textBox2.Text;

            subpanel[clicksub] = new Panel();
            subpanel[clicksub].Size = new Size(180, 90);

            subpanel[clicksub].Controls.Add(sublabel[clicksub]);
            subpanel[clicksub].Controls.Add(subjectbox[clicksub]);

            clicksub += 1;

        } // 생성하기 버튼을 누를때마다 주관식내용을 입력받을 수 있는 텍스트 박스와
        // 항목 이름이 담긴 라벨을 만들어 그 두개를 판넬에 넣어준다. 텍스트박스, 판넬, 라벨은 모두
        // 배열로 구성되어 있다.

        private void subchecked()
        {
            string check = textBox2.Text;

            for (int i = 0; i < clicksub - 1; i++)
            {
                if (check == sublabel[i].Text)
                {
                    norun = 1;
                }
            }

        }//중복 방지하는 함수, 중복된 값을 입력하면 norun에 1을 대입한다.



        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clicksub++;

            if (clicksub == 1)
            {
                SaveinPanel();
                MessageBox.Show("생성되었습니다!");
            }

            if (clicksub > 1)
            {

                subchecked();
                if (norun == 1)
                {
                    MessageBox.Show("항목 중복입니다! 삭제후 다시 작성하세요!");
                    norun = 0;
                    clicksub -= 1;

                }
                else
                {
                    SaveinPanel();
                    MessageBox.Show("생성되었습니다!");
                }
            }



        } //중복체크
    }
}
