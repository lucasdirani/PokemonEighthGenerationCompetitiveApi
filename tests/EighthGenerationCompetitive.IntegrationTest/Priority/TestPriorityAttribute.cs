using System;

namespace EighthGenerationCompetitive.IntegrationTest.Priority
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; private set; }
    }
}