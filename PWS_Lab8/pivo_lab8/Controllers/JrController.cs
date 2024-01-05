using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pivo_lab8.Models;
using pivo_lab8.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace pivo_lab8.Controllers
{
    [RoutePrefix("jr")]
    public class JrController : ApiController
    {
        private readonly JrService _jrService;

        public JrController() 
        {
            _jrService = new JrService();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Process(object reqData)
        {
            try
            {
                Type valueType = reqData.GetType();
                if (valueType.Name == "JArray")
                {
                    var rpcs = JsonConvert.DeserializeObject<JsonRpcReq[]>(reqData.ToString());
                    var res = new List<JsonRpcRes>();
                    for (var i = 0; i < rpcs.Length; i++)
                    {
                        var rpcRes1 = _jrService.ProcessMethod(rpcs[i]);
                        if(rpcRes1 != null && rpcRes1.Id != null)
                            res.Add(rpcRes1);
                    }
                    return Ok(res);
                }
                
                var rpc = JsonConvert.DeserializeObject<JsonRpcReq>(reqData.ToString());
                var rpcRes = _jrService.ProcessMethod(rpc);
                if (rpcRes == null || rpcRes != null && rpcRes.Id == null)
                {
                    return Ok();
                }

                return Ok(rpcRes);
            }
            catch (Exception ex)
            {
                int? id = null;
                if (reqData != null)
                {
                    var property = reqData.GetType().GetProperty("id");

                    if (property != null)
                    {
                        var value = property.GetValue(reqData);
                        if (value != null)
                        {
                            if (int.TryParse(value.ToString(), out var idVal))
                            {
                                id = idVal;
                            }
                        }
                    }
                }

                return Ok(new JsonRpcRes
                {
                    Id = id,
                    Error = new RpcError
                    {
                        Code = -32700,
                        Message = "Parse error"
                    },
                    Jsonrpc = "2.0"
                });
            }
        }
    }
}
