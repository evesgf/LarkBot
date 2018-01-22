//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Dynamic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using Excel = Microsoft.Office.Interop.Excel;

//namespace FileHandler
//{
//    public class ExcelHelper
//    {
//        private Excel.Application _myExcel;
//        private Excel.Workbook _myWorkBook;

//        /// <summary>
//        /// 构造函数，不创建Excel工作薄
//        /// </summary>
//        public ExcelHelper()
//        {
//            _myExcel = new Excel.Application();
//        }

//        /// <summary>
//        /// 创建Excel工作薄
//        /// </summary>
//        public void CreateExcel(string path)
//        {
//            if (File.Exists(path))
//            {
//                return;
//            }
//            else
//            {
//                Excel.Application excelApp; //Excel应用程序  
//                Excel.Workbook excelDoc; //Excel文档  

//                excelApp = new Excel.ApplicationClass();

//                Object nothing = Missing.Value;
//                excelDoc = excelApp.Workbooks.Add(nothing);
//                Object format = Excel.XlFileFormat.xlWorkbookDefault;
//                excelDoc.SaveAs(path, nothing, nothing, nothing, nothing, nothing,
//                    Excel.XlSaveAsAccessMode.xlExclusive, nothing, nothing, nothing, nothing, nothing);
//                excelDoc.Close(nothing, nothing, nothing);
//                excelApp.Quit();
//            }
//        }

//        /// <summary>
//        /// 打开excel
//        /// </summary>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        public Excel._Workbook OpenWorkbook(string path)
//        {
//            _myWorkBook = _myExcel.Application.Workbooks.Add(path);
//            return _myWorkBook;
//        }

//        /// <summary>
//        /// 查询是否存在sheet,无则返回空值,有则返回
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public Excel._Worksheet SelectWorksheet(string name)
//        {
//            var sheets = _myWorkBook.Sheets;
//            return sheets.Cast<Excel.Worksheet>().FirstOrDefault(temp => temp.Name.Equals(name));
//        }

//        /// <summary>
//        /// 插入一个sheet
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public Excel._Worksheet InsertWorksheet(string name)
//        {
//            Excel.Worksheet newWorksheet =
//                (Excel.Worksheet) _myWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
//            newWorksheet.Name = name;
//            return newWorksheet;
//        }

//        /// <summary>
//        /// 保存并关闭
//        /// </summary>
//        /// <param name="path"></param>
//        public void SaveCloseWorkBook(string path)
//        {
//            //保存文件
//            //屏蔽掉系统跳出的Alert
//            _myExcel.AlertBeforeOverwriting = false;
//            _myExcel.DisplayAlerts = false;

//            _myWorkBook.SaveAs(path, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
//                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value,
//                Missing.Value, Missing.Value, Missing.Value);
//            _myExcel.Quit();
//            _myExcel = null;
//        }

//        /// <summary>
//        /// 将数据写入Excel
//        /// </summary>
//        /// <param name="data">要写入的字符串</param>
//        /// <param name="starRow">写入的行</param>
//        /// <param name="startColumn">写入的列</param>
//        public void WriteData(string data, int row, int column)
//        {
//            _myExcel.Cells[row, column] = data;
//        }

//        /// <summary>
//        /// 获取sheet最后一行行号
//        /// </summary>
//        /// <param name="sheet"></param>
//        /// <returns></returns>
//        public int GetLastRow(Excel._Worksheet sheet)
//        {
//            var range = (Excel.Range) sheet.Rows[11, Missing.Value];
//            return 0;
//        }

//        /// <summary>
//        /// 将数据写入Excel
//        /// </summary>
//        /// <param name="data">要写入的数据表</param>
//        /// <param name="startRow">Excel中的起始行</param>
//        /// <param name="startColumn">Excel中的起始列</param>
//        public void WriteData(System.Data.DataTable data, int startRow, int startColumn)
//        {
//            for (int i = 0; i <= data.Rows.Count - 1; i++)
//            {
//                for (int j = 0; j <= data.Columns.Count - 1; j++)
//                {
//                    //在Excel中，如果某单元格以单引号“'”开头，表示该单元格为纯文本，因此，我们在每个单元格前面加单引号。 
//                    _myExcel.Cells[startRow + i, startColumn + j] = "'" + data.Rows[i][j].ToString();
//                }
//            }
//        }

//        /// 提供保存数据的DataTable
//        /// CSV的文件路径
//        public void SaveCSV(DataTable dt, string fileName)
//        {
//            FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
//            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
//            string data = "";
//            //写出列名称
//            for (int i = 0; i < dt.Columns.Count; i++)
//            {
//                data += dt.Columns[i].ColumnName.ToString();
//                if (i < dt.Columns.Count - 1)
//                {
//                    data += ",";
//                }
//            }
//            sw.WriteLine(data);
//            //写出各行数据
//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                data = "";
//                for (int j = 0; j < dt.Columns.Count; j++)
//                {
//                    data += dt.Rows[i][j].ToString();
//                    if (j < dt.Columns.Count - 1)
//                    {
//                        data += ",";
//                    }
//                }
//                sw.WriteLine(data);
//            }
//            sw.Close();
//            fs.Close();
//        }
//    }
//}
