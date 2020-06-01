using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_trees
{
    class Program
    {
        public class AVLTree<TKey, TValue> : IDictionary<TKey, TValue>
        {
            IComparer<TKey> comparer;
            private class Node
            {
                public int Count;
                public Node left;
                public Node right;
                public int height;
                public TKey key;
                public TValue value;

                public Node(TKey x, TValue str)
                {
                    key = x;
                    value = str;
                }
            }
            private Node root1 = null;
            private Node root;
            private int count;
            public AVLTree()
            {
                comparer = Comparer<TKey>.Default;
            }
            public int Count
            {
                get { return count; } 
            }
            public ICollection<TKey> Keys => throw new NotImplementedException();

            public ICollection<TValue> Values => throw new NotImplementedException();

            public bool IsReadOnly => throw new NotImplementedException();

            public TValue this[TKey key]
            {
                get
                {
                    Node r = root;
                    r = contains(r, key);
                    root1 = r;
                    return r.value;
                }
                set
                {
                    Node r = root;
                    r = contains(r, key);
                    r.value = value;

                }
            }
            public bool ContainsKey(TKey key)
            {
                Node p = root;
                p = contains(p, key);
                if ( p== null)
                {
                    return false;
                }
                else return comparer.Compare(key, p.key) == 0; 

            }
            private Node contains(Node p, TKey x)
            {
                if (x == null || root == null) return null;
                while (p != null)
                {
                   // Console.WriteLine(p.key);
                    try
                    {
                        if (comparer.Compare(x, p.key) < 0)
                        {

                            p = p.left;
                        }
                        else if (comparer.Compare(x, p.key) > 0)
                        { 
                            p = p.right;

                        }
                        else if (comparer.Compare(x, p.key) == 0) return p;
                    }
                    catch
                    {
                        return null;
                    }
                }
                return null;
            }
            private Node getNode(TKey key)
            {
                Node p = root;
                p = contains(p, key);
                return p;
            }

            public void Add(TKey key, TValue value)
            {
                root = insert(root, key, value);
            }
            private Node insert(Node p, TKey x, TValue y)
            {
                if (p == null)
                    return new Node(x, y);
                if (comparer.Compare(x, p.key) < 0)// x < p.key
                    p.left = insert(p.left, x, y);
                else if (comparer.Compare(x, p.key) > 0)// x > p.key
                    p.right = insert(p.right, x, y);
                else p.Count++;
                p.height = 1 + Math.Max(height(p.left), height(p.right));
                int balance = p == null ? 0 : height(p.left) - height(p.right);

                if (balance > 1 && comparer.Compare(x, p.left.key) > 0)//left left 
                    return rightRotate(p);
                if (balance < -1 && comparer.Compare(x, p.right.key) < 0)//right right
                    return leftRotate(p);
                if (balance > 1 && comparer.Compare(x, p.left.key) > 0)//lr
                {
                    p.left = leftRotate(p.left);
                    return rightRotate(p);
                }
                if (balance < -1 && comparer.Compare(x, p.right.key) < 0)//rl
                {
                    p.right = rightRotate(p.right);
                    return leftRotate(p);
                }

                return p;
            }
            private int height(Node p)
            {
                return p == null ? 0 : p.height;
            }
            private Node rightRotate(Node y)
            {
                var x = y.left;
                var T2 = x.right;

                x.right = y;
                y.left = T2;

                y.height = 1 + Math.Max(height(y.left), height(y.right));
                x.height = 1 + Math.Max(height(x.left), height(x.right));

                return x;
            }
            private Node leftRotate(Node x)
            {
                var y = x.right;
                var T2 = y.left;

                y.left = x;
                x.right = T2;

                x.height = 1 + Math.Max(height(x.left), height(x.right));
                y.height = 1 + Math.Max(height(y.left), height(y.right));


                return y;
            }
            public void pl (TKey key)
            {
                Node g = root;
                g = contains(g, key);

            }
            public bool Remove(TKey key)
            {
                throw new NotImplementedException();
            }

            public bool TryGetValue(TKey key, out TValue value)
            {
                throw new NotImplementedException();
            }

            public void Add(KeyValuePair<TKey, TValue> item)
            {
                Add(item.Key, item.Value);

                //throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(KeyValuePair<TKey, TValue> item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public bool Remove(KeyValuePair<TKey, TValue> item)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            
            public void Print()
            {
                if (root != null)
                {
                    print(root, 0);
                    Console.WriteLine();
                }
            }
            private void print(Node p, int shift)
            {
                if (p.right != null)
                    print(p.right, shift + 1);

                for (int i = 0; i != shift; i++)
                    Console.Write("  ");
                Console.WriteLine("{0} {1}", p.value, p.key);

                if (p.left != null)
                    print(p.left, shift + 1);

            }
        }
        static void Main(string[] args)
        {
            var a = new AVLTree<string, int>();
            //var a = new SortedDictionary<string, int>();
            //string input_text = "a b  l d e  g  h h k c m ";
            string str = "";
            string input_text = System.IO.File.ReadAllText(@"big.txt");
           // string[] input_check = System.IO.File.ReadAllLines(@"check.txt");
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            long elapsedMs;
            foreach (var i in input_text)
            {
                if (i >= 'a' && i <= 'z' || i >= 'A' && i <= 'Z'||i=='\'')
                {
                    str += i;
                }
                else if (str.Length > 0)
                {
                    if (a.ContainsKey(str))
                    {
                        ++a[str];
                    }
                    else
                    {
                        a.Add(str, 1);
                    }
                    str = "";

                }
            }
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            System.Console.WriteLine("time:  " + elapsedMs);
            //a.Print();

           
        }
    }

}
