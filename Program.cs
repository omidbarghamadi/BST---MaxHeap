using System;

class Person
{
    public int age;
    public char skill;

    public Person(int age, char skill)
    {
        this.age = age;
        this.skill = skill;
    }
}
class Maxheap
{
    public Person[] heap;
    public int size;

    public Maxheap(int len)
    {
        heap = new Person[len];
        this.size = -1; 
    }
    public Maxheap()
    {
        heap = new Person[100];
        this.size = -1;
    }

    public int Left(int i)
    {
        return i * 2 + 1;
    }
    public int Right(int i)
    {
        return i * 2 + 2;
    }
    public int Parent(int i)
    {
        return (i - 1) / 2;
    }

    void MaxHeapfy(int i)
    {
        int largest = i;

        int l = Left(i);
        int r = Right(i);

        if (l <= size && heap[l].skill > heap[largest].skill)
        {
            largest = l;
        }
        if (r <= size && heap[r].skill > heap[largest].skill)
        {
            largest = r;
        }
        if (i != largest)
        {
            swap(i, largest);
            MaxHeapfy(largest);
        }
    }
    public Person Heap_Extract_Max()
    {
        if (size == -1)
            return null;
        Person final = heap[0];
        heap[0] = heap[size];
        size = size - 1;
        MaxHeapfy(0);
        return final;
    }
    void ExchangeUP(int i)
    {
        while (i > 0 && heap[Parent(i)].skill < heap[i].skill)
        {
            swap(Parent(i), i);
            i = Parent(i);
        }
    }
    void swap(int i, int j)
    {
        Person temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
    public void insert_person(int age, char skill)
    {
        Person NewPer = new Person(age, skill);
        insert_person(NewPer);

    }
    public void insert_person(Person p)
    {
        size++;
        heap[size] = p;
        ExchangeUP(size);

    }
    public void Heap_Increase_SKil(int index, char p)
    {
        if (index > size || index < 0)
            Console.WriteLine("Index out of Range");
        
        heap[index].skill = p;
        ExchangeUP(index);
    }

    public void print()
    {
        Console.Write("{");
        for (int i = 0; i <= size; i++)
        {
            Console.Write("Preson {0} -> [age: {1}, skill: {2}], ", i + 1, heap[i].age, heap[i].skill);
        }
        Console.WriteLine("}");
    }
}
namespace Algoritm
{
    class program
    {
        static void Main(string[] args)
        {
            Maxheap maxheap = new Maxheap();
            do
            {
                Console.WriteLine("\t\t***Program Commands***");
                Console.WriteLine("1: Insert Person");
                Console.WriteLine("2: Update Person Skill");
                Console.WriteLine("3: Extrackt Best Person");
                Console.WriteLine("4: Print Persons");
                Console.WriteLine("5: Quit\n");

                string Phrase = Console.ReadLine();

                if (Phrase == "1")
                {
                    Console.Write("Enter age: ");
                    int age = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Skill(A..F): ");
                    char skill =Convert.ToChar( Console.ReadLine().ToUpper());

                    maxheap.insert_person(age, skill);

                    Console.WriteLine("Inserted");
                }
                else if (Phrase == "2")
                {
                    Console.Write("Enter Index(1..{0}): ",maxheap.size+1);
                    int i = Convert.ToInt32(Console.ReadLine())-1;
                    Console.WriteLine("Skill Now: " + maxheap.heap[i].skill);
                    Console.Write("Enter higher Skill(A..F): ");
                    char s = Convert.ToChar(Console.ReadLine().ToUpper());
                    maxheap.Heap_Increase_SKil(i, s);
                    //Console.WriteLine("Next Node: {0}", key);
                }
                else if (Phrase == "3")
                {
                    Console.Write("Best Person: ");
                    Person p = maxheap.Heap_Extract_Max();
                    Console.Write("[age: {0}, skill: {1}]\n, ",p.age,p.skill);
                }
                else if (Phrase == "4")
                {
                    Console.WriteLine("Print Persons:");
                    maxheap.print();
                    
                }
                else if (Phrase == "5")
                {
                    Console.Write("The End!..");
                    break;
                }
            } while (true);
        }
    }
}
