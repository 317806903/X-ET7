using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using MongoDB.Bson.Serialization;
using OfficeOpenXml;
using ProtoBuf;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace ET
{
    public static class ExcelExporterUI
    {
        private const string excelUIPath = "../Unity/Assets/Config/Excel/AbilityConfig/TextKeyValue/LocalizeConfig_UI.xlsx";
        private const string excelUIPathOut = "../Unity/Assets/Config/Excel/AbilityConfig/TextKeyValue/_LocalizeConfig_UI.txt";

        private static Dictionary<string, ExcelPackage> packages = new Dictionary<string, ExcelPackage>();

        public static ExcelPackage GetPackage(string filePath)
        {
            if (!packages.TryGetValue(filePath, out var package))
            {
                using Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                package = new ExcelPackage(stream);
                packages[filePath] = package;
            }

            return package;
        }

        public static void Export()
        {
            try
            {
                //防止编译时裁剪掉protobuf
                ProtoBuf.WireType.Fixed64.ToString();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                ExportExcel(excelUIPath);

                Log.Console("Export ExcelUI Sucess!");
            }
            catch (Exception e)
            {
                Log.Console(e.ToString());
            }
            finally
            {
                foreach (var kv in packages)
                {
                    kv.Value.Dispose();
                }

                packages.Clear();
            }
        }

        private static void ExportExcel(string path)
        {
            Table table = new Table();

            ExcelPackage p = GetPackage(Path.GetFullPath(path));

            ExportExcelJson(p, table);
        }

        #region 导出json


        static void ExportExcelJson(ExcelPackage p, Table table)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ExcelWorksheet worksheet in p.Workbook.Worksheets)
            {
                if (worksheet.Name.StartsWith("#"))
                {
                    continue;
                }

                ExportSheetJson(worksheet, table.HeadInfos, sb);
            }

            string jsonPath = excelUIPathOut;
            using FileStream txt = new FileStream(jsonPath, FileMode.Create);
            using StreamWriter sw = new StreamWriter(txt);
            sw.Write(sb.ToString());
        }

        static void ExportSheetJson(ExcelWorksheet worksheet, Dictionary<string, HeadInfo> classField, StringBuilder sb)
        {
            for (int row = 1; row <= worksheet.Dimension.End.Row; ++row)
            {
                string prefix1 = worksheet.Cells[row, 1].Text.Trim();
                if (prefix1.StartsWith("#"))
                {
                    continue;
                }
                string textKey = worksheet.Cells[row, 2].Text.Trim();
                string textValue = worksheet.Cells[row, 3].Text.Trim();

                sb.Append($"{textKey}|{textValue}\n");
            }
        }

        #endregion

    }
}
