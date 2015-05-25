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
    public partial class PointDetails : Form
    {
        private ShpDemo shpDemo;

        public PointDetails(ShpDemo shpDemo)
        { 
            InitializeComponent();
            this.shpDemo = shpDemo;
        }
        public void setDateSource(List<PointModel> listOfPoints){ 
            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.DataSource = listOfPoints;
            this.dataGridView1.Columns[0].Width = 50;
            this.dataGridView1.Columns[1].Width = 150;
            this.dataGridView1.Columns[2].Width = 150;
            //this.dataGridView1.Columns[3].Width = 150;
            this.dataGridView1.Refresh();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)//防止窗口建立时选择调用该函数
            {
                double x = Double.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                double y = Double.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                chooseItemfromDataGridView(x, y);
                //dataGridView1.SelectedRows[0].DefaultCellStyle.BackColor = Color.Red;
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)//防止窗口建立时选择调用该函数
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                double x = Double.Parse(dataGridView1.Rows[rowIndex].Cells[1].Value.ToString());
                double y = Double.Parse(dataGridView1.Rows[rowIndex].Cells[2].Value.ToString());
                chooseItemfromDataGridView(x, y);
                //dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
                dataGridView1.Refresh();
            }
        }

        private void chooseItemfromDataGridView(double x, double y){
             this.shpDemo.fitWindowFromPoint(x, y);
        }
    }
}
