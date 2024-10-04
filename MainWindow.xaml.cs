using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace SortApp
{
    public partial class MainWindow : Window
    {
        // Делегат для сортировки
        public delegate int[] SortDelegate(int[] data);

        public MainWindow()
        {
            InitializeComponent();
        }

        // Пузырьковая сортировка
        public int[] BubbleSort(int[] data)
        {
            int n = data.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        int temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
            return data;
        }

        // Быстрая сортировка
        public int[] QuickSort(int[] data)
        {
            if (data.Length <= 1) return data;

            int pivot = data[data.Length / 2];
            int[] left = data.Where(x => x < pivot).ToArray();
            int[] right = data.Where(x => x > pivot).ToArray();

            return QuickSort(left).Concat(new int[] { pivot }).Concat(QuickSort(right)).ToArray();
        }

        // Обработчик нажатия на кнопку "Отсортировать"
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из текстового поля
            string input = NumbersTextBox.Text;
            string[] stringNumbers = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] numbers;

            try
            {
                // Преобразуем строку в массив чисел
                numbers = stringNumbers.Select(int.Parse).ToArray();
            }
            catch
            {
                ResultTextBlock.Text = "Ошибка: введите корректные числа через запятую.";
                return;
            }

            // Выбор метода сортировки
            ComboBoxItem selectedSortMethod = (ComboBoxItem)SortComboBox.SelectedItem;
            SortDelegate sortDelegate;

            if (selectedSortMethod == null)
            {
                ResultTextBlock.Text = "Выберите метод сортировки.";
                return;
            }

            if (selectedSortMethod.Content.ToString() == "Пузырьковая сортировка")
            {
                sortDelegate = BubbleSort;
            }
            else if (selectedSortMethod.Content.ToString() == "Быстрая сортировка")
            {
                sortDelegate = QuickSort;
            }
            else
            {
                ResultTextBlock.Text = "Ошибка выбора метода сортировки.";
                return;
            }

            // Выполняем сортировку
            int[] sortedNumbers = sortDelegate(numbers);

            // Выводим результат
            ResultTextBlock.Text = "Отсортированные числа:\n" + string.Join(", ", sortedNumbers);
        }
    }
}
