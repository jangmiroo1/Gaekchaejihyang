using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public static GroupBox[] multiqna = new GroupBox[30];
        public static Panel[] panel1 = new Panel[30];
        public static RadioButton[] multiselect = new RadioButton[100];
        public static int clickmulti=0, buttoncount=0;
        int a = 0, norun;
        



        public Form6()
        {
            InitializeComponent();
        }

        private void savemulti()
        {
            clickmulti -= 1;

            multiqna[clickmulti] = new GroupBox();
            multiqna[clickmulti].Size = new Size(350, 200);
            multiqna[clickmulti].Text = textBox2.Text;

            panel1[clickmulti] = new Panel();
            panel1[clickmulti].Size = new Size(330, 180);
            panel1[clickmulti].Location = new Point(10, 10);
            panel1[clickmulti].AutoScroll = true;

        

            for (int i=buttoncount; i< buttoncount+listBox1.Items.Count; i++)
            {
                
                multiselect[i] = new RadioButton();
                multiselect[i].AutoSize = true;
                multiselect[i].Text = listBox1.Items[a].ToString();
                multiselect[i].Location = new Point(10, 30 * (a+1));
                panel1[clickmulti].Controls.Add(multiselect[i]);
                panel1[clickmulti].Name = listBox1.Items.Count.ToString();
                a++;
            }

            multiqna[clickmulti].Controls.Add(panel1[clickmulti]);

            buttoncount += listBox1.Items.Count;
            a = 0;
            clickmulti += 1;
        } //생성버튼이 클릭되면 clickmulti 카운트가 하나 증가하면서 리스트박스 항목이름들로 구성된
        // 라디오버튼과 라디오버튼을 담을 판넬, 판넬을 담을 그룹박스가 배열로 clickmulti를 인덱스로
        // 사용하여 만들어진다.

        private void listchecked()
        {
            string check = textBox2.Text;

                for (int i = 0; i < clickmulti-1; i++)
                {
                    if (check == multiqna[i].Text)
                    {
                        norun = 1;
                    }
                }
            
        } //중복 방지하는 함수, 중복된 값을 입력하면 norun에 1을 대입한다.


        private void button2_Click(object sender, EventArgs e)
        {           
            clickmulti++;
            
            if (clickmulti==1)
            {
                savemulti();
                MessageBox.Show("생성되었습니다!");
            }

            if (clickmulti > 1)
            {
                
                listchecked();
                if (norun == 1)
                {
                    MessageBox.Show("항목 중복입니다! 삭제후 다시 작성하세요!");
                    norun = 0;
                    clickmulti -= 1;
                }
                else
                {
                    savemulti();
                    MessageBox.Show("생성되었습니다!");
                }
            }
        } // 중복체크

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(textBox3.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                listBox1.Items.Add(textBox1.Text);
            
        }

     
    }
}
