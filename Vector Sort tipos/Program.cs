VectorInt vec = new VectorInt(7);
vec.addElement(0, 6);
vec.addElement(1, 3);
vec.addElement(2, 1);
vec.addElement(3, 2);
vec.addElement(4, 9);
vec.ShowVector();
vec.mergeSort();
vec.ShowVector();


class VectorInt {
    private const int MAX = 2147483591;
    private int length;
    private int[] elements = new int[MAX];

    // Constructor
    public VectorInt(int l) {
        if (l < 0 || l > MAX) throw new ArgumentException("Invalid length");
        length = l;
    }

    public void addElement(int index, int element) {
        if (index >= length || index < 0) throw new ArgumentException("Index out of range or negative");
        elements[index] = element;
    }

    public int getElement(int index) {
        if (index >= length || index < 0) throw new IndexOutOfRangeException("Index out of range or negative.");
        return elements[index];
    }

    public int getLength() {
        return length;
    }

    public void modifyLength(int new_length) {
        if (new_length < 0 || new_length > MAX) throw new ArgumentException("Invalid length");
        length = new_length;
    }

    public void selectionSort() {
        for (int i = 0; i < length; i++) {
            int minIndex = i;
            for (int j = i + 1; j < length; j++) {
                if (elements[j] < elements[minIndex]) minIndex = j;
            }
            int temp = elements[minIndex];
            elements[minIndex] = elements[i];
            elements[i] = temp;
        }
    }

    public void insertionSort() {
        for (int i = 1; i < length; i++) {
            int key = elements[i];
            int j = i - 1;
            while (j >= 0 && elements[j] > key) {
                elements[j + 1] = elements[j];
                j--;
            }
            elements[j + 1] = key;
        }
    }

    private void merge(int left, int mid, int right) {
        int i, j, k, n1 = mid - left + 1, n2 = right - mid;
        int[] L = new int[n1], R = new int[n2];
        for (i = 0; i < n1; i++) L[i] = elements[left + i];
        for (j = 0; j < n2; j++) R[j] = elements[mid + 1 + j];
        i = 0; j = 0; k = left;
        while (i < n1 && j < n2) { 
            if (L[i] <= R[j]) {
                elements[k] = L[i]; i++;
            } else {
                elements[k] = R[j]; j++;
            } 
            k++; 
        }
        while (i < n1) {elements[k] = L[i]; i++; k++;}
        while (j < n2) {elements[k] = R[j]; j++; k++;}
    }

    private void mergeSortPriv(int left, int right) {
        if (left < right) {
            int mid = left + (right - left) / 2;
            mergeSortPriv(left, mid);
            mergeSortPriv(mid + 1, right);
            merge(left, mid, right);
        }
    }

    public void mergeSort() { mergeSortPriv(0, length - 1);}

    public void ShowVector() {
        Console.Write("[");
        for (int i = 0; i < length; i++) {
            Console.Write(elements[i]);
            if (i < length - 1) Console.Write(" ");
        }
        Console.Write("]");
    }
}