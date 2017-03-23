﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace LocalCrm.Infrastructure
{
    public static class FileApiUtilites
    {
        public static List<string[]> GetDataFromXlsx(string filepath)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            var result = new List<string[]>();
            string str;
            int rCnt;
            int cCnt;
            int rw = 0;
            int cl = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(filepath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;
            var isEmptyRow = true;

            for (rCnt = 1; rCnt <= rw; rCnt++)
            {
                isEmptyRow = true;
                var row = new string[cl];
                for (cCnt = 1; cCnt <= cl; cCnt++)
                {
                    if (range.Cells[rCnt, cCnt] != null && range.Cells[rCnt, cCnt].Value2 != null)
                    {
                        str = string.Format("{0}", (range.Cells[rCnt, cCnt] as Excel.Range).Value2);
                        if (str != null) { 
                        row[cCnt - 1] = str;
                            isEmptyRow = false;
                        }
                    }
                }
                if(!isEmptyRow) result.Add(row);
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            return result;
        }
    }
}
