     <add name="LocalCrmContext"
         connectionString="Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=localCrmDb.mdf;Integrated Security=True"
         providerName="System.Data.SqlClient"/>
    <add name="LocalCrm.Properties.Settings.LocalCrmConnectionStringReport"
         connectionString="Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=localCrmDb.mdf;Integrated Security=True"
         providerName="System.Data.SqlClient"/>
 
 
 <!-- <add name="LocalCrmContext" connectionString="Data Source=(local)\SQLEXPRESS;
        Initial Catalog=localCrmDb;Integrated Security=True;MultipleActiveResultSets=True" providerName="System.Data.SqlClient"/>
          <add name="LocalCrm.Properties.Settings.LocalCrmConnectionStringReport" connectionString="Data Source=(local)\SQLEXPRESS;
        Initial Catalog=localCrmDb;Integrated Security=True;MultipleActiveResultSets=True" providerName="System.Data.SqlClient"/>
        

      <add name="LocalCrmContext" 
           connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\temp\localCrmDb.mdf';Integrated Security=True"
           providerName="System.Data.SqlClient"/>
      <add name="LocalCrmConnectionStringReport" 
           connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\temp\localCrmDb.mdf';Integrated Security=True"
           providerName="System.Data.SqlClient"/>

      <add name="LocalCrmContext" 
           connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\localCrmDb.mdf';Integrated Security=True"
           providerName="System.Data.SqlClient"/>
      <add name="LocalCrm.Properties.Settings.LocalCrmConnectionStringReport" 
           connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|localCrmDb.mdf';Integrated Security=True"
           providerName="System.Data.SqlClient"/>

-->

var pathToMdfFileDirectory = // get the path
AppDomain.CurrentDomain.SetData("DataDirectory", pathToMdfFileDirectory);





/* reportDataSource.Name = "ReportDataSet"; //Name of the report dataset in our .RDLC file
                reportDataSource.Value =ds;
                

                this._reportViewer.LocalReport.DataSources.Add(reportDataSource);
                this._reportViewer.LocalReport.ReportEmbeddedResource = 
                    @"LocalCrm.Reports.ReportByDate.rdlc";

                dataset.EndInit();

                    //fill data into adventureWorksDataSet
                       Reports.ReportDataSet.ReportQueryDataTable
                           salesOrderDetailTableAdapter = new Reports.ReportQueryDataTable..SalesOrderHeaderTableAdapter();
                       salesOrderDetailTableAdapter.ClearBeforeFill = true;
                       salesOrderDetailTableAdapter.Fill(dataset.SalesOrderHeader, ReportCondition.StartDate, ReportCondition.EndDate);

                       _reportViewer.RefreshReport();*/





					     var rv = new ReportViwer(rc);
            rc.DataSetName = "DataSetTest";
            rc.ReportName = @"LocalCrm.Reports.TestReport.rdlc";
            var obj = new ObjectsSource();
            rc.DataSet = obj.GetProducts();
            rv.ShowDialog();