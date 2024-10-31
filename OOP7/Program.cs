using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public interface IGeneric<T>
{
    void Add(T item);

    void Delete(T item);
    T Get(T item);
    void SaveToFile(string filePath);
    void LoadFromFile(string filePath);
}

public class CollectionType<T> : IGeneric<T> where T : IComparable<T>
{
    private List<T> collection;
    public CollectionType()
    {
        collection = new List<T>();
    }
    public void Add(T item)
    {
        try
        {
            collection.Add(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция добавления завершена.");
        }
    }
    public void Delete(T item)
    {
        try
        {
            collection.Remove(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция удаления завершена.");
        }
    }

    public T Get(T item)
    {
        try
        {
            T foundItem = collection.Find(x => x.CompareTo(item) == 0);

            if (foundItem != null)
            {
                Console.WriteLine($"Найден элемент: {foundItem}");
                return foundItem;
            }
            else
            {
                Console.WriteLine($"Элемент {item} не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при поиске: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция поиска завершена.");
        }

        return default;
    }

    public void SaveToFile(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (T item in collection)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            Console.WriteLine($"Данные успешно сохранены в файл: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении в файл: {ex.Message}");
        }
    }

    public void LoadFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                collection.Clear();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        T item = (T)Convert.ChangeType(line, typeof(T));
                        collection.Add(item);
                    }
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
                Console.WriteLine($"Данные успешно загружены из файла: {filePath}");
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке из файла: {ex.Message}");
        }
    }
}

public class CandyProduct
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double SugarContent { get; set; }

    public CandyProduct(string name, double weight, double sugarContent)
    {

        Name = name;
        Weight = weight;
        SugarContent = sugarContent;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Weight: {Weight}, Sugar conetent: {SugarContent}");
    }


}

public class CandyBox<T> where T : CandyProduct
{
    private List<T> candies = new List<T>();

    public void AddCandy(T candy)
    {
        candies.Add(candy);
    }

    public void RemoveCandy(T candy)
    {
        candies.Remove(candy);
    }

    public void DisplayAllCandies()
    {
        foreach (T candy in candies)
        {
            candy.DisplayInfo();
        }
    }

    public double GetTotalWeight()
    {
        return candies.Sum(candy => candy.Weight);
    }
}


namespace OOP7
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string filePath = "C:\\Users\\vlad\\source\\repos\\OOP7\\collection.txt";

            CollectionType<int> intCollection = new CollectionType<int>();
            intCollection.Add(10);
            intCollection.Add(20);
            intCollection.Get(10);
            intCollection.Delete(20);

            CollectionType<double> doubleCollection = new CollectionType<double>();
            doubleCollection.Add(10.5);
            doubleCollection.Add(20.3);
            doubleCollection.Get(10.5);
            doubleCollection.Delete(20.3);

            CollectionType<string> stringCollection = new CollectionType<string>();
            stringCollection.Add("white");
            stringCollection.Add("pony");
            stringCollection.Get("white");
            stringCollection.Delete("pony");

            stringCollection.SaveToFile(filePath);
            stringCollection.LoadFromFile(filePath);

            Console.WriteLine("=========================");

            CandyBox<CandyProduct> candyBox = new CandyBox<CandyProduct>();

            CandyProduct chocolate = new CandyProduct("Chocolate", 150, 15);
            CandyProduct gummyBear = new CandyProduct("Gummy Bear", 50, 10);
            CandyProduct lollipop = new CandyProduct("Lollipop", 20, 3);

            candyBox.AddCandy(chocolate);
            candyBox.AddCandy(gummyBear);
            candyBox.AddCandy(lollipop);
            candyBox.DisplayAllCandies();

            candyBox.GetTotalWeight();

            candyBox.RemoveCandy(gummyBear);

            candyBox.DisplayAllCandies();




        }
    }
}
