using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    //Record 记录类型 ，Record重写了ToString，Equals
    //Record的所有构造函数定义的属性都是只读不可写的
    //可以将普通Class也替换为Record
    public record Person(int Id, string Name, int Age);
    public record Person1(int Id, string Name, int Age)
    {
        public string NickName { get; set; }//这样写的属性可读可写
        public Person1(int Id, string Name, int Age, string nickName) :
            this(Id, Name, Age)
        {
            NickName = nickName;
        }//默认生成的构造函数行为不能修改，可以为类型提供多个构造方法，然后其他构造方法通过this调用默认构造方法
    }



}
