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
    public partial class LineDetails : Form
    {
        private ShpDemo shpDemo;

        public LineDetails(ShpDemo shpDemo)
        { 
            InitializeComponent();
            this.shpDemo = shpDemo;
        }
        public void setDateSource(List<LineModel> listOfLine){ 
            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.DataSource = listOfLine;
            this.dataGridView1.Columns[0].Width = 50;
            this.dataGridView1.Columns[1].Width = 50;
            this.dataGridView1.Columns[2].Width = 150;
            //this.dataGridView1.Columns[3].Width = 150;
            this.dataGridView1.Refresh();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)//防止窗口建立时选择调用该函数
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                chooseItemfromDataGridViewInLine(rowIndex);
                //dataGridView1.SelectedRows[0].DefaultCellStyle.BackColor = Color.Red;
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedCells.Count > 0)//防止窗口建立时选择调用该函数
            //{
            //    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            //    chooseItemfromDataGridViewInLine(rowIndex);
            //    //dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
            //    dataGridView1.Refresh();
            //}
            dataGridView1_SelectionChanged(sender, e);
        }

        private void chooseItemfromDataGridViewInLine(int rowIndex)
        {
             this.shpDemo.fitWindowFromLine(rowIndex);
        }

    }
}
