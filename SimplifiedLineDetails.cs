using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PolygonDemo.Model;

namespace PolygonDemo
{
    public partial class SimplifiedLineDetails : Form
    {
        //public SimplifiedLineDetails()
        //{
        //    InitializeComponent();
        //}

        private ShpDemo shpDemo;

        public SimplifiedLineDetails(ShpDemo shpDemo)
        { 
            InitializeComponent();
            this.shpDemo = shpDemo;
        }
        public void setDateSource(List<LineModel> listOfLine){ 
            this.dataGridView2.AutoGenerateColumns = true;
            this.dataGridView2.DataSource = listOfLine;
            this.dataGridView2.Columns[0].Width = 50;
            this.dataGridView2.Columns[1].Width = 150;
            //this.dataGridView1.Columns[2].Width = 150;
            //this.dataGridView1.Columns[3].Width = 150;
            this.dataGridView2.Refresh();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)//防止窗口建立时选择调用该函数
            {
                int rowIndex = dataGridView2.SelectedRows[0].Index;
                chooseItemfromDataGridViewInLine(rowIndex);
                //dataGridView1.SelectedRows[0].DefaultCellStyle.BackColor = Color.Red;
                dataGridView2.Refresh();
            }
        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)//防止窗口建立时选择调用该函数
            {
                int rowIndex = dataGridView2.SelectedCells[0].RowIndex;
                chooseItemfromDataGridViewInLine(rowIndex);
                //dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
                dataGridView2.Refresh();
            }
        }

        private void chooseItemfromDataGridViewInLine(int rowIndex)
        {
             this.shpDemo.fitWindowFromSimplifiedLine(rowIndex);
        }



    }
}
