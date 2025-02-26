using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_1.Models;

namespace WebAPI_1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 每次请求都会重新生成一个控制器实例，所以每次都会实例化此变量
        /// </summary>
        private Dictionary<int, Person> dicPerson = new Dictionary<int, Person>();


        [HttpGet]
        public IEnumerable<Person> GetPeople()
        {
            return dicPerson.Values;
        }

        //[HttpGet]//URL为：api/[controller]?id=1
        [HttpGet("{id}")]//URL为：api/[controller]/{id}
        public Person GetPerson(int id)
        {
            return dicPerson.Where(p => p.Key == id).FirstOrDefault().Value;
        }
        [HttpPut("{id}")]
        public void UpdatePerson(int id, Person person)
        {
            dicPerson[id] = person;
        }

        //[HttpPost]
        //public void SavePerson(Person person)
        //{

        //}
        [HttpDelete("{id}")]
        public void DeletePerson(int id)
        {
            dicPerson.Remove(id);
        }
        [HttpPost]
        public string[] SaveNote(SaveNoteRequest req)
        {
            using (FileStream fs = new FileStream($"{req.Title}.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(req.Content);
                }
            }

            return new string[] { "OK,", req.Title };
        }
    }
}
