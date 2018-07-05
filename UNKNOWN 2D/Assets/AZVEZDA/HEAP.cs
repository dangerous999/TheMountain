using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IHeapItem<T> : IComparer<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}

public class HEAP<T> {

    T[] items;
    int currentItem;

    public HEAP(int maxHeapSize)
    {
        items = new T[maxHeapSize];

    }
    public void Add(T item)
    {
        item.HeapIndex = currentItem;
        items[currentItem] = item;
        sortUp(item);
        currentItem++;
    }
    public T removeFirst()
    {
        T first = items[0];
        currentItem--;
        items[0] = items[currentItem];
        items[0].HeapIndex = 0;
    }

    void sortDown(T item)
    {
        while (true)
        {
            int childIL = item.HeapIndex * 2 + 1;
            int childIR = item.HeapIndex * 2 + 2;
            int swapIndex = 0;
            if (childIL < currentItem)
            {
                swapIndex = childIL;
                if (childIR < currentItem)
                {
                    if (items[childIL].compareTo(items[childIR]))
                }
            }
        }
    }
    void sortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                swap(item, parentItem);
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }
    void swap(T a, T b)
    {
        items[a.HeapIndex] = b;
        items[b.HeapIndex] = a;
        ìnt aIndex = a.HeapIndex;
        a.HeapIndex = b.HeapIndex; ;
        b.HeapIndex = aIndex;
    }
}

