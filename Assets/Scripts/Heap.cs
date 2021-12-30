using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];

    }

    public void add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        sortUp(item);
        currentItemCount++;
    }

    public T removeFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        sortDown(items[0]);
        return firstItem;
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    public int count
    {
        get { return currentItemCount; }
    }

    public void updateItem(T item)
    {
        sortUp(item);
    }

    void sortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = 2 * item.HeapIndex + 1;
            int childIndexRight = 2 * item.HeapIndex + 2;
            int swapIndex = 0;

            if(childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;

                if(childIndexRight < currentItemCount)
                {
                    if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0){
                        swapIndex = childIndexRight;
                    }
                }

                if(item.CompareTo(items[swapIndex]) < 0)
                {
                    swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

        }
    }

    void sortUp(T item)
    {
        int parentIndex= (item.HeapIndex-1)/2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if(item.CompareTo(parentItem) > 0)
            {
                swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex-1)/2;
        }
    }

    void swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int temp = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = temp;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
