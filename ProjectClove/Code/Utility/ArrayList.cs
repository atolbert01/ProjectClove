namespace ProjectClove
{
    class ArrayList<T>
    {
        public T[] Items;
        public ArrayList(){}

        public ArrayList(T[] items)
        {
            Items = items;
        }

        public void Add(T newItem)
        {
            T[] newItems = new T[Items.Length + 1];
            for (int i = 0; i < Items.Length; i++)
            {
                newItems[i] = Items[i];
            }
            newItems[Items.Length] = newItem;
            Items = newItems;
        }

        public void Remove(int removedIndex)
        {
            if (Items.Length != 0)
            {
                T[] resizedItems = new T[Items.Length - 1];

                int j = 0;
                for (int i = 0; i < Items.Length; i++)
                {
                    if (i != removedIndex)
                    {
                        resizedItems[j] = Items[i];
                        j++;
                    }
                }
                Items = resizedItems;
            }
        }

        public void Insert(int currentIndex, T newItem)
        {
            T[] resizedItems = new T[Items.Length + 1];

            int j = 0;
            for (int i = 0; i < resizedItems.Length; i++)
            {
                if (i != currentIndex + 1)
                {
                    resizedItems[i] = Items[j];
                    j++;
                }
                else
                {
                    resizedItems[i] = newItem;
                }
            }
            Items = resizedItems;
        }
    }
}
