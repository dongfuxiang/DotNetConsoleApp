using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_1.Models;

namespace WebAPI_1.Controllers
{
    [Route("api/[controller]/[action]")]//URL路径匹配的则是action方法
    [ApiController]
    public class Test2Controller : ControllerBase
    {

        [HttpGet]//action方法,URL:api/Test2/Add?x=1&y=2
        public int Add(int x, int y)
        {
            return x + y;
        }

        [HttpGet("{x}/{y}")]//如果两个方法名一样，路由会优先匹配带特定路由属性的action方法 路由为："api/[controller]/[action]/{x}/{y}" ,api/Test2/Add2/1/2
        public int Add2(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        /// IActionResult，可以返回状态码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetResult(int id)
        {
            switch (id)
            {
                case 1: return Ok(88);//200状态码
                case 2: return Ok(99);
                default: return NotFound("ID错误");//404状态码
            }

        }

        /// <summary>
        /// ActionResult<T> ，函数直接返回T，会把T转换为ActionResult<T>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<int> GetResult1(int id)
        {
            switch (id)
            {
                case 1: return 88;//200状态码
                case 2: return 99;
                default: return NotFound("ID错误");//404状态码
            }
        }

        [HttpGet("{i1}/{i2}")]//URL：api/Test2/Multi/i1/i2 ,这样就可以不把参数放在QueryString里面，更符合Restful
        public ActionResult<int> Multi(int i1, int i2)
        {
            return i1 * i2;
        }

        [HttpGet("students/school/{schoolName}/class/{classNo}")]//参数来自URL中
        //当参数列表里的参数名称和URL路径中占位符不一致时，可用[FromRoute(Name = "classNo")]
        public ActionResult<Person> GetStudents(string schoolName, [FromRoute(Name = "classNo")] int classNum)
        {
            return new Person("张三", 12);
        }

        [HttpPost]//这种不用URL占位符代表函数参数的方式（默认方式），就是参数在QueryString中，api/Test2/Save?x=1&y=2
        public ActionResult<int> Save(int x,int y)
        {
            return x + y;
        }
    }
}
