using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Data.Entity;
using System.Net;
using System.Runtime.InteropServices;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Reports
{
    public partial class ReportView :Page
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
                        var row = ds.BowlsTable.AddBowlsTableRow(item.Picture, item.BowlSize.Size, item.Bias.BiasSize,
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

        
    }
}
