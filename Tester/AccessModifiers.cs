using NUnit.Framework;
using Tester;

namespace Tester
{

// Namespace and assembly:
//  .NET Framework uses namespaces to organize its many classes Secondly, 
//  declaring your own namespaces can help control the scope of class and method names in larger programming projects
    // http://stackoverflow.com/questions/21530460/namespace-or-assembly

// A namespace is a logical grouping of types (mostly to avoid name collisions). 
// An assembly can contain types in multiple namespaces (System.DLL contains a few...), 
// and a single namespace can be spread across assemblies (e.g. System.Threading).
     
// public: Access is not restricted.
// protected: Access is limited to the containing class or types derived from the containing class.
// Internal: Access is limited to the current assembly.
// protected internal: Access is limited to the current assembly or types derived from the containing class.
// private: Access is limited to the containing type.

// By default:
// - Class is internal
// - 

    // The default access for everything in C# is "the most restricted access you could declare for that member"
    // http://stackoverflow.com/questions/2521459/what-are-the-default-access-modifiers-in-c
    // 

            class InternalClass
    {
        string Message { get; set; }

        class PublicInternalClass
        {
            public int number { get; set; }
        }
    }
}

namespace  SecondTester
{
    public class SecondTester
    {
        [Test]
        public void AccessInternalClass()
        {
            //var publicInternalClass = new InternalClass.PublicInternalClass();

        }
    }

}