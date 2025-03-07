VectorInt vec1 = new VectorInt(4);
vec1.insertElement(0, 4);
vec1.insertElement(1, 2);
vec1.insertElement(2, 5);
vec1.insertElement(3, 12);
vec1.ShowVector();
Console.Write("\n");

VectorInt vec2 = new VectorInt(5);
vec2.insertElement(0, 1);
vec2.insertElement(1, 2);
vec2.insertElement(2, 5);
vec2.insertElement(3, 10);
vec2.insertElement(4, 9);
vec2.ShowVector();

VectorInt vec3 = new VectorInt(3);
vec3.insertElement(0, 1);
vec3.insertElement(1, 2);
vec3.insertElement(2, 5);
vec3.ShowVector();

Console.Write("\n");
Console.WriteLine(vec1.compareVector(vec2));
Console.WriteLine("Combined vectors:");
vec1.union(vec2);
vec1.ShowVector();
Console.Write("\n");
Console.WriteLine(vec1.subconjunt(vec2));
Console.WriteLine(vec1.subconjunt(vec3));

VectorInt vec4 = new VectorInt(10);
vec4.insertElement(0, 4);
vec4.insertElement(1, 7);
vec4.insertElement(2, 8);
vec4.insertElement(3, 10);
vec4.insertElement(4, 2);
vec4.insertElement(5, 7);
vec4.insertElement(6, 12);
vec4.insertElement(7, 10);
vec4.insertElement(8, 10);
vec4.insertElement(9, 18);
vec4.ShowVector();
Console.Write("\n");
vec4.deleteDuplicate();
vec4.ShowVector();

public class VectorInt{
    private const int MAX = 2147483591;
    private int length;
    private int[] elements = new int[MAX];
    private bool usedVectorUnion = false;

    public VectorInt(int l){
        length = l;
    }

    private int getLength() {return length;}

    private int getElement(int index){ return elements[index]; }

    private bool confirm(bool yn) {return usedVectorUnion = yn;} 

    private bool unifedWithVector() {return usedVectorUnion; }

    public void insertElement(int index, int value){
        if (index < 0 || index > length) throw new IndexOutOfRangeException("Index is out of range");
        if (index != length){
            insertElement(index + 1, elements[index]);
            elements[index] = value;
        }else{
            elements[index] = value;
        }
    }
    
    public void deleteElement(int index){
        if (index < 0 || index > length) throw new IndexOutOfRangeException("Index is out of range");
        if (index != length){
            elements[index] = elements[index+1];
            elements[index+1] = 0;
            deleteElement(index + 1);
        } else {
            elements[length] = 0;
            length--;
        }
    }

    public bool compareVector(VectorInt v2){
        if(length != v2.getLength()) return false;
        for(int i=0; i<length; ++i){
            if(elements[i] != v2.getElement(i)) return false;
        }
        return true;
    }

    public void union(VectorInt v2){
        for (int i=0; i<v2.getLength(); ++i){
            bool exists = false;
            // Check if the value already exists in the current vector
            for (int j=0; j<length; ++j){
                if (elements[j] == v2.getElement(i)){
                    exists = true;
                    break;
                }
            }
            // If the value doesn't exist, add it to the current vector
            if (!exists){
                if (length >= MAX)throw new InvalidOperationException("Vector is full. Cannot add more elements.");
                elements[length] = v2.getElement(i);
                length++;
            }
        }
        usedVectorUnion = true;
        v2.confirm(true);
    }

    public bool subconjunt(VectorInt v2){
        return usedVectorUnion && v2.unifedWithVector();
    }

    public void deleteDuplicate(){
        for(int i=0; i<length; ++i){
            int j=i+1;
            while(j<length){
                if(elements[i] == elements[j]){
                    deleteElement(j);
                } else {
                    ++j;
                }
            }
        }
    }

    public void ShowVector() {
        Console.Write("[");
        for (int i = 0; i < length; i++) {
            Console.Write(elements[i]);
            if (i < length - 1) Console.Write(" ");
        }
        Console.Write("]");
    }
}