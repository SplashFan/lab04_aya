/* Задание 1
Создайте класс MyMatrix, представляющий матрицу m на n.
Создайте конструктор, принимающий число строк и столбцов, заполняющий матрицу случайными числами в диапазоне,
который пользователь вводит при запуске программы.
Определите операторы сложения, вычитания и умножения матриц, а также умножения и деления матрицы на число.
Создайте пользовательский индексатор матрицы для доступа к элементам матрицы по номеру строки и столбца.*/
Console.WriteLine("Задание 1");
Console.WriteLine("Введите диапазон значений для матрицы: ");
Console.Write("Минимальное: ");
int min = Convert.ToInt32(Console.ReadLine());
Console.Write("Максимальное: ");
int max = Convert.ToInt32(Console.ReadLine());

MyMatrix matrix1 = new MyMatrix(3, 3, min, max);
MyMatrix matrix2 = new MyMatrix(3, 3, min, max);
MyMatrix matrix3 = new MyMatrix(3, 2, min, max);

Console.WriteLine($"Матрица 1: ");
matrix1.Print();

Console.WriteLine("Матрица 2: ");
matrix2.Print();

Console.WriteLine("Сумма матриц 1 и 2: ");
(matrix1 + matrix2).Print();

Console.WriteLine("Разность матриц 1 и 2: ");
(matrix1 - matrix2).Print();

Console.WriteLine("Умножение матрицы 3x3 на матрицу 3x2: ");
(matrix1 * matrix3).Print();

Console.WriteLine("Умножение матрицы 1 на число: ");
(matrix1 * 6).Print();

Console.WriteLine("Деление матрицы 1 на число: ");
(matrix1 / 8).Print();

/* Задание 2
Создайте класс Car с тремя авто-свойствами: Name, ProductionYear и MaxSpeed, соответствующими названию, 
году выпуска и максимальной скорости соответственно.
Создайте класс CarComparer, наследуемый от IComparer<Car> и реализуйте метод Compare таким образом, 
чтобы можно было сортировать массив элементов Car по названию, году выпуска или максимальной скорости по выбору.
Создайте массив элементов Car и продемонстрируйте сортировку различными способами.*/

Console.WriteLine("Задание 2");
Car car1 = new Car("BMW", 2000, 240);
Car car2 = new Car("Ferrari", 2014, 280);
Car car3 = new Car("Zhigul", 1950, 100);

List<Car> cars = new List<Car> { car1, car2, car3 };
Console.WriteLine("Машины: ");
foreach(var car in cars)
{
    car.Print();
}

Console.WriteLine("\nОтсортировано по имени: ");
cars.Sort(new CarComparer("Название"));
foreach (var car in cars)
{
    car.Print();
}

Console.WriteLine("\nОтсортировано по году выпуска: ");
cars.Sort(new CarComparer("Год"));
foreach (var car in cars)
{
    car.Print();
}

Console.WriteLine("\nОтсортировано по максимальной скорости: ");
cars.Sort(new CarComparer("Скорость"));
foreach (var car in cars)
{
    car.Print();
}

/* Задание 3
Используйте класс Car из задания №2, на его основе создайте класс CarCatalor, содержащий массив элементов типа Car. 
Для класса CarCatalog реализуйте возможность итерации по элементам массива Car с помощью оператора foreach 
различными способами: 
Прямой проход с первого элемента до последнего.
Обратный проход от последнего к первому.
Проход по элементам массива с фильтром по году выпуска.
Проход по элементам массива с фильтром по максимальной скорости. */

//Переменные для машин уже есть выше
Console.WriteLine("\nЗадание 3");
CarCatalor an_cars = new CarCatalor(car1, car2, car3);
Console.WriteLine("\nМашины: ");
foreach (var car in an_cars)
{
    car.Print();
}

Console.WriteLine("\nВ обратном порядке: ");
foreach (var car in an_cars.Reverse())
{
    car.Print();
}

Console.WriteLine("\nС фильтром по году выпуска (2014): ");
foreach (var car in an_cars.Filtr("Год", 2014))
{
    car.Print();
}

Console.WriteLine("\nС фильтром по скорости(240): ");
foreach (var car in an_cars.Filtr("Скорость", 240))
{
    car.Print();
}
class MyMatrix //Для задания 1
{
    List<List<double>> matrix;

    public int n
    {
        get { return matrix.Count; }
    }

    public int m
    {
        get { return matrix[0].Count(); }
    }

    public MyMatrix(int n, int m, int min, int max)
    {
        Random rand = new Random();
        matrix = new List<List<double>>();
        for (int i = 0; i < n; i++)
        {
            matrix.Add(new List<double>());
            for (int j = 0; j < m; j++)
            {
                matrix[i].Add(rand.Next(min, max));
            }
        }
    }
    public double this[int a, int b]
    {
        get { return this.matrix[a][b]; }
        set { this.matrix[a][b] = value; }
    }

    public static MyMatrix operator +(MyMatrix matrix1, MyMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m) return new MyMatrix(0, 0, 0, 0);
        MyMatrix res_matrix = new MyMatrix(matrix1.n, matrix1.m, 0, 0);
        for (int i = 0; i < matrix1.n; i++)
        {
            for (int j = 0; j < matrix1.m; j++)
            {
                res_matrix[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }

        return res_matrix;
    }

    public static MyMatrix operator -(MyMatrix matrix1, MyMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m) return new MyMatrix(0, 0, 0, 0);
        MyMatrix res_matrix = new MyMatrix(matrix1.n, matrix1.m, 0, 0);
        for (int i = 0; i < matrix1.n; i++)
        {
            for (int j = 0; j < matrix1.m; j++)
            {
                res_matrix[i, j] = matrix1[i, j] - matrix2[i, j];
            }
        }

        return res_matrix;
    }

    public static MyMatrix operator *(MyMatrix matrix1, MyMatrix matrix2)
    {
        if (matrix1.m != matrix2.n) return new MyMatrix(0, 0, 0, 0);
        MyMatrix res_matrix = new MyMatrix(matrix1.n, matrix2.m, 0, 0);
        for (int i = 0; i < matrix1.n; i++)
        {
            for (int j = 0; j < matrix2.m; j++)
            {
                res_matrix[i, j] = 0;
                for (int k = 0; k < matrix1.m; k++)
                {
                    res_matrix[i, j] += matrix1[i, k] * matrix2[k, j];
                }
            }
        }
        return res_matrix;
    }

    public static MyMatrix operator *(MyMatrix matrix1, int num)
    {
        MyMatrix res_matrix = new MyMatrix(matrix1.n, matrix1.m, 0, 0);
        for (int i = 0; i < matrix1.n; i++)
        {
            for (int j = 0; j < matrix1.m; j++)
            {
                res_matrix[i, j] = matrix1[i, j] * num;
            }
        }

        return res_matrix;
    }

    public static MyMatrix operator /(MyMatrix matrix1, int num)
    {
        MyMatrix res_matrix = new MyMatrix(matrix1.n, matrix1.m, 0, 0);
        for (int i = 0; i < matrix1.n; i++)
        {
            for (int j = 0; j < matrix1.n; j++)
            {
                res_matrix[i, j] = matrix1[i, j] / num;
            }
        }

        return res_matrix;
    }

    public void Print()
    {
        foreach (var row in matrix)
        {
            foreach (var num in row)
            {
                Console.Write($"{num}; ");
            }
            Console.WriteLine("");
        }
        Console.WriteLine();
    }
}
public class Car  //Для задания 2
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }

    public Car(string name, int productoinYear, int maxSpeed)
    {
        Name = name; ProductionYear = productoinYear; MaxSpeed = maxSpeed;
    }

    public void Print()
    {
        Console.WriteLine($"Название: {Name}, год выпуска: {ProductionYear}, максимальная скорость: {MaxSpeed}");
    }
}
class CarComparer : IComparer<Car>
{
    private string Arg { get; set; }
    public CarComparer(string arg)
    {
        Arg = arg;
    }

    public int Compare(Car? x, Car? y)
    {
        if (x == null || y == null)
        {
            return 0;
        }

        return Arg switch
        {
            "Название" => x.Name.CompareTo(y.Name),
            "Год" => x.ProductionYear.CompareTo(y.ProductionYear),
            "Скорость" => x.MaxSpeed.CompareTo(y.MaxSpeed),
            _ => 0, //Не забыть спросить, как избавиться от предупреждения иначе
        };
    }
}

public class CarCatalor // Для задания 3
{
    private Car[] cars;
    public CarCatalor(params Car[] _cars)
    {
        cars = _cars;
    }

    public IEnumerator<Car> GetEnumerator()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            yield return cars[i];
        }
    }

    public IEnumerable<Car> Reverse()
    {
        for (int i = cars.Length - 1; i > -1; i--)
        {
            yield return cars[i];
        }
    }

    public IEnumerable<Car> Filtr(string arg, int val)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            switch (arg)
            {
                case "Год":
                    {
                        if (cars[i].ProductionYear == val) yield return cars[i];
                        break;
                    }
                case "Скорость":
                    {
                        if (cars[i].MaxSpeed == val) yield return cars[i];
                        break;
                    }
            }
        }
    }
}






