using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
interface ISortAlgorithm<T> where T : IComparable<T>
{
    List<T> Sort(List<T> input, Comparison<T> comparison);
}


class ShellSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        int n = input.Count;
        int gap = n / 2;

        while (gap > 0)
        {
            for (int i = gap; i < n; i++)
            {
                T temp = input[i];
                int j = i;

                while (j >= gap && comparison(input[j - gap], temp) > 0)
                {
                    input[j] = input[j - gap];
                    j -= gap;
                }

                input[j] = temp;
            }

            gap /= 2;
        }

        return input;
    }
}

class SelectionSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        int n = input.Count;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < n; j++)
            {
                if (comparison(input[j], input[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }

            // Swap elements
            T temp = input[i];
            input[i] = input[minIndex];
            input[minIndex] = temp;
        }

        return input;
    }
}

// Implementa los demás algoritmos de ordenamiento de manera similar.
class HeapSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        int n = input.Count;

        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Heapify(input, n, i, comparison);
        }

        for (int i = n - 1; i >= 0; i--)
        {
            T temp = input[0];
            input[0] = input[i];
            input[i] = temp;

            Heapify(input, i, 0, comparison);
        }

        return input;
    }

    void Heapify(List<T> arr, int n, int i, Comparison<T> comparison)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && comparison(arr[left], arr[largest]) > 0)
        {
            largest = left;
        }

        if (right < n && comparison(arr[right], arr[largest]) > 0)
        {
            largest = right;
        }

        if (largest != i)
        {
            T swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;

            Heapify(arr, n, largest, comparison);
        }
    }
}
class QuickSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        QuickSortRecursive(input, 0, input.Count - 1, comparison);
        return input;
    }

    void QuickSortRecursive(List<T> arr, int low, int high, Comparison<T> comparison)
    {
        if (low < high)
        {
            int partitionIndex = Partition(arr, low, high, comparison);

            QuickSortRecursive(arr, low, partitionIndex - 1, comparison);
            QuickSortRecursive(arr, partitionIndex + 1, high, comparison);
        }
    }

    int Partition(List<T> arr, int low, int high, Comparison<T> comparison)
    {
        T pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (comparison(arr[j], pivot) < 0)
            {
                i++;
                T temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        T temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }
}
class BubbleSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        int n = input.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (comparison(input[j], input[j + 1]) > 0)
                {
                    T temp = input[j];
                    input[j] = input[j + 1];
                    input[j + 1] = temp;
                }
            }
        }

        return input;
    }
}
class CocktailSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        int n = input.Count;
        bool swapped;

        do
        {
            swapped = false;

            for (int i = 0; i < n - 1; i++)
            {
                if (comparison(input[i], input[i + 1]) > 0)
                {
                    T temp = input[i];
                    input[i] = input[i + 1];
                    input[i + 1] = temp;
                    swapped = true;
                }
            }

            if (!swapped)
                break;

            swapped = false;

            for (int i = n - 2; i >= 0; i--)
            {
                if (comparison(input[i], input[i + 1]) > 0)
                {
                    T temp = input[i];
                    input[i] = input[i + 1];
                    input[i + 1] = temp;
                    swapped = true;
                }
            }

        } while (swapped);

        return input;
    }
}
class InsertionSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        int n = input.Count;

        for (int i = 1; i < n; i++)
        {
            T key = input[i];
            int j = i - 1;

            while (j >= 0 && comparison(input[j], key) > 0)
            {
                input[j + 1] = input[j];
                j = j - 1;
            }

            input[j + 1] = key;
        }

        return input;
    }
}
class BucketSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        int n = input.Count;
        List<T>[] buckets = new List<T>[n];

        for (int i = 0; i < n; i++)
            buckets[i] = new List<T>();

        foreach (var item in input)
        {
            int bucketIndex = (int)((dynamic)item * n);
            buckets[bucketIndex].Add(item);
        }

        foreach (var bucket in buckets)
            bucket.Sort(comparison);

        input.Clear();

        foreach (var bucket in buckets)
            input.AddRange(bucket);

        return input;
    }
}
class CountingSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        int n = input.Count;

        // Encuentra el rango de valores en la lista
        T min = input[0], max = input[0];
        for (int i = 1; i < n; i++)
        {
            if (comparison(input[i], min) < 0)
                min = input[i];
            if (comparison(input[i], max) > 0)
                max = input[i];
        }

        int range = Convert.ToInt32(max) - Convert.ToInt32(min) + 1;

        // Inicializa el arreglo de recuento y el arreglo de salida
        int[] count = new int[range];
        T[] output = new T[n];

        // Llena el arreglo de recuento
        for (int i = 0; i < n; i++)
            count[Convert.ToInt32(input[i]) - Convert.ToInt32(min)]++;

        // Ajusta el arreglo de recuento
        for (int i = 1; i < range; i++)
            count[i] += count[i - 1];

        // Construye el arreglo de salida
        for (int i = n - 1; i >= 0; i--)
        {
            output[count[Convert.ToInt32(input[i]) - Convert.ToInt32(min)] - 1] = input[i];
            count[Convert.ToInt32(input[i]) - Convert.ToInt32(min)]--;
        }

        // Copia el arreglo de salida al arreglo original
        for (int i = 0; i < n; i++)
            input[i] = output[i];

        return input;
    }
}
class MergeSort<T> : ISortAlgorithm<T> where T : IComparable<T>
{
    public List<T> Sort(List<T> input, Comparison<T> comparison)
    {
        if (input.Count <= 1)
            return input;

        int middle = input.Count / 2;
        List<T> left = input.GetRange(0, middle);
        List<T> right = input.GetRange(middle, input.Count - middle);

        left = Sort(left, comparison);
        right = Sort(right, comparison);

        return Merge(left, right, comparison);
    }

    private List<T> Merge(List<T> left, List<T> right, Comparison<T> comparison)
    {
        List<T> result = new List<T>();
        int leftIndex = 0, rightIndex = 0;

        while (leftIndex < left.Count && rightIndex < right.Count)
        {
            if (comparison(left[leftIndex], right[rightIndex]) <= 0)
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }
            else
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }
        }

        while (leftIndex < left.Count)
        {
            result.Add(left[leftIndex]);
            leftIndex++;
        }

        while (rightIndex < right.Count)
        {
            result.Add(right[rightIndex]);
            rightIndex++;
        }

        return result;
    }
}
class BinaryTreeSort<TNode> : ISortAlgorithm<TNode> where TNode : IComparable<TNode>
{
    private Node root;

    public List<TNode> Sort(List<TNode> input, Comparison<TNode> comparison)
    {
        foreach (var item in input)
            root = InsertRec(root, item);

        List<TNode> sortedList = new List<TNode>();
        InOrderTraversalRec(root, sortedList.Add);

        return sortedList;
    }

    private Node InsertRec(Node root, TNode value)
    {
        if (root == null)
            return new Node(value);

        if (value.CompareTo(root.Value) < 0)
            root.Left = InsertRec(root.Left, value);
        else if (value.CompareTo(root.Value) > 0)
            root.Right = InsertRec(root.Right, value);

        return root;
    }

    private void InOrderTraversalRec(Node root, Action<TNode> action)
    {
        if (root != null)
        {
            InOrderTraversalRec(root.Left, action);
            action(root.Value);
            InOrderTraversalRec(root.Right, action);
        }
    }

    private class Node
    {
        public TNode Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(TNode value)
        {
            Value = value;
        }
    }
}
class RadixSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            int maxDigits = GetMaxDigits(input);

            for (int digitPlace = 1; digitPlace <= maxDigits; digitPlace++)
                CountingSortByDigit(input, digitPlace, comparison);

            return input;
        }

        private int GetMaxDigits(List<T> input)
        {
            int maxDigits = 0;
            foreach (var item in input)
            {
                int numDigits = GetNumberOfDigits(Convert.ToInt32(item));
                if (numDigits > maxDigits)
                    maxDigits = numDigits;
            }
            return maxDigits;
        }

        private int GetNumberOfDigits(int num)
        {
            if (num == 0)
                return 1;

            int count = 0;
            while (num != 0)
            {
                num /= 10;
                count++;
            }

            return count;
        }

        private void CountingSortByDigit(List<T> input, int digitPlace, Comparison<T> comparison)
        {
            int n = input.Count;
            List<T> output = new List<T>(new T[n]);
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
            {
                int digit = GetDigitAtPlace(Convert.ToInt32(input[i]), digitPlace);
                count[digit]++;
            }

            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            for (int i = n - 1; i >= 0; i--)
            {
                int digit = GetDigitAtPlace(Convert.ToInt32(input[i]), digitPlace);
                output[count[digit] - 1] = input[i];
                count[digit]--;
            }

            for (int i = 0; i < n; i++)
                input[i] = output[i];
        }

        private int GetDigitAtPlace(int num, int place)
        {
            while (place > 1)
            {
                num /= 10;
                place--;
            }

            return num % 10;
        }
    }
    class GnomeSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            int n = input.Count;
            int index = 0;

            while (index < n)
            {
                if (index == 0)
                    index++;

                if (comparison(input[index], input[index - 1]) >= 0)
                    index++;
                else
                {
                    Swap(input, index, index - 1);
                    index--;
                }
            }

            return input;
        }

        private void Swap(List<T> input, int i, int j)
        {
            T temp = input[i];
            input[i] = input[j];
            input[j] = temp;
        }
    }


    class Unit : IComparable<Unit>
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int AttackPower { get; set; }
        public int Speed { get; set; }

        public int CompareTo(Unit other)
        {
            // Implementa la lógica de comparación aquí
            // Puedes comparar por nivel, velocidad, poder de ataque, etc.
            // Ejemplo: return this.Level.CompareTo(other.Level);

            // Por ejemplo, comparar por nivel y luego por velocidad si los niveles son iguales
            int levelComparison = this.Level.CompareTo(other.Level);
            if (levelComparison != 0)
            {
                return levelComparison;
            }

            return this.Speed.CompareTo(other.Speed);
        }

        public override string ToString()
        {
            return $"{Name} (Level: {Level}, Attack: {AttackPower}, Speed: {Speed})";
        }
    }


    class ArmyManagement
    {
        static void Main()
        {
            bool siono = true;
            do
            {
                Game.RunGame();
                Console.WriteLine("Usar otro? y/n");
                string resp = Console.ReadLine();
            if (resp == "y"||resp=="Y")
            {
               
            }
            if (resp == "n"||resp=="N")
                {
                    siono = false;
                }
            else
            {
                Console.WriteLine("ESCRIBA y PARA SI \n ESCRIBA n para No");
            }

        } while (siono == true);
            
         
        }
    }

    class Game
    {
    public static void RunGame()
    {
        List<Unit> army = GenerateArmy();

        Console.WriteLine("Original Army:");
        PrintArmy(army);

        ISortAlgorithm<Unit> sortingAlgorithm = null;

        while (sortingAlgorithm == null)
        {
            Console.WriteLine("Choose Sorting Algorithm:");
            Console.WriteLine("1. Shell Sort");
            Console.WriteLine("2. Selection Sort");
            Console.WriteLine("3. HeapSort");
            Console.WriteLine("4. QuickSort");
            Console.WriteLine("5. BubbleSort");
            Console.WriteLine("6. CocktailSort");
            Console.WriteLine("7. InsertionSort");
            Console.WriteLine("8. BucketSort");
            Console.WriteLine("9. CountingSort");
            Console.WriteLine("10. MergeSort");
            Console.WriteLine("11. BinaryTreeSort");
            Console.WriteLine("12. RadixSort");
            Console.WriteLine("13. GnomeSort");
            // Agrega opciones para los demás algoritmos de ordenamiento

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 1 && choice <= 13)
                {
                    sortingAlgorithm = GetSortingAlgorithm(choice);

                    if (sortingAlgorithm != null)
                    {
                        army = sortingAlgorithm.Sort(army, Comparer<Unit>.Default.Compare);

                        Console.WriteLine("\nSorted Army:");
                        PrintArmy(army);
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid option (1-13).");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }
    }

        static ISortAlgorithm<Unit> GetSortingAlgorithm(int choice)
        {
            switch (choice)
            {
                case 1:
                    return new ShellSort<Unit>();
                case 2:
                    return new SelectionSort<Unit>();
            case 3:
                return new HeapSort<Unit>();
            case 4:
                return new QuickSort<Unit>();
            case 5:
                return new BubbleSort<Unit>();
            case 6:
                return new CocktailSort<Unit>();
            case 7:
                return new InsertionSort<Unit>();
            case 8:
                return new BucketSort<Unit>();
            case 9:
                return new CountingSort<Unit>();
            case 10:
                return new MergeSort<Unit>();
            case 11:
                return new BinaryTreeSort<Unit>();
            case 12:
                return new RadixSort<Unit>();
            case 13:
                return new GnomeSort<Unit>();
          
            // Agrega casos para los demás algoritmos de ordenamiento
            default:
                    return null;
            }
        }

        static void PrintArmy(List<Unit> army)
        {
            foreach (var unit in army)
            {
                Console.WriteLine(unit);
            }
        }

        static List<Unit> GenerateArmy()
        {
            Random random = new Random();
            List<Unit> army = new List<Unit>();

            for (int i = 0; i < 5; i++)
            {
                army.Add(new Unit
                {
                    Name = $"Unit{i + 1}",
                    Level = random.Next(1, 10),
                    AttackPower = random.Next(10, 30),
                    Speed = random.Next(5, 20)
                });
            }

            return army;
        }
    }
