using HavalProject.Entities;
using HavalProject.Tools.Instruments;
using HavalProject.Tools.ModelsOfETypes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using System.Windows;

namespace HavalProject.Tools
{
    class GenerationsOfReports
    {
        #region Check
        public static void DoACheck(Order order)
        {
            OrderModel orderModel = new OrderModel(order);
            GenerateCheck(orderModel);
        }
        public static async System.Threading.Tasks.Task DoACheckAsync(Order order,
            FrameworkElement ControlToDisable,
            FrameworkElement ControlToVisible)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Microsoft Word Document (*.docx)|*.docx";
            if (sv.ShowDialog() == true)
            {
                ControlToDisable.IsEnabled = false;
                ControlToVisible.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Run(() => GenerateCheck(new OrderModel(order), sv.FileName));
                MessageBox.Show("Генерация чека успешно завершена!");
                ControlToDisable.IsEnabled = true;
                ControlToVisible.Visibility = Visibility.Hidden;
            }
        }
        private static void GenerateCheck(OrderModel order, string FilePath)
        {
            try
            {
                Word.Application App = new Word.Application();
                App.Visible = false;
                Word.Document document = App.Documents.Open(Directories.Check());

                ChangeWordsInCheck(order, document);

                DoAllTableChack(order, document);

                document.SaveAs2(FileName: FilePath);
                document.Close();
                App.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void GenerateCheck(OrderModel order)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Microsoft Word Document (*.docx)|*.docx";
            if (sv.ShowDialog() == true)
            {
                Word.Application App = new Word.Application();
                App.Visible = false;
                Word.Document document = App.Documents.Open(Directories.Check());

                ChangeWordsInCheck(order, document);

                DoAllTableChack(order, document);

                document.SaveAs2(FileName: sv.FileName);
                document.Close();
                App.Quit();
            }
        }
        private static void ChangeWordsInCheck(OrderModel order, Word.Document document)
        {
            ToolsForGeneration.ReplaceWord("{price}", $"{order.TotalCost:N2}", document);
            ToolsForGeneration.ReplaceWord("{current_date}", order.DateTimeModel.ToString("g"), document);
            ToolsForGeneration.ReplaceWord("{cashier_name}", $"{order.SurnameOfManager} {order.NameOfManager}", document);
            ToolsForGeneration.ReplaceWord("{client_name}", $"{order.SurnameOfClient} {order.NameOfClient}", document);
        }
        private static void DoAllTableChack(OrderModel order, Word.Document document)
        {
            Word.Table table = document.Tables.Add(document.Bookmarks["Table"].Range, 6, 2);

            table.Rows[1].Cells[1].Range.Text = "Марка и модель автомобиля";
            table.Rows[1].Cells[2].Range.Text = $"{string.Join(' ', order.MarkOfCar, order.ModelName)}";
            table.Rows[2].Cells[1].Range.Text = "Комплектация авто";
            table.Rows[2].Cells[2].Range.Text = $"{order.ConfigurationName}";
            table.Rows[3].Cells[1].Range.Text = "Цвет авто";
            table.Rows[3].Cells[2].Range.Text = $"{order.ColorName}";

            table.Rows[4].Cells[1].Range.Text = "Цена авто (руб.)";
            table.Rows[4].Cells[2].Range.Text = $"{order.PriceOfCar:N2}";
            table.Rows[4].Cells[2].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            table.Rows[5].Cells[1].Range.Text = "Список дополнительных услуг";
            table.Rows[5].Cells[2].Range.Text = $"{string.Join(", ", order.AdditationalServices)}";

            table.Rows[6].Cells[1].Range.Text = "Стоимость дополнительных услуг (руб.)";
            table.Rows[6].Cells[2].Range.Text = $"{order.PriceOfAdditationalServices:N2}";
            table.Rows[6].Cells[2].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            table.Borders.Enable = 1;
        }
        #endregion

        #region SalesReport
        public static async System.Threading.Tasks.Task DoASalesReportAsync(ModelForSalesReport Data,
            FrameworkElement ControlToDisable,
            FrameworkElement ControlToVisible)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Microsoft Word Document (*.docx)|*.docx";
            if (sv.ShowDialog() == true)
            {
                ControlToDisable.IsEnabled = false;
                ControlToVisible.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Run(() => GenerateSalesReport(Data, sv.FileName));
                MessageBox.Show("Генерация отчёта успешно завершена!");
                ControlToDisable.IsEnabled = true;
                ControlToVisible.Visibility = Visibility.Hidden;
            }
        }
        private static void GenerateSalesReport(ModelForSalesReport Data, string FilePath)
        {
            try
            {
                Word.Application App = new Word.Application();
                App.Visible = false;

                Word.Document document = App.Documents.Open(Directories.SalesReport());

                ChangeFieldsSalesReport(Data, document);

                DoATableSalesReport(Data, document);

                document.SaveAs2(FileName: FilePath);
                document.Close();
                App.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void ChangeFieldsSalesReport(ModelForSalesReport Data, Word.Document document)
        {
            ToolsForGeneration.ReplaceWord("{current_date}", DateTime.Now.ToString("g"), document);
            ToolsForGeneration.ReplaceWord("{startDate}", Data.StartDate.ToString("d"), document);
            ToolsForGeneration.ReplaceWord("{endDate}", Data.EndDate.ToString("d"), document);
            ToolsForGeneration.ReplaceWord("{AllTotalCost}", $"{Data.TotalCostOfReport:N2} руб.", document);
        }
        private static void DoATableSalesReport(ModelForSalesReport Data, Word.Document document)
        {
            int CountOfRows = 1 + Data
                .OrdersForReport
                .Select(x => x.IdCarNavigation.IdModel)
                .Distinct()
                .Count() + Data.OrdersForReport.Count();
            Word.Table table = document.Tables.Add(document.Bookmarks["Table"].Range, CountOfRows, 5);
            table.Rows[1].Cells[1].Range.Text = "Клиент";
            table.Rows[1].Cells[2].Range.Text = "Модель автомобиля";
            table.Rows[1].Cells[3].Range.Text = "Менеджер по продажам";
            table.Rows[1].Cells[4].Range.Text = "Дата/время продажи";
            table.Rows[1].Cells[5].Range.Text = "Стоимость (руб.)";

            int index = 0;
            Order PreviousOrder = null;
            bool SpecialString = false;

            foreach (Row row in table.Rows)
            {
                if (row.Index == 1)
                    continue;

                //Проверка на окончание Model
                if (SpecialString || index == Data.OrdersForReport.Count)
                {
                    row.Cells[2].Range.Text = $"Haval {Data.OrdersForReport[index - 1].IdCarNavigation.IdModelNavigation.ModelName}";
                    row.Cells[2].Range.Bold = 1;

                    decimal PriceOfPrevModels = Data
                        .OrdersForReport
                        .Where(x
                            => x.IdCarNavigation.IdModel ==
                            Data.OrdersForReport[index - 1].IdCarNavigation.IdModel)
                        .Select(x => x.IdCarNavigation.ClientPrice)
                        .Sum();
                    row.Cells[5].Range.Text = $"{PriceOfPrevModels:N2}";
                    row.Cells[5].Range.Bold = 1;
                    row.Cells[5].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

                    SpecialString = false;

                    continue;
                }

                if (index != Data.OrdersForReport.Count)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        if (cell.ColumnIndex == 5)
                            cell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        string InitialsOfClient = Data.OrdersForReport[index].IdClientNavigation.Surname +
                            " " +
                            Data.OrdersForReport[index].IdClientNavigation.Name[0] +
                            ".";
                        string InitialsOfManager = Data.OrdersForReport[index].IdManagerNavigation.Surname +
                            " " +
                            Data.OrdersForReport[index].IdManagerNavigation.Name[0] +
                            ".";
                        _ = cell.ColumnIndex == 1 ?
                            cell.Range.Text = $"{InitialsOfClient}" :
                            cell.ColumnIndex == 2 ?
                            cell.Range.Text = $" Haval {Data.OrdersForReport[index].IdCarNavigation.IdModelNavigation.ModelName}" :
                            cell.ColumnIndex == 3 ?
                            cell.Range.Text = $"{InitialsOfManager}" :
                            cell.ColumnIndex == 4 ?
                            cell.Range.Text = Data.OrdersForReport[index].DateTimeOfOrder.ToString("g") :
                            cell.Range.Text = $"{Data.OrdersForReport[index].IdCarNavigation.ClientPrice:N2}";

                    }
                }
                if (index != Data.OrdersForReport.Count)
                    PreviousOrder = Data.OrdersForReport[index];
                index++;
                if (index != 0 && index != Data.OrdersForReport.Count &&
                    (Data.OrdersForReport[index].IdCarNavigation.IdModel !=
                    Data.OrdersForReport[index - 1].IdCarNavigation.IdModel) &&
                    PreviousOrder != Data.OrdersForReport[index])
                    SpecialString = true;
            }
            Tools.Instruments.ToolsForGeneration.AutoSizeColumn(table, 4);
            table.Borders.Enable = 1;
        }
        #endregion

        #region SupplyReport
        public static async System.Threading.Tasks.Task DoASupplyReportAsync(ModelForSupplyReport Data,
            FrameworkElement ControlToDisable,
            FrameworkElement ControlToVisible)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Microsoft Word Document (*.docx)|*.docx";
            if (sv.ShowDialog() == true)
            {
                ControlToDisable.IsEnabled = false;
                ControlToVisible.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Run(() => GenerateSupplyReport(Data, sv.FileName));
                MessageBox.Show("Генерация отчётау успешно завершена!");
                ControlToDisable.IsEnabled = true;
                ControlToVisible.Visibility = Visibility.Hidden;
            }
        }
        private static void GenerateSupplyReport(ModelForSupplyReport Data, string FilePath)
        {
            try
            {
                Word.Application App = new Word.Application();
                App.Visible = false;

                Word.Document document = App.Documents.Open(Directories.SupplyReport());

                ChangeFieldsSupplyReport(Data, document);

                DoATableSupplyReport(Data, document);

                document.SaveAs2(FileName: FilePath);
                document.Close();
                App.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void ChangeFieldsSupplyReport(ModelForSupplyReport Data, Word.Document document)
        {
            ToolsForGeneration.ReplaceWord("{current_date}", DateTime.Now.ToString("g"), document);
            ToolsForGeneration.ReplaceWord("{startDate}", Data.StartDate.ToString("d"), document);
            ToolsForGeneration.ReplaceWord("{endDate}", Data.EndDate.ToString("d"), document);
            ToolsForGeneration.ReplaceWord("{AllTotalCost}", $"{Data.TotalCost:N2} руб.", document);
        }
        private static void DoATableSupplyReport(ModelForSupplyReport Data, Word.Document document)
        {
            int CountOfRows = 1 + Data.GoodsInSupplies.Count;
            Word.Table table = document.Tables.Add(document.Bookmarks["Table"].Range, CountOfRows, 6);
            table.Rows[1].Cells[1].Range.Text = "Модель автомобиля";
            table.Rows[1].Cells[2].Range.Text = "Цвет автомобиля";
            table.Rows[1].Cells[3].Range.Text = "Комплектация автомобиля";
            table.Rows[1].Cells[4].Range.Text = "Дата/время поставки";
            table.Rows[1].Cells[5].Range.Text = "Цена автомобиля за 1 штуку (руб.)";
            table.Rows[1].Cells[6].Range.Text = "Количество авто в поставке (шт.)";

            int index = 0;

            foreach (Row row in table.Rows)
            {
                if (row.Index == 1)
                    continue;
                foreach (Cell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 5)
                        cell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                    _ = cell.ColumnIndex == 1 ?
                        cell.Range.Text = $"Haval {Data.GoodsInSupplies[index].IdCarNavigation.IdModelNavigation.ModelName}" :
                        cell.ColumnIndex == 2 ?
                        cell.Range.Text = $"{Data.GoodsInSupplies[index].IdCarNavigation.IdColorNavigation.ColorName}" :
                        cell.ColumnIndex == 3 ?
                        cell.Range.Text = $"{Data.GoodsInSupplies[index].IdCarNavigation.IdConfigurationNavigation.ConfigurationName}" :
                        cell.ColumnIndex == 4 ?
                        cell.Range.Text = Data
                                          .GoodsInSupplies[index]
                                          .IdSupplyNavigation
                                          .DateTimeOfSupply
                                          .Value.ToString("g") :
                        cell.ColumnIndex == 5 ?
                        cell.Range.Text = $"{Data.GoodsInSupplies[index].SupplyPrice:N2}" :
                        cell.Range.Text = $"{Data.GoodsInSupplies[index].Quantity}";
                }
                index++;
            }
            Tools.Instruments.ToolsForGeneration.AutoSizeColumn(table, 3);
            table.Borders.Enable = 1;
        }
        #endregion
    }
}
