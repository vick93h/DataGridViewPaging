using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewPaging
{
    public partial class Form1 : Form
    {
        int pageNumber = 0;
        int pageSize = 100;
        public Form1()
        {
            InitializeComponent();
            btnPrevious.Enabled = false;
            btnLatest.Enabled = false;
            this.storeTableAdapter.Fill(this.adventureWorks2019DataSet.Store);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var query = from c in adventureWorks2019DataSet.Store
                        select new
                        {
                            BusinessEntityID = c.BusinessEntityID,
                            Name = c.Name,
                            SalesPersonID = c.SalesPersonID,
                            Demographics = c.Demographics,
                            rowguid = c.rowguid,
                            ModifiedDate = c.ModifiedDate

                        };

            storeBindingSource.DataSource = query.Skip(pageSize * pageNumber).Take(pageSize).ToList();
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            Display.Text = String.Format("Page {0}/{1}", pageNumber, query.Count() / pageSize);


        }

        private void btnLatest_Click(object sender, EventArgs e)
        {

            var query = from c in adventureWorks2019DataSet.Store
                        select new
                        {
                            BusinessEntityID = c.BusinessEntityID,
                            Name = c.Name,
                            SalesPersonID = c.SalesPersonID,
                            Demographics = c.Demographics,
                            rowguid = c.rowguid,
                            ModifiedDate = c.ModifiedDate

                        };
            pageNumber = 0;
            storeBindingSource.DataSource = query.Skip(pageSize * pageNumber).Take(pageSize).ToList();
            btnNext.Enabled = true;
            btnPrevious.Enabled = false;
            btnLatest.Enabled = !(pageNumber == 0);
            btnLast.Enabled = true;
            Display.Text = String.Format("Page {0}/{1}", pageNumber, query.Count() / pageSize);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

            var query = from c in adventureWorks2019DataSet.Store
                        select new
                        {
                            BusinessEntityID = c.BusinessEntityID,
                            Name = c.Name,
                            SalesPersonID = c.SalesPersonID,
                            Demographics = c.Demographics,
                            rowguid = c.rowguid,
                            ModifiedDate = c.ModifiedDate

                        };
            pageNumber--;
            storeBindingSource.DataSource = query.Skip(pageSize * pageNumber).Take(pageSize).ToList();

            if (query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Count() > 0)
                btnPrevious.Enabled = true;
            else
                btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            if (pageNumber == 0)
            {
                btnPrevious.Enabled = false;
                btnLatest.Enabled = false;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnLatest.Enabled = true;
            }
            Display.Text = String.Format("Page {0}/{1}", pageNumber, query.Count() / pageSize);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            var query = from c in adventureWorks2019DataSet.Store
                        select new
                        {
                            BusinessEntityID = c.BusinessEntityID,
                            Name = c.Name,
                            SalesPersonID = c.SalesPersonID,
                            Demographics = c.Demographics,
                            rowguid = c.rowguid,
                            ModifiedDate = c.ModifiedDate

                        };
            pageNumber++;
            storeBindingSource.DataSource = query.Skip(pageSize * pageNumber).Take(pageSize).ToList();

            if (query.Skip(pageSize * (pageNumber + 1)).Take(pageSize).Count() > 0)
                btnNext.Enabled = true;
            else
                btnNext.Enabled = false;
            btnPrevious.Enabled = true;
            btnLatest.Enabled = true;
            if (pageNumber == query.Count() / pageSize)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }

            Display.Text = String.Format("Page {0}/{1}", pageNumber, query.Count() / pageSize);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {

            var query = from c in adventureWorks2019DataSet.Store
                        select new
                        {
                            BusinessEntityID = c.BusinessEntityID,
                            Name = c.Name,
                            SalesPersonID = c.SalesPersonID,
                            Demographics = c.Demographics,
                            rowguid = c.rowguid,
                            ModifiedDate = c.ModifiedDate

                        };
            pageNumber = query.Count() / (pageSize);
            storeBindingSource.DataSource = query.Skip(pageSize * pageNumber).Take(pageSize).ToList();
            btnNext.Enabled = false;
            btnPrevious.Enabled = true;
            btnLatest.Enabled = true;
            btnLast.Enabled = !(pageNumber == query.Count() / pageSize);

            Display.Text = String.Format("Page {0}/{1}", pageNumber, query.Count() / pageSize);
        }
    }
}
