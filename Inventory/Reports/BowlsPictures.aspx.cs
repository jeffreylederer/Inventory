using Inventory.DAL;
using Microsoft.Reporting.WebForms;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Inventory.Reports
{
    public partial class BowlsPictures : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rv1.Height = Unit.Parse("100%");
                rv1.Width = Unit.Parse("100%");
                rv1.CssClass = "table";

                // Set report mode for local processing.
                rv1.ProcessingMode = ProcessingMode.Local;

                rv1.LocalReport.DataSources.Clear();

                var ds = new InventoryDS();
                using (BowlContext db = new BowlContext())
                {
                    var list = db.Bowls.ToList();
                    for(int i=0;i<list.Count;i=i+2)
                    {
                        if(i==list.Count-1)
                            ds.BowlsPicture.AddBowlsPictureRow(list[i].Picture, list[i].Picture);
                        else
                            ds.BowlsPicture.AddBowlsPictureRow(list[i].Picture, list[i+1].Picture);
                    }
                }

                rv1.LocalReport.DataSources.Add(new ReportDataSource("PictureRows", ds.BowlsPicture.Rows));

                //parameters

                rv1.ShowToolBar = true;


                // Refresh the ReportViewer.
                rv1.LocalReport.Refresh();
            }
        }

        protected void Rv1_ReportError(object sender, ReportErrorEventArgs e)
        {
            lblError.Text = e.Exception.Message;
            e.Handled = true;
        }
    }
}