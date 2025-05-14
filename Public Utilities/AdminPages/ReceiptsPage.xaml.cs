using Microsoft.Win32;
using Public_Utilities.Add_Folder;
using Public_Utilities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Public_Utilities.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для ReceiptsPage.xaml
    /// </summary>
    public partial class ReceiptsPage : System.Windows.Controls.Page
    {
        public ReceiptsPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DB.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ReceiptsGrid.ItemsSource = DB.GetContext().Receipts.ToList();
            }
        }

        private void Button_edit_data(object sender, RoutedEventArgs e)
        {
            var receiptRemoving = ReceiptsGrid.SelectedItems.Cast<Receipts>().ToList();
            try
            {
                Edit_Folder.Edit_Receipt edit_receipt
                    = new Edit_Folder.Edit_Receipt((sender as System.Windows.Controls.Button).DataContext as Receipts);
                edit_receipt.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Add_Receipt add_Receipt = new Add_Receipt();
            add_Receipt.Show();
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            var receiptRemoving = ReceiptsGrid.SelectedItems.Cast<Receipts>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {receiptRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DB.GetContext().Receipts.RemoveRange((IEnumerable<Receipts>)receiptRemoving);
                    DB.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    ReceiptsGrid.ItemsSource = DB.GetContext().Receipts.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Button_Click_Word(object sender, RoutedEventArgs e)
        {
            Word.Application word = new Word.Application();
            word.Visible = false;
            Word.Document document = word.Documents.Add();

            Word.Paragraph para = document.Content.Paragraphs.Add();
            para.Range.Text = "Отчет о квитанциях";

            Word.Table table = document.Tables.Add(para.Range, (ReceiptsGrid.Items.Count - 1) + 1, (ReceiptsGrid.Columns.Count - 1));
            table.Borders.Enable = 1;

            for (int j = 0; j < (ReceiptsGrid.Columns.Count - 1); j++)
            {
                table.Cell(1, j + 1).Range.Text = ReceiptsGrid.Columns[j].Header.ToString();
            }

            for (int i = 0; i < ReceiptsGrid.Items.Count; i++)
            {
                for (int j = 0; j < (ReceiptsGrid.Columns.Count - 1); j++)
                {
                    TextBlock b = ReceiptsGrid.Columns[j].GetCellContent(ReceiptsGrid.Items[i]) as TextBlock;
                    table.Cell(i + 2, j + 1).Range.Text = b.Text;
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word files (*.docx)|*.docx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                document.SaveAs2(filePath);
                document.Close();
                word.Quit();
            }
        }

        private void Button_Click_Excel(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = false;
            Workbook workbook = excel.Workbooks.Add();
            Worksheet worksheet = workbook.ActiveSheet;
            if (ReceiptsGrid != null)
            {
                for (int j = 0; j < (ReceiptsGrid.Columns.Count - 1); j++)
                {
                    worksheet.Cells[2, j + 1] = ReceiptsGrid.Columns[j].Header.ToString();
                }

                for (int i = 0; i < (ReceiptsGrid.Items.Count - 1); i++)
                {
                    for (int j = 0; j < (ReceiptsGrid.Columns.Count - 1); j++)
                    {
                        TextBlock b = ReceiptsGrid.Columns[j].GetCellContent(ReceiptsGrid.Items[i]) as TextBlock;
                        worksheet.Cells[i + 2, j + 1] = b.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("datagridkolvo не инициализирован или равен null.");
                worksheet.Cells[1, 1] = "Отчет о квитанциях";
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                workbook.SaveAs(filePath);
                workbook.Close();
                excel.Quit();
            }
        }
    }
}