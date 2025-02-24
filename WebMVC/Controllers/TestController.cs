using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Demo1()//Action:操作方法，访问这个方法时，就用控制器的名字+方法名字
        {
            Person p1 = new Person("TOM", true, DateTime.Now);

            return View(p1);//意思是：让和此方法名称相同的视图去渲染这个模型
        }
    }
}
