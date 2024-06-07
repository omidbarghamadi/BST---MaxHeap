class Node
{
    public int data;
    public Node Rchild;
    public Node Lchild;
    public Node parent;

    public Node(int data)
    {
        this.data = data;
        Rchild = null;
        Lchild = null;
        parent = null;
    }

    public Node()
    {
        this.data = 0;
        Rchild = null;
        Lchild = null;
        parent = null;
    }
}

class BST
{
    public Node root;

    public BST(Node root)   //Contructor
    {
        this.root = root;
    }
    public BST()    //Contructor
    {
        root = null;
    }           

    public Node Search(int key)     //Search Key in the Tree    T(n) = O(h)
    {
        Node r = root;
        while (r != null && key != r.data)
        {
            if (key < r.data)
                r = r.Lchild;
            else
                r = r.Rchild;
        }
        return r;
    }

    public Node Tree_Minimum(Node x)    //Find Minimum data in the Tree    T(n) = O(h)
    {
        while(x.Lchild!=null)
            x = x.Lchild;
        return x;
    }
    public Node Tree_Maximum(Node x)    //Find Minimum data in the Tree    T(n) = O(h)
    {
        while (x.Rchild != null)
            x = x.Rchild;
        return x;
    }

    public void Insert_Key(int data)   //Insert Data in the Tree     T(n) = O(h)
    {
        Node z = new Node(data);
        Node x = root;
        Node y = null;

        while (x != null)
        {
            y = x;
            if (z.data < x.data)
                x = x.Lchild;
            else
                x = x.Rchild;
        }
        z.parent = y;

        if (y == null)
            root = z;
        else if (z.data < y.data)
            y.Lchild = z;
        else
            y.Rchild = z;
    }   
    public void Insert_Node(Node NewNode)   //Insert Node in the Tree
    {
        if (NewNode == null)
            return;
        Insert_Key(NewNode.data);
        Insert_Node(NewNode.Lchild);
        Insert_Node(NewNode.Rchild);
    }

    public Node Next_Node(Node x)       //Find the Next Node in the Tree  T(n) = O(h)
    {
        if (x.Rchild != null)       //If it has a right sub-tree
            return Tree_Minimum(x.Rchild);
        Node y = x.parent;  
        while (y != null && x == y.Rchild)
        {
            x = y;
            y = y.parent;
        }
        return y;
    }
    public Node Next_Node(int key)
    {
        return Next_Node(Search(key));
    }

    public Node Before_Node(Node x)     //Find the Previous Node in the Tree  T(n) = O(h)
    {
        if (x.Lchild != null)
            return Tree_Maximum(x.Lchild);
        Node y = x.parent;
        while (y != null && x == y.Lchild)
        {
            x = y;
            y = y.parent;
        }
        return y;
    }
    public Node Before_Node(int key)
    {
        return Before_Node(Search(key));
    }

    public void Delete(int key) //Find the Key then Return Node
    {
        Delete(Search(key));
    }
    private void Delete(Node z) //Delete Node And 
    {
        Node y,x;
        if (z.Lchild == null || z.Rchild == null)   //If the Node Has at Most One Child
            y = z;
        else        //If the node has two children
            y = Next_Node(z);

        if(y.Lchild != null)
            x = y.Lchild;
        else
            x = y.Rchild;

        if (x != null)
            x.parent = y.parent;

        if (y.parent == null)
            root = x;
        else if (y == y.parent.Lchild)
            y.parent.Lchild = x;
        else
            y.parent.Rchild = x;

        if (y != z)
            z.data = y.data;
    }

    public static BST Merge(BST b1, BST b2) //merge Two Bst   O(n) = O(h1) + O(h2)
    {
        BST MergeBST = new BST();
        MergeBST.Insert_Node(b1.root);
        MergeBST.Insert_Node(b2.root);
        return MergeBST;
    }

    
    public Maxheap Kth_Biggest(int k)
    {
        Node max = Tree_Maximum(root);
        Maxheap maxheap = new Maxheap(k);

        while (k>0)
        {
            maxheap.Insert_to_Heap(max.data);
            max = Before_Node(max);
            k--;
        }
        return maxheap;
    }
    public void Print()
    {
        if (root == null)
            Console.Write("BST is empty!");
        else
            Print(root);
    }
    public void Print(Node x)   //Print Inorder Keys    T(n) = O(n)
    {
        if (x == null)
            return;

        Print(x.Lchild);
        Console.Write(x.data + " ");
        Print(x.Rchild);
    }
}

class Maxheap
{
    private int[] heap;
    private int size;
    public Maxheap(int n)
    {
        heap = new int[n];
        size = 0;
    }
    public int LEft(int index)
    {
        return heap[index * 2];
    }
    private int Parent(int index)
    {
        return (index - 1) / 2;
    }
    public void Max_Heapify(int index)
    {
        while (index > 0 && heap[index] > heap[Parent(index)])
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }
    private void Swap(int index1, int index2)
    {
        int temp = heap[index1];
        heap[index1] = heap[index2];
        heap[index2] = temp;
    }
    public void Insert_to_Heap(int data)
    {
        heap[size++] = data;
        Max_Heapify(size - 1);
    }
    private void HeapifyUp(int index)
    {
        while (index > 0 && heap[index] > heap[Parent(index)])
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    public string ToString()
    {
        string s = "[";
        foreach (int n in heap)
        {
            s += n.ToString() + ", ";
        }
        return s + "]";
    }
}
namespace AlgorithmDesignProj
{
    class program
    {
        static void Main(string[] args)
        {
            BST mainbst = new BST();
            do
            {
                Console.WriteLine("\t\t***Program Commands***");
                Console.WriteLine("1: Insert Node");
                Console.WriteLine("2: Next Node");
                Console.WriteLine("3: Before Node");
                Console.WriteLine("4: Merge Two BST");
                Console.WriteLine("5: Kth Largest Put Into MaxHeap");
                Console.WriteLine("6: Print BST");
                Console.WriteLine("7: Maximum Tree");
                Console.WriteLine("8: Minimum Tree");
                Console.WriteLine("9: Empty Tree");
                Console.WriteLine("10: Quit\n");

                string Phrase = Console.ReadLine();

                if (Phrase == "1")
                {   
                    Console.Write("Enter number: ");
                    mainbst.Insert_Key(Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine("Inserted");
                }
                else if (Phrase == "2")
                {
                    Console.Write("Enter number: ");
                    int key = mainbst.Next_Node(Convert.ToInt32(Console.ReadLine())).data;
                    Console.WriteLine("Next Node: {0}",key);
                }
                else if (Phrase == "3")
                {
                    Console.Write("Enter number: ");
                    int key = mainbst.Before_Node(Convert.ToInt32(Console.ReadLine())).data;
                    Console.WriteLine("Before Node: {0}", key);
                }
                else if (Phrase == "4")
                {
                    Console.WriteLine("Enter Lentgh BST2:");
                    int len = Convert.ToInt32(Console.ReadLine());

                    BST bst2 = new BST();

                    for (int i = 0; i < len; i++)
                    {
                        Console.Write("Enter number BST: ");
                        bst2.Insert_Key(Convert.ToInt32(Console.ReadLine()));
                    }
                    Console.Write("BST Merged: <");
                    BST.Merge(mainbst, bst2).Print();
                    Console.Write(">\n");

                }
                else if (Phrase == "5")
                {
                    Console.Write("Enter K: ");
                    Maxheap MH = mainbst.Kth_Biggest(Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine("Max Heap:" + MH.ToString());
                }
                else if (Phrase == "6")
                {
                    Console.Write("BST: <");
                    mainbst.Print();
                    Console.Write(">\n");
                }
                else if (Phrase == "7")
                {
                    Console.WriteLine("Maximum BST: {0}",mainbst.Tree_Maximum(mainbst.root).data);
                }
                else if (Phrase == "8")
                {
                    Console.WriteLine("Minimum BST: {0}", mainbst.Tree_Minimum(mainbst.root).data);
                }
                else if (Phrase == "9")
                {
                    mainbst.root = null;
                    Console.WriteLine("Empty Tree!..");
                }
                else if (Phrase == "10")
                {
                    Console.Write("The End!..");
                    break;
                }
            } while (true);
        }
    }
}
