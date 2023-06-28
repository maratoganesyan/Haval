using System;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;

namespace HavalProject.Tools.Instruments
{
    internal static class ToolsForGeneration
    {
        public static void ReplaceWord(string Original, string NewText, Document WordDocument)
        {
            Word.Range Range = WordDocument.Content;
            Range.Find.ClearFormatting();
            Range.Find.Execute(FindText: Original, ReplaceWith: NewText);
        }

        public static void AutoSizeColumn(Table MyTable, int ColumnNumber)
        {
            MyTable.AllowAutoFit = true;
            Column SomeColumn = MyTable.Columns[ColumnNumber];
            SomeColumn.AutoFit(); // force fit sizing
            float ColAutoWidth = SomeColumn.Width; // store autofit width
            MyTable.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitWindow); // fill page width
            SomeColumn.SetWidth(ColAutoWidth, WdRulerStyle.wdAdjustFirstColumn); // reset width keeping right table margin
        }
    }
}
