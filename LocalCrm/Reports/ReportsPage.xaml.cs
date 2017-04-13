﻿using LocalCrm.DataAccess;
using LocalCrm.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalCrm
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        IReportCondition rc;
        public ReportsPage()
        {
            rc = new ReportCondition();
            DataContext = rc;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ShowReport(@"LocalCrm.Reports.ReportByDate.rdlc", "Orders");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowReport(@"LocalCrm.Reports.ReportByTransportCompany.rdlc", "Orders");
        }

        private void ShowReport(string reportName, string dataStName)
        {
            rc.ReportName = reportName;
            rc.DataSetName = dataStName;

            ReportConditionForm rcf = new ReportConditionForm();
            rcf.ShowDialog();

            if (rcf.DialogResult == true)
            {

                var stId = "";
                foreach (var item in rcf.lstStatus.Where(x => x.IsChecked))
                {
                    stId += "," + item.Id.ToString();
                }
                System.Windows.Forms.MessageBox.Show(stId);



                using (var dataService = new EFDataService())
                {
                    rc.DataSet = dataService.GetSalesOrdersByPeriod(rc.StartDate, rc.EndDate);
                }
                var rv = new ReportViwer(rc);
                rv.ShowDialog();
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShowReport(@"LocalCrm.Reports.ReportByStatus.rdlc", "Orders");
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ShowReport(@"LocalCrm.Reports.Report1.rdlc", "Orders");
        }
    }
}
