using System;
using NUnit.Framework;

namespace CSharp
{
    public class ShortCircuit
    {
        [Test]
        public void Method_Scenario_Result()
        {
            var y = false;
            var x = false;
            object o = new Person();
            object o2 = null;

            if (o != null)
            {
                CheckO(o);
            }

            if ((o == null ? CheckO(o) : false))
            {
                CheckO(o);
            }


            object b = o ?? CheckO(o);

            if (o != null & CheckO(o))
            {
                Console.WriteLine("normal check");
            }
        }

        [Test]
        public void Method_Scenario_2()
        {
            var y = false;
            var x = false;
            object o = null;

            if (o != null && CheckO(o))
            {
                Console.WriteLine("short circuit check");
            }
        }

        private bool CheckY()
        {
            Console.WriteLine("Checking Y");
            return true;
        }

        private bool CheckO(object o)
        {
            Console.WriteLine("Checking X");
            return false;
        }
    }

    public class Person
    {
    }
}