using Inventory.DAL;
using Microsoft.Reporting.WebForms;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.Reports
{
    public partial class BowlsInventory :Page
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

                // Validate report source.
                //var rptPath = Server.MapPath(@"./Reports/BowlsInventory.rdlc");

                //if(!File.Exists(rptPath))
                //    return;

                // Set report path.
                //this.rv1.LocalReport.ReportPath = rptPath;

                rv1.LocalReport.DataSources.Clear();

                var ds = new InventoryDS();
                using (BowlContext db = new BowlContext())
                {
                    foreach (var item in db.Bowls)
                    {
                        ds.BowlsTable.AddBowlsTableRow(item.Picture, item.BowlSize.Size, item.Bias.BiasSize,
                            item.Weight.BowlWeight, item.InLocker, item.OwnerName??"", item.Comment);
                    }
                }

                rv1.LocalReport.DataSources.Add(new ReportDataSource("Bowls", ds.BowlsTable.Rows));

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
