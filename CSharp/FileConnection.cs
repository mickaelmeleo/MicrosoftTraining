using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSharp
{
    public class FileConnection
    {
        public string ConnectionString { get; set; }

        public override int GetHashCode()
        {
            return 52302769;
        }

//        public override bool Equals(object obj)
//        {
//            return GetHashCode() == obj.GetHashCode();
//        }
    }

    public class TestFileConnection
    {
        [Test]
        public void Method_Scenario_Result()
        {
            var fileConnection = new FileConnection {ConnectionString = "1"};
            var fileConnection2 = new FileConnection {ConnectionString = "1"};

            Console.WriteLine(fileConnection.GetHashCode());
            Console.WriteLine(fileConnection2.GetHashCode());

            var dictionary = new Dictionary<FileConnection, string>
            {
                {fileConnection, "test 1"},
                {fileConnection2, "test 2"}
            };

            var containsKey = dictionary.ContainsKey(fileConnection);
            Console.WriteLine(containsKey);

            //int outputint;
            //dictionary.TryGetValue(connection1, out outputint);

            //Console.WriteLine(outputint);
        }
    }
}