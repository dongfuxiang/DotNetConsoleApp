namespace ConsoleApp2;
internal class Program
{
    static void Main(string[] args)
    {
        Student student = new Student("Tom");
        Console.WriteLine(student.Name.ToLower());
        //Console.WriteLine(student.NumberPhone.ToLower());//调用可空引用类型时，编译器会给出警告
        if (student.NumberPhone != null)
        {
            Console.WriteLine(student.NumberPhone.ToLower());//可以消除编译器的警告
        }
        //Console.WriteLine(student.NumberPhone!.ToLower());//也可以消除编译器的警告，但依旧会抛异常


        //Record 记录类型
        Person p1 = new Person(1, "Tom", 12);
        Person p2 = new Person(1, "Tom", 12);
        Person p3 = new Person(3, "Jerry", 14);
        Console.WriteLine(p1.ToString());//打印出每个属性的值:Person { Id = 1, Name = Tom, Age = 12 }
        Console.WriteLine(p1 == p2);//虽然是不同的对象，但是属性的值一样，此处返回True
        Console.WriteLine(p1 == p3);//返回False

        Person p4 = p1 with { };//创建p1的一个副本，内容一样，但是指向不同的引用
        Person p5 = p1 with { Age = 17 };//创建副本时，给指定属性赋值

     
        Console.WriteLine("Hello, World!");
    }
}


class Student
{
    public string Name { get; set; }//不可为空的引用类型
    public string? NumberPhone { get; set; }//可为空的引用类型
    public Student(string name)
    {
        this.Name = name;
    }


}
