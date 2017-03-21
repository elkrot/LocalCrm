using LocalCrm.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using LocalCrm.DataAccess;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;

namespace LocalCrm
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ReportViwer : Window
    {
        IReportCondition ReportCondition;
        public ReportViwer()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }


        public ReportViwer(IReportCondition reportCondition) : base()
        {
            ReportCondition = reportCondition;
            InitializeComponent();
            _isReportViewerLoaded = false;
            _reportViewer.Load += ReportViewer_Load;

        }


        private bool _isReportViewerLoaded = false;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                var dataset = ReportCondition.DataSet;
                var datasource = new ReportDataSource(ReportCondition.DataSetName, dataset);
                this._reportViewer.LocalReport.ReportEmbeddedResource = ReportCondition.ReportName;
                this._reportViewer.LocalReport.DataSources.Clear();
                this._reportViewer.LocalReport.DataSources.Add(datasource);
                this._reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }
    }
}
