1.尽量按照Rest（符合HTTP语义的风格）编写接口；
2.HTTP谓词：GET，POST（新增），PUT（整体更新），DELETE，PATCH（局部更新等）
3.幂等：执行多次的结果都是一样。DELETE，PUT，GET是幂等，POST不是幂等；
4.GET的响应可以被缓存，
5.服务器端要通过状态码来反映资源获取的结果：404、403（没有权限）、201（新增成功）等。


REST的优点
1.通过URL对资源定位，语义更清晰；
2.通过HTTP谓词表示不同的操作，接口自描述；
3.可以对GET、PUT、DELETE请求进行重试；
4.可以用GET请求做缓存；
5.通过HTTP状态码反映服务器端的处理结果，统一错误处理机制；
6.网关等可以分析请求处理结果；

REST缺点
1.真实系统中的资源非常复杂，很难清晰地进行资源的划分，对技术人员的业务和技术水平较高；
2.不是所有的操作都能简单的定位到HTTP谓词中；
3.系统地进化可能会改变幂等性；
4.通过URL进行资源定位不符合中文用户习惯；
5.HTTP状态码个数有限；
6.有些环节会篡改非200响应码的响应报文；
7.有的客户端不支持PUT、DELETE请求；

HTTP传递参数的三种方式
1.URL：适合定位；长度限制；语义：资源定位；
2.QueryString：灵活；长度限制；语义：URL之外的额外数据；
3.请求报文体：灵活；长度不限制；不支持GET，DELETE；语义：供PUT、POST等提供数据；
建议：
1).对于保存、更新类的请求POST、PUT，把全部参数都放到请求报文体中；
2).对于DELETE请求，要传递的参数就是一个资源的id，因此把参数放到QueryString中即可；
3).对于GET请求，一般参数内容不会太长，因此统一通过QueryString传递参数就可以。对于极少数内容超过URL限制的请求，
	由于GET、PUT请求都是幂等的，因此我们把请求改成通过PUT请求，然后通过报文来传递参数；

返回状态码：400派
1.对于数据库服务器连接失败、请求报文格式、服务端异常等非业务错误，服务端应该返回4xx，5xx等状态码；
2.对于业务层面的错误，比如用户不存在，我们要使用4xx等HTTP状态码返回。同样在响应报文体中给出详细的
	错误信息，比如{"code":"3","message":"用户不存在"}，这样可以区分出问题出现在哪里；

REST落地建议：
1.路径使用RPC风格：Users/AddNew、Users/GetAll、Users/DeleteById;
2.对于可以缓存的操作，使用GET请求；对于幂等更新操作，使用PUT请求；对于幂等删除操作，使用DELETE请求；对于其他请求，统一使用POST请求；
3.参数：保存、更新类的请求使用POST、PUT请求，把全部参数放到请求报文体中；对于GET和DELETE请求，把参数放到QueryString中。
	推荐尽量使用URL做资源定位；
4.对于业务错误，服务端返回合适的4XX状态码，不知道该选择哪个状态码就用	400；同时，在报文体中通过code参数提供业务错误码以及错误消息；
5.如果请求执行成功，服务端返回值为200的HTTP状态码，如果有需要返回给客户端的数据，则服务端会把这些数据放到响应报文体中；

实现技术：
1.控制器上[Route("[controller]/[action]")];
2.强制要求控制器中不同的操作用不同的方法名；
3.把[HttpGet],[HttpPost],[HttpDelete],[HttpPut]等添加到对应的操作方法上；

Action方法的异步
1.Action方法既可以同步也可以异步；
2.异步Action方法名字一般不需以Async结尾，因为Action方法不会认为去调用；
3.Web API中Action方法的返回值如果是普通数据类型，那么数据就会被默认序列化为Json格式，如返回Person类；
4.WebAPI中的Action方法的返回值同样支持IActionResult类型（MVC），返回值为int等不包含消息类型，因此Swaggar
	等无法推断出消息类型，所以推荐使用ActionResult<T>，它支持类型转换，从而用起来更简单。


捕捉URL占位符
1.在[HttpGet]、[HttpPost]等中使用占位符，比如{schoolName},捕捉路径中的内容，从而供Action方法的参数使用
	/Students/GetAll/school/MIT/class/A001
	[HttpGet("school/{schoolName}/class{classNo}")]
2.捕捉的值会被自动赋值给Action中的同名参数，如果名字不一致，可用[FromRoute(Name="名字")]

捕捉QueryString的值
1.使用[FromQuery]来获取QueryString中的值，如果名字一致，只要为参数添加[FromQuery]即可；如果名字不一致，[FromQuery(Name=名字)].
2.QueryString和Route可以混用；
