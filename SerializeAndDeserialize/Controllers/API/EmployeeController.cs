using Newtonsoft.Json;
using SerializeAndDeserialize.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;

namespace SerializeAndDeserialize.Controllers.API
{
    [RoutePrefix("Employee")]
    public class EmployeeController : ApiController
    {
        private  IEnumerable<Employee> employees;
        public EmployeeController()
        {
            this.employees = new List<Employee>
            {
                new Employee {Id = 1,FName = "mostafa" , LName = "mohamed" , Email = "tenant_developer@outlook.com"},
                new Employee {Id = 2,FName = "Ahmed" , LName = "ibrahim" , Email = "kfnf@outlook.com"},
                new Employee {Id = 3,FName = "Khaled" , LName = "yossery" , Email = "gdv@outlook.com"},
                new Employee {Id = 4,FName = "Aliaa" , LName = "galal" , Email = "btf@outlook.com"},
                new Employee {Id = 5,FName = "nada" , LName = "mohamed" , Email = "vbg@outlook.com"},

            };
        }
        [Route("GetSerializing")]
        public IHttpActionResult GetSerializing()
        {
           
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(IEnumerable<Employee>));
            MemoryStream msobj = new MemoryStream();
            js.WriteObject(msobj, employees);
            msobj.Position = 0;
            StreamReader sr = new StreamReader(msobj);
            string json = sr.ReadToEnd();
            sr.Close();
            msobj.Close();
            return Ok(json);
        }
        [Route("GetDeserializing")]
        public IHttpActionResult GetDeserializing()
        {
            string json = "[{\"Email\":\"tenant_developer@outlook.com\",\"FName\":\"mostafa\",\"Id\":1,\"LName\":\"mohamed\"},{\"Email\":\"kfnf@outlook.com\",\"FName\":\"Ahmed\",\"Id\":2,\"LName\":\"ibrahim\"},{\"Email\":\"gdv@outlook.com\",\"FName\":\"Khaled\",\"Id\":3,\"LName\":\"yossery\"},{\"Email\":\"btf@outlook.com\",\"FName\":\"Aliaa\",\"Id\":4,\"LName\":\"galal\"},{\"Email\":\"vbg@outlook.com\",\"FName\":\"nada\",\"Id\":5,\"LName\":\"mohamed\"}]";
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(IEnumerable<Employee>));
                IEnumerable<Employee> _employees = (IEnumerable<Employee>)deserializer.ReadObject(ms);
                return Ok(_employees);
            }
        }
        [Route("GetSerializingWithJsonConvert")]
        public IHttpActionResult GetSerializingWithJsonConvert()
        {
            string jsonData = JsonConvert.SerializeObject(employees);
            return Ok(jsonData);
        }
        [Route("GetDeserializingWithJsonConvert")]
        public IHttpActionResult GetDeserializingWithJsonConvert()
        {
            string json = "[{\"Email\":\"tenant_developer@outlook.com\",\"FName\":\"mostafa\",\"Id\":1,\"LName\":\"mohamed\"},{\"Email\":\"kfnf@outlook.com\",\"FName\":\"Ahmed\",\"Id\":2,\"LName\":\"ibrahim\"},{\"Email\":\"gdv@outlook.com\",\"FName\":\"Khaled\",\"Id\":3,\"LName\":\"yossery\"},{\"Email\":\"btf@outlook.com\",\"FName\":\"Aliaa\",\"Id\":4,\"LName\":\"galal\"},{\"Email\":\"vbg@outlook.com\",\"FName\":\"nada\",\"Id\":5,\"LName\":\"mohamed\"}]";
           var data =  JsonConvert.DeserializeObject<IEnumerable<Employee>>(json);
            return Ok(data);
        }
    }
}
